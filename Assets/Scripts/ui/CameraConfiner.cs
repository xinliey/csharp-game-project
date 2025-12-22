using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfiner : MonoBehaviour
{
    [SerializeField] CinemachineConfiner confiner; 

    void Start()
    {
        UpdateBounds();
    }
    public void UpdateBounds() //find cofiner on this scene and store it into the confiner machine
    {
        GameObject go = GameObject.Find("confiner");
        if(go == null)//if confiner is not found , proceed without confiner
        {
            confiner.m_BoundingShape2D = null;
            return;
        }
        Collider2D bounds = go.GetComponent<Collider2D>();
        confiner.m_BoundingShape2D = bounds;
    }
}
