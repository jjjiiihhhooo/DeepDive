using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    Santa santa;

    public void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 300f);
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.TryGetComponent<Santa>(out Santa com))
        {
            santa = com;
        }

    }
}
