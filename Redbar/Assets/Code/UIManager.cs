using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CanvasGroup uiElement;

    public string winText;
    public string loseText;
    public string creditsText;

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
    {
        float timeStarting = Time.time;
        float timeSinceStarted = Time.time - timeStarting;
        float percentage = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - timeStarting;
            percentage = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentage);

            cg.alpha = currentValue;

            if (percentage >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        print("done");
    }

    public void changeText(string newText)
    {
        uiElement.GetComponentInChildren<Text>().text = newText;
    }
}
