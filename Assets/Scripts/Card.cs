using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _scaleOnHover;
    [SerializeField] private float _yOffsetOnHover;

    private Vector3 _initialScale;
    private int _initialSiblingIndex;

    private bool _isHovered;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Vector3 _targetScale;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _initialSiblingIndex = transform.GetSiblingIndex();

        _targetScale = _initialScale;
    }

    private void Update()
    {
        var trs = transform;
        trs.localPosition = _targetPosition;
        trs.localRotation = _targetRotation;
        trs.localScale = _targetScale;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetTargetPositionAndRotation(Vector3 positionAndRotation)
    {
        if (_isHovered)
        {
            return;
        }

        _targetPosition = new Vector3(positionAndRotation.x, positionAndRotation.y, 0f);
        _targetRotation = Quaternion.Euler(0f, 0f, positionAndRotation.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _targetScale = _initialScale * _scaleOnHover;
        _targetPosition = new Vector3(transform.localPosition.x, _yOffsetOnHover, 0f);
        _targetRotation = quaternion.identity;

        transform.SetAsLastSibling();

        _isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _targetScale = _initialScale;

        transform.SetSiblingIndex(_initialSiblingIndex);

        _isHovered = false;
    }
}