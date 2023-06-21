using System.Collections;
using UnityEngine;

public abstract class App : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Fader fader;
    [SerializeField] private CanvasGroup[] canvasGroups;
    
    public bool IsMinimized { get; private set; }
    private GazeInteractable[] _gazeInteractables;

    private void Awake()
    {
        _gazeInteractables = GetComponentsInChildren<GazeInteractable>();
    }

    public void Show()
    {
        foreach (var canvas in canvasGroups)
        {
            canvas.gameObject.SetActive(true);
        }
        foreach (var interactable in _gazeInteractables)
        {
            interactable.enabled = true;
        }
        IsMinimized = false;
        fader.FadeIn(canvasGroups);
    }

    public void Hide()
    {
        StartCoroutine(HideRoutine());
    }

    public void SoloHomeMenu()
    {
        AppManager.Instance.MinimizeAll();
    }
    
    private IEnumerator HideRoutine()
    {
        fader.FadeOut(canvasGroups);
        yield return new WaitForSeconds(fader.DefaultDuration);
        foreach (var canvas in canvasGroups)
        {
            canvas.gameObject.SetActive(false);
        }
        foreach (var interactable in _gazeInteractables)
        {
            interactable.enabled = false;
        }
        IsMinimized = true;
    }
}