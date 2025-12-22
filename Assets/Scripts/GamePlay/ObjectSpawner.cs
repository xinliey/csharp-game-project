using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 


public class ObjectSpawner : MonoBehaviour
{
    //define spawn object area 
    [SerializeField] float spawnArea_height = 5f;
    [SerializeField] float spawnArea_width = 5f;

    [SerializeField] GameObject[] spawn; //list of the different item to be spawn 
    int length; 
    [SerializeField] float probability = 0.1f;
    [SerializeField] int spawnCount = 1; //to make one timetick able to spawn more than one object 
    [SerializeField] int objectSpawnLimit = -1; //-1 mean no limit
    [SerializeField] bool oneTime = false;
    List<SpawnedObject> spawnedObjects; //keeping record of items spawned to send them to spawnedobject script
    [SerializeField] JSONStringList targetSaveJSONList; //reference line inside the list
    [SerializeField] int idInList = -1; //different list contain different id ,to not confuse the system
    
    private void Start()
    {
        length = spawn.Length; 
        if(oneTime == false)
        {
               TimeAgent timeAgent = GetComponent<TimeAgent>();
               timeAgent.onTimeTick += Spawn;
               spawnedObjects = new List<SpawnedObject>();

            LoadData();// load previous item
        }
        else
        {
            Spawn(null); //in here we didnt assign gametime parameter since this
                         //function is not depend of the time from game
            Destroy(gameObject); 
        }
     
    }
   
    internal void SpawnedObjectDestroyed(SpawnedObject spawnedObject)
    {
        spawnedObjects.Remove(spawnedObject);
    }
    void Spawn(GameTime gameTime)
    {
        
        
        if (UnityEngine.Random.value > probability)
        {//not spawning every time tick but will spawn base on probability
             return; 
        }
        //check the amount of the spawned object and its limit 
        if(objectSpawnLimit <= spawnedObjects.Count && objectSpawnLimit != -1) { return; }
        //if probability check is true , we will proceed to spawn object 
        for(int i = 0; i< spawnCount; i++)//spawn more than one object 
        {
            //instantiate the object on the scene

            int id = UnityEngine.Random.Range(0, length);
                GameObject go = Instantiate(spawn[id]); //spawning random item on the list 
                
                //store the reference to the transform
                Transform t = go.transform;
                
                if(oneTime == false)
                {
           
                t.SetParent(transform);
                SpawnedObject spawnedObject = go.AddComponent<SpawnedObject>();
                //below , store it into the lsit 
                spawnedObjects.Add(spawnedObject);
                //assign the id to the object
                spawnedObject.objId = id; 
               

                }


            Vector3 position = transform.position;
                position.x += UnityEngine.Random.Range(-spawnArea_width, spawnArea_width);
                position.y += UnityEngine.Random.Range(-spawnArea_height, spawnArea_height);

                t.position = position;
        }
        
    }
    //save all spawned object list 
    public class ToSave
    {
        public List<SpawnedObject.SaveSpawnedObjectData> spawnedObjectDatas;
        public ToSave()
        {
            spawnedObjectDatas = new List<SpawnedObject.SaveSpawnedObjectData>();
        }
    }
    string Read()
    {
        ToSave toSave = new ToSave();
        for(int i = 0; i< spawnedObjects.Count; i++)
        {
            toSave.spawnedObjectDatas.Add(new SpawnedObject.SaveSpawnedObjectData(
                spawnedObjects[i].objId,
                spawnedObjects[i].transform.position)
                );
        }
        return JsonUtility.ToJson(toSave);
    }
    public void Load(string json)
    {
        //check if json is null
        if(json == " "|| json == "{}"||json == null)
        {
            return; 
        }
        //if it's not empyty 
        ToSave toLoad = JsonUtility.FromJson<ToSave>(json);
        for(int i = 0; i<toLoad.spawnedObjectDatas.Count; i++)
        {
            SpawnedObject.SaveSpawnedObjectData data = toLoad.spawnedObjectDatas[i];
            GameObject go = Instantiate(spawn[data.objectId]);
           
            go.transform.position = data.worldPosition; 
            go.transform.SetParent(transform);
            SpawnedObject so = go.AddComponent<SpawnedObject>();
            so.objId = data.objectId;
            spawnedObjects.Add(so); 
             
        }
    }
    private void OnDestroy()
    {
        SaveData();
    }

    private bool CheckJSON()
    {
        if (oneTime == true) { return false; }
        if(targetSaveJSONList == null) { Debug.LogError("json scriptable object is null");
            return false; 
        }
        if (idInList == -1)
        {
            Debug.LogError("id list is not assigned , cant be saved");
            return false;
        }
        return true;
    }

    private void SaveData()
    {
        if (CheckJSON() == false) { return; }
        string jsonString = Read();
        targetSaveJSONList.SetString(jsonString, idInList);
    }
    private void LoadData()
    {
        if(CheckJSON() == false) { return; }
        Load(targetSaveJSONList.GetString(idInList));
    }
    //assign gismos to the spawn object 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea_width * 2, spawnArea_height * 2));
    }
}
