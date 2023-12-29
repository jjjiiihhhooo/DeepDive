using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerScript : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public UnityEvent _event;


    public int toothCount = 3; 

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
    public void OnMouseDown()
    {
        _event.Invoke();
    }

    public void Tooth()
    {
        if(toothCount > 0)
            toothCount--;   
    }

    public void Toutle()
    {
        TurtleMovemont turtleMovemont = GetComponent<TurtleMovemont>();
        turtleMovemont.SpeedUpTurtle();
    }
}
