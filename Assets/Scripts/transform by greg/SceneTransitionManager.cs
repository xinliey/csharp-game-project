using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    
    private void Awake()
    {
         Instance = this;
      
    }
    
    //private bool sceneTransitionInProgress = false; // Prevent multiple triggers
    [SerializeField] ScreenFader screenFader;
    [SerializeField] CameraConfiner cameraConfiner;
    string currentTime;
    public string GetHomeTime = "19";
    private string currentScene; // Current active scene name
    AsyncOperation unload; //like pouring cup of water in between two scene to avoid game lag
    AsyncOperation load;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void InitSwitchScene(string to, Vector3 targetPosition) //call this when fading screen is require
    {
        StartCoroutine(Transition(to, targetPosition));
    }


    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenFader.Tint();
        yield return new WaitForSeconds(1f / screenFader.speed + 0.1f);
        SwitchScene(to, targetPosition);

        while (load != null & unload != null)
        {
            if (load.isDone) { load = null; }
            if (unload.isDone) { unload = null; }

            yield return new WaitForSeconds(0.2f);
        }

        cameraConfiner.UpdateBounds();
        yield return new WaitForEndOfFrame();

        screenFader.UnTint();
        currentTime = GameManager.instance.gameTime.GetCurrentTime();
        string result = currentTime.Substring(0, 2);
        //Debug.Log($"get home time is 19 current time is {result}");
        if (result == GetHomeTime)
        {
            Debug.Log("result is equal to get home time");
            SystemMessengerBox.Instance.ShowMessage("it's getting quite late");
        }
    }
    
    public void SwitchScene(string to,Vector3 targetPosition) //changing scene without fading screen
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;

        Transform playerTransform = GameManager.instance.player.transform;
        //keep the camera tracking character even after changing scene
        CinemachineBrain currentCamera =  Camera.main.GetComponent<CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
            playerTransform,
            targetPosition - playerTransform.position);
        playerTransform.position = new Vector3(
            targetPosition.x,
            targetPosition.y,
            playerTransform.position.z); ;
    }
    public void Respawn(Vector3 respawnPosition, string respawnScene)
    {
        SystemMessengerBox.Instance.ShowMessage("I'm so tired, I have no more strength");
        Debug.Log($"Respawning player at position {respawnPosition} in scene {respawnScene}");
        InitSwitchScene(respawnScene, respawnPosition);
        GameManager.instance.gameTime.SkipToMorning();


    }
    /*public void InitSwitchScene(string sceneName, Vector3 spawnPosition)
    {
        StartCoroutine(Transition(sceneName, spawnPosition));
    }
   IEnumerator Transition(string sceneName, Vector3 spawnPosition)
    {
        screenFader.Tint();
        yield return new WaitForSeconds(1f*screenFader.speed); //1sec * speed of tint
        TransitionToScene(sceneName,spawnPosition);
        yield return new WaitForEndOfFrame();
        screenFader.UnTint();
    }
    public void TransitionToScene(string sceneName, Vector3 spawnPosition)
    {
        //check screenfader if it's nul
        //Debug.Log($"Checking ScreenFader.Instance: {ScreenFader.Instance}");

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
        currentScene = sceneName;
        Transform playerTransform = GameManager.instance.player.transform;
        playerTransform.position = new Vector3(spawnPosition.x,
            spawnPosition.y,
            playerTransform.position.z);
    }
    
    private IEnumerator HandleSceneTransition(string newScene)
    {
        // Load the new scene additively
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        yield return new WaitUntil(() => loadOperation.isDone);
        Camera newCamera = Camera.main;
        if (newCamera != null)
        {
            newCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No camera found in the new scene. Keeping Essential scene active.");
        }

        // Unload the old gameplay scene (but NOT Essential)
        if (currentScene != "Essential")
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
            yield return new WaitUntil(() => unloadOperation.isDone);
        }
        // Update the current scene
        currentScene = newScene;
        sceneTransitionInProgress = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != currentScene) return; // Ignore if it's not the intended scene
        GameObject player = GameObject.FindWithTag("Player");
        
        if (player != null)
        {
            player.transform.position = playerSpawnPosition;
            
            //Debug.Log($"Player repositioned to {playerSpawnPosition} in scene {scene.name}");

        }
        else
        {
            Debug.LogWarning("Player not found in the loaded scene. Ensure the player object exists and is tagged correctly.");
        }

        // Assign gameTime if applicable
        gameTime = FindObjectOfType<GameTime>();
        if (gameTime != null)
        {
           // Debug.Log("GameTime successfully assigned after scene load.");
        }
        else
        {
            Debug.LogError("GameTime not found after scene load.");
        }
    }
   


    }*/

}
