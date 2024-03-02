using System;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    private const int MaxNumberOfCardsInHand = 20;

    [Header("Dependencies")]
    [SerializeField] private Card _cardPrefab;

    [SerializeField] private Transform _cardsContainer;

    [Header("Settings")]
    [SerializeField] private float _cardsOffsetX;

    [SerializeField] private int _numberOfCardsToStartRotate;
    [SerializeField] private float _cardsOffsetRotationAngle;
    [SerializeField] private float _edgeCardMaxRotationAngle;

    [SerializeField] private float _cardsLoweringOffsetBasedOnAngle;

    [Header("Debug")]
    [Range(0, MaxNumberOfCardsInHand)]
    [SerializeField] private int _numberOfCards;

    private Card[] _cards;

    private void Start()
    {
        _cards = new Card[MaxNumberOfCardsInHand];
        for (var i = 0; i < _cards.Length; i++)
        {
            _cards[i] = Instantiate(_cardPrefab, _cardsContainer);
        }
    }

    private void Update()
    {
        for (var i = 0; i < _cards.Length; i++)
        {
            _cards[i].SetActive(i < _numberOfCards);
        }

        for (var i = 0; i < _numberOfCards; i++)
        {
            var card = _cards[i];
            
            var cardPositionAndRotation = GetCardPositionAndRotation(i, _numberOfCards, _cardsOffsetX,
                _cardsLoweringOffsetBasedOnAngle, _cardsOffsetRotationAngle, _edgeCardMaxRotationAngle, _numberOfCardsToStartRotate);
            card.SetTargetPositionAndRotation(cardPositionAndRotation);
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