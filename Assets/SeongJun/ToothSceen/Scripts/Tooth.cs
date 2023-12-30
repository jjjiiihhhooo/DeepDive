using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    [SerializeField] AudioClip tooh;


    public BoxCollider boxCollider;

    Rigidbody body;
    int Hp = 3;


    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;

    [Header("Settings")]
    [Range(0f, 2f)]
    public float _time = 0.2f;
    [Range(0f, 2f)]
    public float _distance = 0.1f;
    [Range(0f, 0.1f)]
    public float _delayBetweenShakes = 0f;


    private bool isTouchable = true;


    [SerializeField] Teeth teeth;

    public void Start()
    {
        body = GetComponent<Rigidbody>();
        boxCollider = transform.GetChild(0).GetComponent<BoxCollider>();
        var meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        if (this.enabled == false)
        {
            meshRenderer.material.color = Color.white;
            isTouchable = false;
        }
        else
        {
           
            meshRenderer.material.color = Color.black;
        }
    }

    public void OnMouseDown()
    {     
        if(isTouchable)
        {
            if (Hp > 0)
            {
                StartCoroutine(Shake());
                Hp--;
                
            }
            if (Hp <= 0)
            {
                isTouchable = false;
                body.useGravity = true;
                print("ºüÁø´Ù");
                Dead(); 
                
            }
        }
       
    }

    public void Dead()
    {

        Manager.Instance.soundManager.Play(tooh, false);
        teeth.RemoveTooth();
    }

  

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnValidate()
    {
        if (_delayBetweenShakes > _time)
            _delayBetweenShakes = _time;
    }

    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    public void NoChose()
    {
        var tooh = this.GetComponent<Tooth>();
        tooh.enabled = false;
        boxCollider = transform.GetChild(0).GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    private IEnumerator Shake()
    {
        _timer = 0f;
        isTouchable = false;
        while (_timer < _time)
        {
            _timer += Time.deltaTime;

            _randomPos = _startPos + (Random.insideUnitSphere * _distance);

            transform.position = _randomPos;

            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }
        if(Hp>0)
        isTouchable = true;
        transform.position = _startPos;
    }
}

