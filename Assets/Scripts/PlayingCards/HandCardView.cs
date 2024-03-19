using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayingCards
{
    public class HandCardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
        IPointerUpHandler
    {
        [SerializeField] private CardView _cardView;

        private CardHand _cardHand;

        private Card _card;
        private int _initialSiblingIndex;

        private CardState _cardState;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        public int CardId => _card.Id;

        public void Init(CardHand cardHand)
        {
            _cardHand = cardHand;
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

        public void UpdateView(Card card, int siblingIndex)
        {
            _card = card;
            _initialSiblingIndex = siblingIndex;

            _cardView.UpdateView(card);
            transform.SetSiblingIndex(siblingIndex);

            _cardState = CardState.InHand;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void DecrementSiblingIndex()
        {
            _initialSiblingIndex--;
            transform.SetSiblingIndex(_initialSiblingIndex);
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

            _cardHand.ShowPreviewCard(_card, transform.position);
            _cardView.Hide();

            _cardState = CardState.Hovered;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_cardState == CardState.Dragged)
            {
                return;
            }

            _cardHand.HidePreviewCard();
            _cardView.Show();

            _cardState = CardState.InHand;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _cardHand.HidePreviewCard();

            _cardView.Show();
            transform.SetAsLastSibling();

            _cardState = CardState.Dragged;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_cardHand.InDropZone(eventData.position))
            {
                _cardHand.RemoveCard(_card);
            }
            else
            {
                transform.SetSiblingIndex(_initialSiblingIndex);
                _cardState = CardState.InHand;
            }
        }
    }
}