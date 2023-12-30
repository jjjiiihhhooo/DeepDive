using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Move : MonoBehaviour
{
    

    void Update()
    {
        if (!Manager.Instance.isStart) return;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f * Time.deltaTime);
    }
}
