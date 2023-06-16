using UnityEngine;

public class Resizer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform objectToScale;
    
    [Header("Settings")]
    [SerializeField] private float scaleFactor = 0.1f;
    [SerializeField] private float duration = 1.0f;

    private Vector3 _originalScale;
    private bool _isHovering = false;

    private void Start()
    {
        _originalScale = objectToScale.transform.localScale;
    }

    private void Update()
    {
        if (_isHovering)
        {
            // Scale the object up to a maximum of originalScale * (1 + scaleFactor)
            if (objectToScale.transform.localScale.magnitude < _originalScale.magnitude * (1 + scaleFactor))
            {
                objectToScale.transform.localScale += _originalScale * (scaleFactor * Time.deltaTime) / duration;
            }
        }
        else
        {
            // Scale the object down to the original scale
            if (objectToScale.transform.localScale.magnitude > _originalScale.magnitude)
            {
                objectToScale.transform.localScale -= _originalScale * (scaleFactor * Time.deltaTime) / duration;
            }

            // Make sure we don't go below the original scale
            if (objectToScale.transform.localScale.magnitude < _originalScale.magnitude)
            {
                objectToScale.transform.localScale = _originalScale;
            }
        }
    }

    public void OnHoverEnter()
    {
        _isHovering = true;
    }

    public void OnHoverExit()
    {
        _isHovering = false;
    }
}
