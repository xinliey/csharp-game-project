using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //public GameTime gameTime;
    public Text timeText;
    public Text dateText;
    public static GameUI Instance { get; private set; }
    Vector3 respawnPointPosition = new Vector3(-4, 17, 0);

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Destroy the duplicate
            return;
        }

        Instance = this;  // Set this instance as the singleton
        DontDestroyOnLoad(gameObject);  // Optional: Persist across scenes
    }


    void Update()
    {
        if (GameTime.Instance != null)
        {
            // Update time display
            timeText.text = "Time: " + GameTime.Instance.GetCurrentTime();
            if (timeText.text == "Time: 20:00")
            {
               // Debug.Log("relocating player back home");
                SceneTransitionManager.Instance.Respawn(respawnPointPosition,"mc_house");

            }
            // Update date display
            dateText.text = GameTime.Instance.GetCurrentDate();

            


        }
    }
}
