using System;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private Transform _cardsContainer;

    [Header("Settings")]
    [SerializeField] private float _cardsOffsetX;

    [SerializeField] private int _numberOfCardsToStartRotate;
    [SerializeField] private float _cardsOffsetRotationAngle;
    [SerializeField] private float _edgeCardMaxRotationAngle;

    [SerializeField] private float _cardsLoweringOffsetBasedOnAngle;

    [Header("Debug")]
    [SerializeField] private int _numberOfCards;

    private GameObject[] _cards;

    private void Start()
    {
        _cards = new GameObject[_numberOfCards];

        for (var i = 0; i < _numberOfCards; i++)
        {
            var card = Instantiate(_cardPrefab, _cardsContainer);
            var cardPosition = GetCardPositionAndRotation(i, _numberOfCards, _cardsOffsetX,
                _cardsLoweringOffsetBasedOnAngle, _cardsOffsetRotationAngle, _edgeCardMaxRotationAngle, _numberOfCardsToStartRotate);
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
            var cardPosition = GetCardPositionAndRotation(i, _numberOfCards, _cardsOffsetX,
                _cardsLoweringOffsetBasedOnAngle, _cardsOffsetRotationAngle, _edgeCardMaxRotationAngle, _numberOfCardsToStartRotate);
            card.transform.localPosition = cardPosition;
            card.transform.localRotation = Quaternion.Euler(0f, 0f, cardPosition.z);
        }
    }

    private static Vector3 GetCardPositionAndRotation(int handCardIndex, int numberOfCardsInHand, float cardsOffsetX,
        float cardsOffsetY, float cardsOffsetRotationAngle, float edgeCardMaxRotationAngle, int numberOfCardsToStartRotate)
    {
        var centerIndex = numberOfCardsInHand / 2f - 0.5f;
        var offsetFromCenter = handCardIndex - centerIndex;

        var positionX = offsetFromCenter * cardsOffsetX;

        var edgeCardRotationAngle = centerIndex * cardsOffsetRotationAngle;
        if (edgeCardRotationAngle > edgeCardMaxRotationAngle)
        {
            cardsOffsetRotationAngle = edgeCardMaxRotationAngle / (numberOfCardsInHand / 2f);
        }

        var positionY = 0f;
        var rotationAngle = 0f;

        if (numberOfCardsInHand > numberOfCardsToStartRotate)
        {
            positionY = Math.Abs(offsetFromCenter) * -cardsOffsetY;
            rotationAngle = offsetFromCenter * -cardsOffsetRotationAngle;
        }

        return new Vector3(positionX, positionY, rotationAngle);
    }
}