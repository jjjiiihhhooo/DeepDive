using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunCamara : MonoBehaviour
{
    public GameObject player;
    public Vector3 fixedV3;

    public void Update()
    {
        transform.position = player.transform.position + fixedV3;
    }
}
