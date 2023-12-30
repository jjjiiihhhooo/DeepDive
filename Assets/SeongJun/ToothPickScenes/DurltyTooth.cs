using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurltyTooth : MonoBehaviour
{
    public void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = objPos;

        if (Mathf.Abs(transform.localPosition.x) > 0.1f || Mathf.Abs(transform.localPosition.y) > 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
