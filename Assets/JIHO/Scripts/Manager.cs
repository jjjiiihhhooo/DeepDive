using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity;
using UnityEngine.Events;
using TMPro.EditorUtilities;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    [SerializeField] public string name;
    [SerializeField] public string type;
    [SerializeField] public float timer;
    [SerializeField] public float clearTimer;
    [SerializeField] public UnityEvent _clearEvent;
    [SerializeField] public UnityEvent _overEvent;
}


public class Manager : MonoBehaviour
{
    public static Manager Instance;
    
    public TimerManager timerManager;
    public SceneChanger sceneChanger;
    public UIManager uiManager;

    public int roundCount = 1;
    public int sceneIndex = -1;
    public int lifeCount = 3;

    public string tempSceneName = "Test2";
    public float tempTimer = 10f;

    public SceneData[] sceneDatas;

    public bool isStart;
    public bool isOver;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        UpdateManagers();
    }

    private void Init()
    {
        ListShuffle();
        roundCount = 0;
        //GameStart();
    }

    private void ListShuffle()
    {
        for(int i = 0; i < sceneDatas.Length; i++)
        {
            System.Random ran = new System.Random();
            int randomValue = ran.Next(0, sceneDatas.Length);
            SceneData temp = sceneDatas[i];
            sceneDatas[i] = sceneDatas[randomValue];
            sceneDatas[randomValue] = temp;
        }
    }

    private void UpdateManagers()
    {
        if (!isStart || isOver) return; 

        timerManager.UpdateTimer();
        uiManager.UpdateImage(timerManager.timer, timerManager.maxTime);
    }

    public void GameStart() // firstStart
    {
        uiManager.firstStartButton_obj.SetActive(false);
        uiManager.RoundEnd(0.5f);
    }


    public void NextGame()
    {
        roundCount++;
        sceneIndex++;

        if(sceneDatas.Length <= sceneIndex)
        {
            LastGameClear();
            return;
        }

        sceneChanger.SceneLoad(sceneDatas[sceneIndex].name);
        uiManager.RoundStart(2f);
    }

    public void RoundStart()
    {
        timerManager.StartTimer(sceneDatas[sceneIndex].timer);
        uiManager.typeText.text = sceneDatas[sceneIndex].type;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "FakeText") uiManager.typeText.gameObject.SetActive(true);
        
        isStart = true;
        isOver = false;

        if (scene.name == "PlugPull")
        {
            PointerScript pointer = FindObjectOfType<PointerScript>();
            pointer.plugs[5].SetActive(true);
        }
    }

    public void GameClear()
    {
        isStart = false;
        sceneDatas[sceneIndex]._clearEvent?.Invoke();
        uiManager.RoundClear(sceneDatas[sceneIndex].clearTimer);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }

    public void RoundOver()
    {
        isOver = true;
        isStart = false;
        
        uiManager.RoundOver(sceneDatas[sceneIndex].clearTimer);
        sceneDatas[sceneIndex]._overEvent?.Invoke();
    }

    public void LastGameClear()
    {

    }
}
