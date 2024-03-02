using UnityEngine;
using UnityEngine.Serialization;

public class CardHand : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardsContainer;

    [SerializeField] private float _cardsOffsetX;

    [FormerlySerializedAs("_cardsRotationOffset")] [SerializeField]
    private float _cardsOffsetRotationAngle;

    [SerializeField] private float _edgeCardMaxRotationAngle;

    [SerializeField] private int _numberOfCards;

    private GameObject[] _cards;

    private void Start()
    {
        _cards = new GameObject[_numberOfCards];

        for (var i = 0; i < _numberOfCards; i++)
        {
            var card = Instantiate(_cardPrefab, _cardsContainer);
            var cardPosition = GetCardPositionAndRotation(i, _numberOfCards, _cardsOffsetX, _cardsOffsetRotationAngle,
                _edgeCardMaxRotationAngle);
            card.transform.localPosition = cardPosition;
            card.transform.localRotation = Quaternion.Euler(0f, 0f, cardPosition.z);

            _cards[i] = card;
        }
    }

    private void Update()
    {
        for (var i = 0; i < _cards.Length; i++)
        {
            var card = _cards[i];
            var cardPosition = GetCardPositionAndRotation(i, _numberOfCards, _cardsOffsetX, _cardsOffsetRotationAngle,
                _edgeCardMaxRotationAngle);
            card.transform.localPosition = cardPosition;
            card.transform.localRotation = Quaternion.Euler(0f, 0f, cardPosition.z);
        }
    }

    private static Vector3 GetCardPositionAndRotation(int handCardIndex, int cardsInHand, float cardsOffsetX,
        float cardsOffsetRotationAngle, float edgeCardMaxRotationAngle)
    {
        var centerIndex = cardsInHand / 2f - 0.5f;

        var offsetFromCenter = handCardIndex - centerIndex;

        var positionX = offsetFromCenter * cardsOffsetX;

        var edgeCardRotationAngle = centerIndex * cardsOffsetRotationAngle;
        if (edgeCardRotationAngle > edgeCardMaxRotationAngle)
        {
            cardsOffsetRotationAngle = edgeCardMaxRotationAngle / (cardsInHand / 2f);
        }

        var rotationAngle = offsetFromCenter * -cardsOffsetRotationAngle;

        return new Vector3(positionX, 0f, rotationAngle);
    }
}