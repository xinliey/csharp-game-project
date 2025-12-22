using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class TimeAgent : MonoBehaviour
{
    //tell agent how to react , read the time , making gametime as a paramater in timeagent
    public Action<GameTime> onTimeTick;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        GameManager.instance.gameTime.Subscribe(this); 
    }
    public void Invoke(GameTime gameTime)
    {
        //invoke by time controller to tell agent that time has passed 
        //timeagent will know how to react when time passed
        onTimeTick?.Invoke(gameTime);
    }
    private void OnDestroy()
    {
        GameManager.instance.gameTime.Unsubscribe(this);
    }
}
