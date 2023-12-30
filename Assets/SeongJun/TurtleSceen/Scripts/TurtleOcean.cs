using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleOcean : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PointerScript>(out PointerScript com))
        {
            com.ToutleClear();
        }
    }
}
