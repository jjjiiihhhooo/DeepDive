using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Type
{
    Click, Drag
}


public class PointerScript : MonoBehaviour
{
    public UnityEvent _event;
    public UnityEvent _clearEvent;
    public UnityEvent _overEvent;

    public Animator animator;

    public int toothCount = 3;

    public GameObject beforeCoke;
    public GameObject afterCoke;
    public GameObject fakeText;
    public GameObject bulbLight;
    public GameObject[] plugs;
    public GameObject[] hairs;
    public GameObject[] jengas;
    public GameObject ppt_j;
    public GameObject ppt_back;
    public ParticleSystem[] cokeEffect;
    public ParticleSystem coolManEffect;

    private Vector3 startPos = Vector3.zero;

    public float test;
    public float clearCount;

    public bool isHairTarget;

    public Type myType;

    private void Awake()
    {
        if (_clearEvent != null) Manager.Instance.sceneDatas[Manager.Instance.sceneIndex]._clearEvent = _clearEvent; 
        if(_overEvent != null) Manager.Instance.sceneDatas[Manager.Instance.sceneIndex]._overEvent = _overEvent;

        if(coolManEffect != null) coolManEffect.Play();

    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "CrossCouple" && Manager.Instance.isStart)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f * Time.deltaTime);
        }
    }

    public void OnMouseDrag()
    {
        if (myType != Type.Drag || !Manager.Instance.isStart) return;

        _event.Invoke();
    }

    public void OnMouseDown()
    {
        if (myType != Type.Click || !Manager.Instance.isStart) return;
        _event.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name == "DiveMan" || scene.name == "CoolMan")
            {
                Debug.Log("TriggerMan");
                Manager.Instance.soundManager.Play(Manager.Instance.soundManager.audioDictionary["DiveWater"], false);
                DiveManClear();
            }
        }
        else if(other.tag == "Platform")
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "DiveMan" || scene.name == "CoolMan")
            {
                DiveManOver();
            }
        }
        else if(other.tag == "Couple")
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "CrossCouple")
            {
                CrossCoupleOver();
            }
        }
        else if(other.tag == "Player")
        {
            RedCarpetOver();
        }
    }

    ////////////////////////////////////////
    public void Tooth()
    {
        if(toothCount > 0)
            toothCount--;   
    }

    public void ToothClear()
    {
        Manager.Instance.GameClear();
    }

    public void ToothClearEvent()
    {

    }

    public void ToothOver()
    {
        Manager.Instance.RoundOver();
    }

    public void ToothOverEvent()
    {

    }
    ////////////////////////////////////////
    public void Toutle()
    {
        TurtleMovemont turtleMovemont = GetComponent<TurtleMovemont>();
        turtleMovemont.SpeedUpTurtle();
    }

    public void ToutleClear()
    {
        Manager.Instance.GameClear();
    }

    public void ToutleClearEvent()
    {

    }

    public void ToutleOver()
    {
        Manager.Instance.RoundOver();
    }

    public void ToutleOverEvent()
    {

    }

    ////////////////////////////////////////
    public void Coke()
    {
        if (animator == null) animator = GetComponent<Animator>();

        if (!cokeEffect[0].gameObject.activeSelf)
        {
            cokeEffect[0].gameObject.SetActive(true);
            cokeEffect[1].gameObject.SetActive(true);
            cokeEffect[0].Play();
            cokeEffect[1].Play();
        }

        if (startPos == Vector3.zero) startPos = Input.mousePosition;

        Vector3 tempPos = Input.mousePosition;

        if (tempPos.x - startPos.x >= 0)
        {
            test += tempPos.x - startPos.x;
            transform.Rotate(new Vector3(0f, transform.rotation.y - (tempPos.x - startPos.x) * 0.3f, 0f));
            transform.position += Vector3.up * 0.006f;
        }

        startPos = tempPos;

        if (test >= clearCount)
        {
            CokeClear();
        }
    }

    public void CokeClear()
    {
        Manager.Instance.GameClear();
    }

    public void CokeClearEvent()
    {
        Manager.Instance.soundManager.Play(Manager.Instance.soundManager.audioDictionary["Coke"], false);
        cokeEffect[0].Stop();
        cokeEffect[1].Stop();
        animator.SetTrigger("Clear");
        cokeEffect[2].gameObject.SetActive(true);
        cokeEffect[2].Play();
        beforeCoke.SetActive(false);
        afterCoke.SetActive(true);
    }

    public void CokeOver()
    {
        Manager.Instance.RoundOver();
    }

    public void CokeOverEvent()
    {

    }
    ////////////////////////////////////////

    public void DiveMan()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dive"))
        {
            animator.applyRootMotion = true;
            animator.SetTrigger("Dive");
        }

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.z = 0.06f;
        transform.position = objPos;
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.zero;
    }

    public void DiveManClear()
    {
        Debug.Log("ClearMan");
        Manager.Instance.GameClear();
    }

    public void DiveManClearEvent()
    {
        if (coolManEffect != null) coolManEffect.Stop();
        animator.SetTrigger("Clear");
    }

    public void DiveManOver()
    {
        Manager.Instance.RoundOver();
    }

    public void DiveManOverEvent()
    {
        if (animator == null) animator = GetComponent<Animator>();
        animator.SetTrigger("Fail");
    }
    ////////////////////////////////////////
    public void HairRemove()
    {
        if (startPos == Vector3.zero) startPos = Input.mousePosition;

        Vector3 tempPos = Input.mousePosition;

        if (Vector3.Distance(startPos, tempPos) > 300f)
        {
            HairRemoveClear();
        }
        else if (Vector3.Distance(startPos, tempPos) > 100f && !hairs[1].activeSelf)
        {
            Debug.Log(hairs.Length);
            hairs[0].SetActive(false);
            hairs[1].SetActive(true);
            hairs[3].SetActive(false);
            hairs[4].SetActive(true);
        }
    }

    public void HairRemoveClear()
    {
        Manager.Instance.GameClear();
    }

    public void HairRemoveClearEvent()
    {
        Debug.Log("dd");

        hairs[1].SetActive(false);
        hairs[2].SetActive(true);
        hairs[4].SetActive(false);
        hairs[5].SetActive(true);
    }

    public void HairRemoveOver()
    {
        Manager.Instance.RoundOver();
    }

    public void HairRemoveOverEvent()
    {

    }
    ////////////////////////////////////////
    public void CrossCouple()
    {
        if (startPos == Vector3.zero) startPos = transform.position;

        if (animator == null) animator = GetComponent<Animator>();

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = new Vector3(objPos.x, transform.position.y, transform.position.z);

        if(Vector3.Distance(transform.position, startPos) > 4)
        {
            CrossCoupleClear();
        }
    }

    public void CrossCoupleClear()
    {
        Manager.Instance.GameClear();
    }

    public void CrossCoupleClearEvent()
    {

    }

    public void CrossCoupleOver()
    {
        Manager.Instance.RoundOver();
    }
    public void CrossCoupleOverEvent()
    {
        StartCoroutine(CrossCoupleOverCor());
    }

    private IEnumerator CrossCoupleOverCor()
    {
        float time = -2f;
        while(time < 2f)
        {
            time += 0.05f;
            transform.position = new Vector3(time, transform.position.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }

    }

    ////////////////////////////////////////
    public void FakeText()
    {
        if (startPos == Vector3.zero) startPos = transform.position;

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = new Vector3(objPos.x, transform.position.y, transform.position.z);

        if (Vector3.Distance(transform.position, startPos) > 8)
        {
            FakeTextClear();
        }
    }

    public void FakeTextClear()
    {
        Manager.Instance.GameClear();
    }

    public void FakeTextClearEvent()
    {
        fakeText.SetActive(false);
    }

    public void FakeTextOver()
    {
        Manager.Instance.RoundOver();
    }

    public void FakeTextOverEvent()
    {
        StartCoroutine(FakeTextOverCor());
    }

    private IEnumerator FakeTextOverCor()
    {
        float time = 0f;
        while (time < 12f)
        {
            time += 0.03f;
            transform.position = new Vector3(time, transform.position.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }
    ////////////////////////////////////////
    public void BulbRemove()
    {
        if (animator == null) animator = GetComponent<Animator>();

        
        if (startPos == Vector3.zero) startPos = Input.mousePosition;

        Vector3 tempPos = Input.mousePosition;

        if (tempPos.x - startPos.x >= 0)
        {
            test += tempPos.x - startPos.x;
            transform.Rotate(new Vector3(0f, transform.rotation.y - (tempPos.x - startPos.x) * 0.3f, 0f));
            transform.position += Vector3.up * 0.006f;
        }

        startPos = tempPos;

        if (test >= clearCount)
        {
            BulbRemoveClear();
        }
    }

    public void BulbRemoveClear()
    {
        Manager.Instance.GameClear();
    }

    public void BulbRemoveClearEvent()
    {
        animator.SetTrigger("Clear");
        StartCoroutine(BulbLightCor());
    }

    private IEnumerator BulbLightCor()
    {
        bulbLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        bulbLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        bulbLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        bulbLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        bulbLight.SetActive(true);
    }

    public void BulbRemoveOver()
    {
        Manager.Instance.RoundOver();
    }

    public void BulbRemoveOverEvent()
    {
        if (animator == null) animator = GetComponent<Animator>();
        animator.SetTrigger("Clear");
    }
    ////////////////////////////////////////
    public void RedCarpet()
    {
        if (startPos == Vector3.zero) startPos = transform.position;

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = new Vector3(objPos.x, transform.position.y, transform.position.z);

        if (Vector3.Distance(transform.position, startPos) > 4)
        {
            RedCarpetClear();
        }
    }

    public void RedCarpetClear()
    {
        Manager.Instance.GameClear();
    }

    public void RedCarpetClearEvent()
    {
        
    }

    public void RedCarpetOver()
    {
        Manager.Instance.RoundOver();
    }

    public void RedCarpetOverEvent()
    {
        StartCoroutine(RedCarpetOverCor());
    }

    private IEnumerator RedCarpetOverCor()
    {
        float time = -2f;
        while (time < 2f)
        {
            time += 0.05f;
            transform.position = new Vector3(time, transform.position.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }

    }

    ////////////////////////////////////////
    public void PlugPull()
    {
        if (startPos == Vector3.zero) startPos = Input.mousePosition;

        Vector3 tempPos = Input.mousePosition;

        if(Vector3.Distance(startPos, tempPos) > 700f)
        {
            PlugPullClear();
        }
        else if(Vector3.Distance(startPos, tempPos) > 300f && !plugs[1].activeSelf)
        {
            plugs[0].SetActive(false);
            plugs[1].SetActive(true);
        }
        
    }

    public void PlugPullClear()
    {
        Manager.Instance.GameClear();
    }

    public void PlugPullClearEvent()
    {
        plugs[1].SetActive(false);
        plugs[2].SetActive(true);
        plugs[2].GetComponent<Rigidbody>().useGravity = true;
        plugs[3].SetActive(true);
        StartCoroutine(PlugClearCor());
    }

    private IEnumerator PlugClearCor()
    {
        float count = 1f;
        RectTransform rect = plugs[4].GetComponent<RectTransform>();

        while (count > 0)
        {
            count -= 0.02f;
            rect.localScale = new Vector3(count, count, 1f);
            yield return new WaitForEndOfFrame();
        }
        rect.localScale = Vector3.zero;
    }

    public void PlugPullOver()
    {
        Manager.Instance.RoundOver();
    }

    public void PlugPullOverEvent()
    {
        plugs[0].SetActive(false);
        plugs[1].SetActive(false);
        plugs[2].SetActive(true);
        plugs[6].SetActive(true);
    }

    ////////////////////////////////////////
    public void HiddenPlug()
    {
        if (startPos == Vector3.zero) startPos = Input.mousePosition;

        Vector3 tempPos = Input.mousePosition;

        if (Vector3.Distance(startPos, tempPos) > 100f)
        {
            PlugPullClear();
        }
        else if (Vector3.Distance(startPos, tempPos) > 50f && !plugs[1].activeSelf)
        {
            plugs[0].SetActive(false);
            plugs[1].SetActive(true);
        }

    }

    public void HiddenPlugClear()
    {
        Manager.Instance.GameClear();
    }

    public void HiddenPlugClearEvent()
    {
        plugs[1].SetActive(false);
        plugs[2].SetActive(true);
        plugs[2].GetComponent<Rigidbody>().useGravity = true;
    }

    public void HiddenPlugOver()
    {
        Manager.Instance.RoundOver();
    }

    public void HiddenPlugOverEvent()
    {

    }

    ////////////////////////////////////////

    public void Jenga()
    {
        if (startPos == Vector3.zero) startPos = transform.position;

        float distance = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = new Vector3(objPos.x, transform.position.y, transform.position.z);

        if (Vector3.Distance(transform.position, startPos) > 10f)
        {
            CrossCoupleClear();
        }

    }

    public void JengaClear()
    {
        Manager.Instance.GameClear();
    }

    public void JengaClearEvent()
    {
        for(int i = 0; i < jengas.Length; i++)
        {
            jengas[i].GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void JengaOver()
    {
        Manager.Instance.RoundOver();
    }

    public void JengaOverEvent()
    {
        StartCoroutine(JengaOverCor());
    }

    private IEnumerator JengaOverCor()
    {
        float time = 0.24f;
        while (time < 10f)
        {
            time += 0.13f;
            transform.position = new Vector3(time, transform.position.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < jengas.Length; i++)
        {
            jengas[i].GetComponent<Rigidbody>().useGravity = true;
        }
    }
    ////////////////////////////////////////

    public void PptName()
    {
        if (startPos == Vector3.zero) startPos = ppt_j.transform.position;

        float distance = Camera.main.WorldToScreenPoint(ppt_j.transform.position).z;

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.z = 0.06f;
        ppt_j.transform.position = objPos;

        if (Vector3.Distance(startPos, ppt_j.transform.position) > 8f)
        {
            PptNameClear();
        }
    }

    public void PptNameClear()
    {
        Manager.Instance.GameClear();
    }

    public void PptNameClearEvent()
    {
        ppt_j.SetActive(false);
        StartCoroutine(PptCor());
    }

    private IEnumerator PptCor()
    {
        Vector3 target = new Vector3(0f, -1.7f, -5.4f);
        float time = 5f;
        while(ppt_back.transform.position != target || time > 0f)
        {
            time -= Time.deltaTime;
            ppt_back.transform.position = Vector3.MoveTowards(ppt_back.transform.position, target, 0.2f);
            yield return new WaitForEndOfFrame();
        }
    }

    public void PptNameOver()
    {
        Manager.Instance.RoundOver();
    }

    public void PptNameOverEvent()
    {
        StartCoroutine(PptNameOverCor());
    }

    private IEnumerator PptNameOverCor()
    {
        float time = 0f;
        while (time < 15f)
        {
            time += 0.2f;
            transform.position = new Vector3(time, transform.position.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }

    }
}




