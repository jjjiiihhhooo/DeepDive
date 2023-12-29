using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image timer_fill;


    public void UpdateImage(float time, float maxTime)
    {
        timer_fill.fillAmount = time / maxTime;
    }

    public void RoundClear()
    {

    }

    public void RoundStart()
    {

    }

    public void RoundEnd()
    {

    }
}
