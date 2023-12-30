using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piza : MonoBehaviour
{
    [SerializeField] List<Pineapple> pineapples;
    int Max;
    public void Start()
    {
        for (var i = 0; i < pineapples.Count; i++)
        {
            if (Random.Range(1, 10) >= 5)
            {
                Max++;
                pineapples[i].remove += RemovePineapple;
            }
            else
            {
                pineapples[i].NoChose();
            }
        }
    }

    public void RemovePineapple()
    {
        Max--;
        if (Max <= 0)
            Manager.Instance.GameClear(); //.ToothClear();
    }
}
