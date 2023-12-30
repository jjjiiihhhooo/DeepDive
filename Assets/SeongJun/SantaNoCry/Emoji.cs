using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Emoji : MonoBehaviour
{
    [SerializeField] Chimney chimney;
    
    public void Start()
    {
        chimney = transform.parent.GetComponent<Chimney>(); 
    }

    public void OnMouseDrag()
    {
        if (!chimney.isSmile)
        {
            float distance = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = objPos;

            if(Mathf.Abs(transform.localPosition.x) > 0.15f || Mathf.Abs(transform.localPosition.y) > 0.15f)
            {
                chimney.isSmile = true;
                Destroy(gameObject);
            }
        }
    }
}
