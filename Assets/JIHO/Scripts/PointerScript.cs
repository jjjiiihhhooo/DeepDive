using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum Type
{
    Click, Drag
}


public class PointerScript : MonoBehaviour
{
    public UnityEvent _event;
    public UnityEvent _clearEvent;
    public UnityEvent _overEvent;

    public Animator animator;

    public int toothCount = 3;

    public GameObject beforeCoke;
    public GameObject afterCoke;

    private Vector3 startPos = Vector3.zero;

    public float test;
    public float clearCount;

    public bool isHairTarget;

    public Type myType;

    private void Awake()
    {
        if (_clearEvent != null) Manager.Instance.sceneDatas[Manager.Instance.sceneIndex]._clearEvent = _clearEvent; 
        if(_overEvent != null) Manager.Instance.sceneDatas[Manager.Instance.sceneIndex]._overEvent = _overEvent;
    }

    public void OnMouseDrag()
    {
        if (myType != Type.Drag || !Manager.Instance.isStart) return;

        _event.Invoke();
    }

    public void OnMouseDown()
    {
        if (myType != Type.Click || !Manager.Instance.isStart) return;
        _event.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name == "DiveMan")
            {
                DiveManClear();
            }
        }
        else if(other.tag == "Platform")
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "DiveMan")
            {
                DiveManOver();
            }
        }
    }

    ////////////////////////////////////////
    public void Tooth()
    {
        if(toothCount > 0)
            toothCount--;   
    }

    public void ToothClear()
    {

    }

    public void ToothClearEvent()
    {

    }

    public void ToothOver()
    {

    }

    public void ToothOverEvent()
    {

    }
    ////////////////////////////////////////
    public void Toutle()
    {
        TurtleMovemont turtleMovemont = GetComponent<TurtleMovemont>();
        turtleMovemont.SpeedUpTurtle();
    }

    public void ToutleClear()
    {

    }

    public void ToutleClearEvent()
    {

    }

    public void ToutleOver()
    {

    }

    public void ToutleOverEvent()
    {

    }

    ////////////////////////////////////////
    public void Coke()
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
            CokeClear();
        }
    }

    public void CokeClear()
    {
        Manager.Instance.GameClear();
    }

    public void CokeClearEvent()
    {
        Debug.Log("CokeClear");
        beforeCoke.SetActive(false);
        afterCoke.SetActive(true);
    }

    public void CokeOver()
    {
        Manager.Instance.RoundOver();
    }

    public void CokeOverEvent()
    {

    }
    ////////////////////////////////////////

    public void DiveMan()
    {
        if (animator == null) animator = GetComponent<Animator>();

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dive"))
        {
            animator.SetTrigger("Dive");
        }

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.z = 0.06f;
        transform.position = objPos;
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.zero;
    }

    public void DiveManClear()
    {
        Manager.Instance.GameClear();
    }

    public void DiveManClearEvent()
    {
        Debug.Log("DiveClear");
        animator.SetTrigger("Clear");
    }

    public void DiveManOver()
    {
        Manager.Instance.RoundOver();
    }

    public void DiveManOverEvent()
    {
        animator.SetTrigger("Fail");
    }
    ////////////////////////////////////////
    public void HairRemove()
    {
        if(animator == null) animator = GetComponent<Animator>();

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Pull"))
        {
            animator.SetTrigger("Pull");
        }

        if (startPos == Vector3.zero) startPos = Input.mousePosition;
        Vector3 curPos = Input.mousePosition;

        if (Vector3.Distance(startPos, curPos) > 500f) 
        {
            if (isHairTarget) HairRemoveClear();
            else HairRemoveOver();
        }

    }

    public void HairRemoveClear()
    {
        Manager.Instance.GameClear();
    }

    public void HairRemoveClearEvent()
    {
        Debug.Log("HairClear");
        animator.SetTrigger("Clear");
    }

    public void HairRemoveOver()
    {
        Manager.Instance.RoundOver();
    }

    public void HairRemoveOverEvent()
    {

    }
    ////////////////////////////////////////
}
