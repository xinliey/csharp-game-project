using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
public enum TransitionType
{
    Warp, 
    Scene 
}
public class Transition : MonoBehaviour
{
    [SerializeField] TransitionType transitionType;
    [SerializeField] string sceneNameToTransition;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] TilemapRenderer ShowLayer;

    public bool ShowHidingLayer; //some layer is hidden from hide object above layer script
    Transform destination; 
    void Start()
    {
        
        destination = transform.GetChild(1);
        if (ShowHidingLayer == false)
        {
            ShowLayer = null;
        }   
    }
    public void DoorTransition(Transform toTransition)
    {
        //Debug.Log($"transitioning to{targetPosition}");
        
        InitiateTransition(toTransition);
    }

    internal void  InitiateTransition(Transform toTransition)
    { 
        switch (transitionType)
        {
            case TransitionType.Warp:
                Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition,
                    destination.position-toTransition.position);
                if (ShowHidingLayer == true)
                {
                    ShowLayer.gameObject.SetActive(true);
                }
               toTransition.position = new Vector3(
                destination.position.x,
                destination.position.y,
                toTransition.position.z

                
                ); 

            break;
            case TransitionType.Scene:
                //Debug.Log("transitioning to the scene");
                SceneTransitionManager.Instance.InitSwitchScene(sceneNameToTransition,targetPosition);
              
                break;
        }
    }
}
