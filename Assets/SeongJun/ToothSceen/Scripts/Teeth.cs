using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teeth : MonoBehaviour
{
    [SerializeField] PointerScript pointerScript;

    [SerializeField] int Max;
    List<Tooth> ToothList;


    public void Awake()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Tooth>())
            {
                if (Random.Range(1, 10) >= 5)
                {
                    Max++;
                }
                else
                {
                    transform.GetChild(i).GetComponent<Tooth>().NoChose();  
                }

            }

        }
    }
    public void RemoveTooth()
    {
        Max--;
        if(Max <= 0)
            pointerScript.ToothClear();
    }
}
