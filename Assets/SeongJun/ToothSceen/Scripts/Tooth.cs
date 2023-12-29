using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    public Action OnClick;
    [field: SerializeField] public float count { get; set; }


    public void Start()
    {
        OnClick += OnClicked;
    }

    void OnClicked()
    {
        if(count > 0)
        {
            count--;
            print(count);
        }
    }

    private void OnMouseDown()
    {
        
    }
}
