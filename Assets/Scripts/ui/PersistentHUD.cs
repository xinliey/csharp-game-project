using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PersistentHUD : MonoBehaviour
{
    private static PersistentHUD instance;
    public Text timeText;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Update()
    {
        if (GameTime.Instance != null)
        {
            timeText.text = GameTime.Instance.GetCurrentTime();
        }
    }
}
