using UnityEngine;

public class GlobalNPCManager : MonoBehaviour
{
    public static GlobalNPCManager Instance;
    public float gameTime; // Simulated in-game time
    public float timeScale = 60f; // Real seconds per in-game hour

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        gameTime += Time.deltaTime * timeScale;
    }
}
