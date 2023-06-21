using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public float DefaultDuration => defaultDuration;
    [SerializeField] private float defaultDuration = 0.25f;

    public void FadeIn(CanvasGroup[] canvasGroups)
    {
        foreach (CanvasGroup group in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(group, 0, 1, DefaultDuration));
        }
    }

    public void FadeOut(CanvasGroup[] canvasGroups)
    {
        foreach (CanvasGroup group in canvasGroups)
        {
            StartCoroutine(FadeCanvasGroup(group, 1, 0, DefaultDuration));
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