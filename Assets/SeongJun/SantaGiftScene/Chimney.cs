using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chimney : MonoBehaviour
{
    public  Santa santa;
    public bool isSmile = false;
    public bool Finsh = false;
    public bool isNoCry = false;

    [SerializeField] AudioClip SmileClip;
    [SerializeField] AudioClip CryClip;

    [SerializeField] Image Smile;
    MeshRenderer mesh;
    [SerializeField] List<Material> EmoList; 

    public void Start()
    {
        mesh = transform.GetChild(0).GetComponent<MeshRenderer>();

        if (isSmile)
        {
            mesh.material = EmoList[0];
        }
        else
        {
            mesh.material = EmoList[1];
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
                    if(!isNoCry)
                    {
                        Finsh = true;
                        santa.CheckClear();
                    }
                }
                else
                {
                    santa.GameOver();
                }
            }
        }
      
    }

  
}
