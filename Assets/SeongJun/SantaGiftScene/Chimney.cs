using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimney : MonoBehaviour
{
    public  Santa santa;
    public bool isSmile = false;
    public bool Finsh = false;

    [SerializeField] AudioClip SmileClip;
    [SerializeField] AudioClip CryClip;

    public void Start()
    {
        if (isSmile)
        {

        }
        else
        {

        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(!Finsh)
        {
            if (other.gameObject.GetComponent<Gift>())
            {
                if (isSmile)
                {
                    Finsh = true;

                    santa.CheckClear();
                }
                else
                {
                    Manager.Instance.GameOver();
                }
            }
        }
      
    }
}
