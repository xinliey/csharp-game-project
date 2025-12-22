using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveme : MonoBehaviour
{
    private static bool isCanvasExist = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isCanvasExist)
        {
            Destroy(gameObject); // Destroy the duplicate canvas if it already exists
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Keep the canvas between scenes
            isCanvasExist = true; // Mark that the canvas exists
        }
    }
}