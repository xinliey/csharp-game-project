using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class SpawnedObject : MonoBehaviour
{
    //managing persistent item 
    [Serializable]
    public class SaveSpawnedObjectData
    {
        public int objectId;
        public Vector3 worldPosition;

        public SaveSpawnedObjectData(int id , Vector3 worldPosition)
        {
            this.objectId = id;
            this.worldPosition = worldPosition;
        }
    }
    //get the id of the item which is the prefab
    public int objId;
    //if the item being destroy , remove it from the list
    public void SpawnedObjectDestroyed()
    {
        transform.parent.GetComponent<ObjectSpawner>().SpawnedObjectDestroyed(this);
    }
}
