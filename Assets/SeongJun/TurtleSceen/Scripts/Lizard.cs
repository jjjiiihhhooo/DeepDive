using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public float UpSpeed;
    private Rigidbody TurtleRigidbody;

    void Start()
    {
        TurtleRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(MoveStart());
    }

    public void Update()
    {
        
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PointerScript>(out PointerScript com))
        {
            com.ToutleOver();
        }
    }

    IEnumerator MoveStart()
    {
        yield return new  WaitForSeconds(3.5f);
        while(this.gameObject && Manager.Instance.isStart)
        {
            TurtleRigidbody.AddForce(0, 0, UpSpeed);
            yield return null;
        }
        
        yield return null;
    }
}
