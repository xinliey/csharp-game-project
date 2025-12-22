using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;

    private void Awake()
    {
        instance = this; 
    }
    string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void SwitchScene(string to)
    {
        SceneManager.LoadScene(to, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;
    }

   public void Respawn(Vector3 respawnPosition, string respawnScene)
    {
        Debug.Log("this is inside respawn of the gamescenemanager");

        SceneManager.LoadScene(respawnScene, LoadSceneMode.Single);
        StartCoroutine(PositionPlayer(respawnPosition));
    }
  
    private IEnumerator PositionPlayer(Vector3 position)
    {
        yield return new WaitForEndOfFrame(); // Wait for the scene to load
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = position;
        }
    }

}
