using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("SpawnX"))
        {
            GameObject player = GameObject.FindWithTag("Player"); // Ensure your Player GameObject has the "Player" tag.
            if (player != null)
            {
                Vector3 spawnPosition = new Vector3(
                    PlayerPrefs.GetFloat("SpawnX"),
                    PlayerPrefs.GetFloat("SpawnY"),
                    PlayerPrefs.GetFloat("SpawnZ")
                );
                player.transform.position = spawnPosition;
            }
        }
    }
}
