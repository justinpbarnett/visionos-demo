using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public CanvasGroup[] canvasGroups;
    public float duration = 1.0f;

    public void FadeIn()
    {
        foreach (CanvasGroup group in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(group, group.alpha, 1, duration));
        }
    }

    public void FadeOut()
    {
        foreach (CanvasGroup group in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(group, group.alpha, 0, duration));
        }
    }

    private static IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        var timeStartedLerping = Time.time;

        while (true)
        {
            var timeSinceStarted = Time.time - timeStartedLerping;
            var percentageComplete = timeSinceStarted / lerpTime;
            var currentValue = Mathf.Lerp(start, end, percentageComplete);
            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }
    }
}