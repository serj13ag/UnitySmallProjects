using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _scaleOnHover;
    [SerializeField] private float _yOffsetOnHover;

    private Vector3 _initialScale;
    private int _initialSiblingIndex;

    private CardState _cardState;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Vector3 _targetScale;
    private Vector3 _localPositionOnClick;
    private Vector3 _mousePositionOnClick;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _initialSiblingIndex = transform.GetSiblingIndex();

        _targetScale = _initialScale;
    }

    private void Update()
    {
        if (_cardState == CardState.Dragged)
        {
            var mouseDragDistance = Input.mousePosition - _mousePositionOnClick;
            transform.localPosition = _localPositionOnClick + mouseDragDistance;
        }
        else
        {
            var trs = transform;
            trs.localPosition = _targetPosition;
            trs.localRotation = _targetRotation;
            trs.localScale = _targetScale;
        }
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetTargetPositionAndRotation(Vector3 positionAndRotation)
    {
        if (_cardState == CardState.InHand)
        {
            _targetPosition = new Vector3(positionAndRotation.x, positionAndRotation.y, 0f);
            _targetRotation = Quaternion.Euler(0f, 0f, positionAndRotation.z);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _targetScale = _initialScale * _scaleOnHover;
        _targetPosition = new Vector3(transform.localPosition.x, _yOffsetOnHover, 0f);
        _targetRotation = quaternion.identity;

        transform.SetAsLastSibling();

        _cardState = CardState.Hovered;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _targetScale = _initialScale;

        transform.SetSiblingIndex(_initialSiblingIndex);

        _cardState = CardState.InHand;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _localPositionOnClick = transform.localPosition;
        _mousePositionOnClick = Input.mousePosition;

        _cardState = CardState.Dragged;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _cardState = CardState.Hovered;
    }
}