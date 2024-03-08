using UnityEngine;
using UnityEngine.EventSystems;

public class HandCardView : CardView, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private CardHand _cardHand;

    private CardState _cardState;

    private int _initialSiblingIndex;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    public void Init(CardHand cardHand)
    {
        _cardHand = cardHand;

        _initialSiblingIndex = transform.GetSiblingIndex();
    }

    private void Update()
    {
        if (_cardState == CardState.Dragged)
        {
            var trs = transform;
            trs.position = Input.mousePosition;
            trs.localRotation = Quaternion.identity;
        }
        else
        {
            var trs = transform;
            trs.localPosition = _targetPosition;
            trs.localRotation = _targetRotation;
        }
    }

    public void SetData(Card card)
    {
        // TODO add text
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
        if (_cardState == CardState.Dragged)
        {
            return;
        }

        _cardHand.ShowPreviewCard(transform.position);
        Hide();

        _cardState = CardState.Hovered;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_cardState == CardState.Dragged)
        {
            return;
        }

        _cardHand.HidePreviewCard();
        Show();

        _cardState = CardState.InHand;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _cardHand.HidePreviewCard();

        Show();
        transform.SetAsLastSibling();

        _cardState = CardState.Dragged;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.SetSiblingIndex(_initialSiblingIndex);

        _cardState = CardState.InHand;
    }
}