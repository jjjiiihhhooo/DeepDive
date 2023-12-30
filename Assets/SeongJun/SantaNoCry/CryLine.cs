using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryLine : MonoBehaviour
{
    [SerializeField] Santa santa;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Chimney>(out Chimney com))
        {
            com.isNoCry = true;
            santa.DropGift();

        }
    }
}
