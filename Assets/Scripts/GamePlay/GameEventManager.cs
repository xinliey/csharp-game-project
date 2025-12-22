using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void TriggerDayStartEvents()
    {
        Debug.Log("Day start events triggered.");
        // Implement events like resetting NPC schedules or spawning new items
    }
}

