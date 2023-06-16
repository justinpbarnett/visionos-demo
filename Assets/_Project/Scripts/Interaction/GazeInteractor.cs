using System.Linq;
using UnityEngine;

public class GazeInteractor : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool showRay = false;
    [SerializeField] private Color lineColor = Color.white;
    [SerializeField] private Color lineHoverColor = Color.yellow;
    [SerializeField] private Color lineSelectColor = Color.red;

    [Header("Settings")]
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float pinchSelectStrength = 0.9f;

    private GazeInteractable _hoveredInteractable;
    private GazeInteractable _selectedInteractable;
    private OVRHand[] _hands;
    private Color _debugColor;

    private void Start()
    {
        _hands = FindObjectsOfType<OVRHand>();
        _debugColor = lineColor;
    }

    private void Update()
    {
        PerformRaycast();
        TrySelect();

#if UNITY_EDITOR
        if (showRay)
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, _debugColor);
        }
#endif
    }

    private void PerformRaycast()
    {
        Vector3 raycastDirection = transform.forward * maxDistance;
        if (Physics.Raycast(transform.position, raycastDirection, out RaycastHit hit, maxDistance, layerMask))
        {
            if (hit.transform.gameObject.CompareTag("GazeInteractable"))
            {
                var interactable = hit.collider.GetComponent<GazeInteractable>();
                if (_hoveredInteractable != null && _hoveredInteractable != interactable)
                {
                    _hoveredInteractable.onHoverExit?.Invoke();
                    _hoveredInteractable = interactable;
                    _hoveredInteractable.onHoverEnter?.Invoke();
                }
                else if (_hoveredInteractable == null)
                {
                    _hoveredInteractable = interactable;
                    _hoveredInteractable.onHoverEnter?.Invoke();
                }
                _debugColor = lineHoverColor;
            }
        }
        else if (_hoveredInteractable != null)
        {
            _hoveredInteractable.onHoverExit?.Invoke();
            _hoveredInteractable = null;
            _debugColor = lineColor;
        }
    }

    private bool IsPinching()
    {
        return _hands.Any(hand => hand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > pinchSelectStrength);
    }

    private void TrySelect()
    {
        if (IsPinching())
        {
            if (_selectedInteractable == null && _hoveredInteractable != null)
            {
                _selectedInteractable = _hoveredInteractable;
                _selectedInteractable.onSelectEnter?.Invoke();
                _debugColor = lineSelectColor;
            }
        }
        else if (_selectedInteractable != null)
        {
            _selectedInteractable.onSelectExit?.Invoke();
            _selectedInteractable = null;
        }
    }
}
