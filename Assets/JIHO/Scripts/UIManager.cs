using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image timer_fill;
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI roundOverText;
    public GameObject firstStartButton_obj;

    public Animator clearAnimator;


    public void UpdateImage(float time, float maxTime)
    {
        timer_fill.fillAmount = time / maxTime;
    }

    public void RoundClear(float delay) //clear motion
    {
        clearText.gameObject.SetActive(true);
        RoundEnd(delay);
    }

    public void RoundStart(float delay) //fade in
    {
        StartCoroutine(RoundStartCor(delay));
    }

    private IEnumerator RoundStartCor(float delay)
    {
        roundText.text = Manager.Instance.roundCount.ToString() + " ROUND";
        roundText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        roundText.gameObject.SetActive(false);
        clearAnimator.SetTrigger("Fade_in");
    }

    public void RoundEnd(float delay) //fade out
    {
        StartCoroutine(RoundEndCor(delay));
    }

    private IEnumerator RoundEndCor(float delay)
    {
        yield return new WaitForSeconds(delay);
        clearText.gameObject.SetActive(false);
        roundOverText.gameObject.SetActive(false);
        clearAnimator.SetTrigger("Fade_out");
    }

    public void AnimationEventNextGame()
    {
        Manager.Instance.NextGame();
    }

    public void AnimationEventRoundStart()
    {
        timer_fill.gameObject.SetActive(true);
        Manager.Instance.RoundStart();
    }

    public void RoundOver(float delay)
    {
        roundOverText.gameObject.SetActive(true);
        RoundEnd(delay);
    }
}
