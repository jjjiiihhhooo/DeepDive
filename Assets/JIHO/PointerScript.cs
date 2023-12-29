using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum Type
{
    클릭, 드래그
}


public class PointerScript : MonoBehaviour
{
    public UnityEvent _event;

    private Vector3 startPos = Vector3.zero;

    public float test;
    public float clearCount;

    public Type myType;

    public void OnMouseDrag()
    {
        if (myType != Type.드래그) return;

        _event.Invoke();
    }

    public void OnMouseDown()
    {
        if (myType != Type.클릭) return;
        _event.Invoke();
    }

    public void Rotation()
    {
        if (startPos == Vector3.zero) startPos = Input.mousePosition;

        Vector3 tempPos = Input.mousePosition;

        if (tempPos.x - startPos.x >= 0)
        {
            test += tempPos.x - startPos.x;
            transform.Rotate(new Vector3(0f, transform.rotation.y - (tempPos.x - startPos.x) * 0.3f, 0f));
            transform.position += Vector3.up * 0.003f;
        }

        startPos = tempPos;

        if (test >= clearCount)
        {
            Debug.Log("GameClear");
        }
    }

}
