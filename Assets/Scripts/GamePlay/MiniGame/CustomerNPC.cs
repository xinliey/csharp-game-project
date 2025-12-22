using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CustomerData
{
    public bool Ordered;
    public int OrderedTime;
   
}
[Serializable]
public class OrderMenu
{
    public string dialogue;
    public Sprite dessert;
    public string DessertName;
}
[Serializable]
public class Schedule
{

    public Vector3 transform; // Acts as a destination
    public bool arrivedTable;// Scheduled time to go (format: "HH:mm")
}
[CreateAssetMenu(menuName = "Data/Minigame/PartTimeCustomer")]
public class CustomerNPC : ScriptableObject
{
    public string Name = "customer";
    public CustomerData data;
    //public List<Transform> transform
    public List<OrderMenu> OrderDialogue;
    public List<Schedule> movement;
}
