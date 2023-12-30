using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class DurltyTooth : MonoBehaviour
{
    [SerializeField] AudioClip Durlty;
    [SerializeField] Teeth teeth;
    public void OnMouseDrag()
    {
        float distance = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = objPos;
        if (Mathf.Abs(transform.localPosition.x) > 0.1f || Mathf.Abs(transform.localPosition.y) > 0.1f)
        {
            teeth.RemoveTooth();    
            Destroy(gameObject);
        }
    }

    public void NoChose()
    {
        this.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        Manager.Instance.soundManager.Play(Durlty, false);
    }
}
