using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject pickUpItemPrefab;

    public void SpawnItem(Vector3 position, Item item, int count)
    {
        // Corrected the typo from pickUpItemPrefap to pickUpItemPrefab
        GameObject o = Instantiate(pickUpItemPrefab, position, Quaternion.identity);
        o.GetComponent<PickUpItem>().Set(item, count);
    }
}
