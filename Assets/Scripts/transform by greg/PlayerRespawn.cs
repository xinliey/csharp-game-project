using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Vector3 respawnPointPosition = new Vector3(-6, 15, 0); // Default respawn position
    [SerializeField] string respawnPointScene = "mc_house";

    internal void StartRespawn()
    {
        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.Respawn(respawnPointPosition, respawnPointScene);
        }
        else
        {
            Debug.LogError("SceneTransitionManager instance is null! Respawn cannot proceed.");
        }
    }
}
