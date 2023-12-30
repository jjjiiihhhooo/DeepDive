using System.Collections.Generic;
using UnityEngine;

public class Teeth : MonoBehaviour
{
    [SerializeField] PointerScript pointerScript;

    [SerializeField] int Max;
    List<Tooth> ToothList;

    [SerializeField] List<DurltyTooth> durltyTeethList;


    public void Awake()
    {


        if (durltyTeethList.Count <= 0)
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
        else
        {
            for (var i = 0; i < durltyTeethList.Count; i++)
            {
                if (Random.Range(1, 10) >= 5)
                {
                    Max++;
                }
                else
                {
                    durltyTeethList[i].NoChose();
                }
            }

        }
    }
    public void RemoveTooth()
    {
        Max--;
        if (Max <= 0)
            pointerScript.ToothClear();
    }
}
