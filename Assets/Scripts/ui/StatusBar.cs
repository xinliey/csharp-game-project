using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Text text;
    //using slider on image to convert it into decrease/increase able icon
    [SerializeField] Slider bar;

    public void Set(int curr, int max)
    {
        //get value and send them to slider 
        bar.maxValue = max;
        bar.value = curr;

        text.text = curr.ToString() + "/" + max.ToString();
    }
}
