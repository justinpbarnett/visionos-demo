using UnityEngine;
using UnityEngine.Events;

public class GazeInteractable : MonoBehaviour
{
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;
    public UnityEvent onSelectEnter;
    public UnityEvent onSelectExit;

    private void Start()
    {
        onHoverEnter.AddListener(HandleHoverEnter);
        onHoverExit.AddListener(HandleHoverExit);
        onSelectEnter.AddListener(HandleSelectEnter);
        onSelectExit.AddListener(HandleSelectExit);
    }

    private void OnDestroy()
    {
        onHoverEnter.RemoveListener(HandleHoverEnter);
        onHoverExit.RemoveListener(HandleHoverExit);
        onSelectEnter.RemoveListener(HandleSelectEnter);
        onSelectExit.RemoveListener(HandleSelectExit);
    }

    private void HandleHoverEnter()
    {

    }

    private void HandleHoverExit()
    {

    }

    private void HandleSelectEnter()
    {

    }

    private void HandleSelectExit()
    {

    }
}
