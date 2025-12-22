using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHouse : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    public Vector3 spawnPosition; // Player spawn position in the next scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player triggering
        {
            Debug.Log("you enter the exit area" );
            //SceneTransitionManager.Instance.InitSwitchScene(sceneToLoad, spawnPosition);
        }
    }
}
