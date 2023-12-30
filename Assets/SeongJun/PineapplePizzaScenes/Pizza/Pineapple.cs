using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pineapple : MonoBehaviour
{
    public Action remove;
    public void OnMouseDown()
    {
        remove?.Invoke();
        Destroy(gameObject);
    }

    public void NoChose()
    {
        gameObject.SetActive(false);    
    }
}
