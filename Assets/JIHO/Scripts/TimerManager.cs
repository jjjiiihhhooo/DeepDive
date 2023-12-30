using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    public float timer;
    public float maxTime;

    public UnityEvent _event;

    public void StartTimer(float time)
    {
        maxTime = time;
        timer = maxTime;
    }

    public void UpdateTimer()
    {
        if (timer <= 0f && !Manager.Instance.isOver)
        {
            if(Manager.Instance.lifeCount > 0) Manager.Instance.RoundOver();
            else Manager.Instance.GameOver();
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        _event.Invoke();
    }

    public void OneClick(float count)
    {

    }

}


