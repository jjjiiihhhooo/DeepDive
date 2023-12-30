using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Santa : MonoBehaviour
{
    [SerializeField] AudioClip DropSFX;

    public Rigidbody body;
    public float UpSpeed;
    [SerializeField] GameObject Bag;
    [SerializeField] List<GameObject> GiftList;

    [SerializeField] List<Chimney> ChimneyList;

    float rotSpeed = 1000f;
    bool isMove = false;
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
        if (isMove && transform.childCount < 4)
            DropGift();
    }

    IEnumerator MoveStart()
    {
        yield return new WaitForSeconds(4f);
        body.AddForce(-UpSpeed, 0, 0);
        isMove = true;
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
        {
            var box = GetComponent<BoxCollider>();  
            box.enabled = false;    
            if(transform.childCount < 4)
            {
                Manager.Instance.RoundOver();
                body.AddForce(0, 0, 0);
            }
            else
            {
                Manager.Instance.GameClear();
                body.AddForce(0, 0, 0);
            }
            print("¾Æ¾Æ");
        }
            
    }

    public void GameOver()
    {
        if(!Manager.Instance.isOver)
        {
            Manager.Instance.RoundOver();
            body.AddForce(UpSpeed, 2000f, 0);
        }
        
       // yield return new WaitForSeconds(2f);
       // yield return null;  
    }


    public void DropGift()
    {

        Instantiate(GiftList[Random.Range(0, GiftList.Count)], Bag.transform.position, Quaternion.identity);
    }
}
