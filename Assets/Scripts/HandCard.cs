using UnityEngine;
using UnityEngine.EventSystems;

public class HandCard : Card, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private CardHand _cardHand;

    private CardState _cardState;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Vector3 _localPositionOnClick;
    private Vector3 _mousePositionOnClick;

    public void Init(CardHand cardHand)
    {
        _cardHand = cardHand;
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
        _cardHand.ShowPreviewCard(transform.position);
        Hide();

        _cardState = CardState.Hovered;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardHand.HidePreviewCard();
        Show();

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