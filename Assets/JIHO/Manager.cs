using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    
    public TimerManager timerManager;
    public SceneChanger sceneChanger;
    public UIManager uiManager;

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
        NextGame("Test2", 10f);
    }

    private void UpdateManagers()
    {
        if (!isStart || isOver) return; 

        timerManager.UpdateTimer();
        uiManager.UpdateImage(timerManager.timer, timerManager.maxTime);

    }

    public void NextGame(string name, float time)
    {
        sceneChanger.SceneLoad(name);
        timerManager.StartTimer(time);
        isStart = true;
    }

    public void GameOver()
    {
        isOver = true;
        isStart = false;

        Debug.Log("GameOver");
    }
}