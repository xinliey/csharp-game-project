using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScheduleTest : MonoBehaviour
{
    public List<SchedulePoint> dailySchedule; // Define waypoints with times
    private int currentPointIndex = 0;

    private NPCMovementTest npcMovement;
    private SpriteRenderer spriteRenderer;
    private bool isWaitingForNextPoint = false;

    private void Start()
    {
        npcMovement = GetComponent<NPCMovementTest>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Reference to the SpriteRenderer
        SetNPCInactive();  // Initially deactivate the NPC (make invisible, stop movement)
    }

    private void Update()
    {
        // Continuously check for the current game time
        if (!isWaitingForNextPoint && currentPointIndex < dailySchedule.Count)
        {
            CheckSchedule();
        }
        else if (currentPointIndex >= dailySchedule.Count)
        {
            SetNPCInactive();  // Deactivate the NPC once the schedule is finished
        }
    }

    private void CheckSchedule()
    {
        SchedulePoint point = dailySchedule[currentPointIndex];

        // Convert `GameTime` to minutes since midnight
        string[] currentTime = GameTime.Instance.GetCurrentTime().Split(':');
        int currentHour = int.Parse(currentTime[0]);
        int currentMinute = int.Parse(currentTime[1]);
        float currentGameTimeInMinutes = currentHour * 60 + currentMinute;

        // Debug log to check the game time and compare it with the scheduled time
       // Debug.Log($"Current Game Time: {currentGameTimeInMinutes}, Scheduled Time: {point.time}");

        if (currentGameTimeInMinutes >= point.time)
        {
            ActivateNPC(); // Activate the NPC at the scheduled time
            MoveToPoint(point);
            currentPointIndex++;
        }
    }

    private void SetNPCInactive()
    {
        // Deactivate NPC by making it invisible and stopping movement
        spriteRenderer.enabled = false; // Hide the NPC sprite
        npcMovement.ToggleMovement(false); // Stop the NPC movement (use ToggleMovement method from NPCMovementTest)
    }

    private void ActivateNPC()
    {
        // Activate the NPC at the scheduled time (only once)
        if (!spriteRenderer.enabled)
        {
           // Debug.Log("Activating NPC");
            spriteRenderer.enabled = true; // Make NPC visible
            npcMovement.ToggleMovement(true); // Allow NPC movement
        }
    }
    public SchedulePoint GetCurrentPoint()
    {
        if (currentPointIndex < dailySchedule.Count)
        {
            return dailySchedule[currentPointIndex];
        }
        else
        {
            Debug.LogWarning("No more schedule points available.");
            return null; // Return null or a default point if no schedule points are left
        }
    }

    private void MoveToPoint(SchedulePoint point)
    {
        isWaitingForNextPoint = true;

        // Log the current position and target position to compare
       // Debug.Log($"Moving NPC to target position: {point.position}");
       // Debug.Log($"Current NPC Position: {npcMovement.transform.position}");

        npcMovement.SetTarget(point.position); // Set the target destination

        // Continuously check if NPC is reaching the target
        StartCoroutine(TrackMovement(point.position));
    }

    private IEnumerator TrackMovement(Vector3 targetPosition)
    {
        // Debug loop to check NPC's movement toward the target position
        while (Vector3.Distance(npcMovement.transform.position, targetPosition) > 0.1f) // Allow a small tolerance
        {
            //Debug.Log($"NPC current position: {npcMovement.transform.position}, Target position: {targetPosition}");
            yield return null; // Wait for the next frame
        }

        // Once the NPC reaches the target, we log that it has reached the destination
       // Debug.Log("NPC reached the destination: " + targetPosition);

        AllowNextPoint(); // Allow the next point to be checked after reaching the target
    }

    private void AllowNextPoint()
    {
        isWaitingForNextPoint = false;
    }
}

[System.Serializable]
public class SchedulePoint
{
    public Vector3 position;
    public float time; // Time in minutes since midnight
}
