using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] float UpSpeed;
    [SerializeField] GameObject Bag;
    [SerializeField] List<GameObject> GiftList;

    [SerializeField] List<Chimney> ChimneyList;

    float rotSpeed = 200f;
    public void Awake()
    {       
       for(var i =0; i< ChimneyList.Count; i++)
            ChimneyList[i].santa = this;
        var smileCount = 3;
        while(smileCount > 0)
        {
            var chi = ChimneyList[Random.Range(0, ChimneyList.Count)];
            if (!chi.isSmile)
            {
                chi.isSmile = true;
                smileCount--;
            }
        }
    }
    void Start()
    {
        body = GetComponent<Rigidbody>();
        
        StartCoroutine(MoveStart());
    }
    public void Update()
    {
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime,0));
    }

    public void OnMouseDown()
    {
        Instantiate(GiftList[Random.Range(0, GiftList.Count)], Bag.transform.position, Quaternion.identity);
    }

    IEnumerator MoveStart()
    {
        yield return new WaitForSeconds(4f);
        body.AddForce(-UpSpeed, 0, 0);
        
        yield return null;
    }
    public void CheckClear()
    {
        if(ChimneyList.Find(x => x.isSmile && !x.Finsh))
        {
            
            return;
        }
        body.AddForce(UpSpeed, 0, 0);
        Manager.Instance.GameClear();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Floor"))
            Manager.Instance.GameOver();
    }

}
