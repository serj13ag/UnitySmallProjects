using System;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    private const int MaxNumberOfCardsInHand = 20;

    [Header("Dependencies")]
    [SerializeField] private HandCardView _handCardViewPrefab;

    [SerializeField] private CardView _previewCardView;
    [SerializeField] private Transform _cardsContainer;

    [Header("Settings")]
    [SerializeField] private float _cardsOffsetX;

    [SerializeField] private int _numberOfCardsToStartRotate;
    [SerializeField] private float _cardsOffsetRotationAngle;
    [SerializeField] private float _edgeCardMaxRotationAngle;

    [SerializeField] private float _cardsLoweringOffsetBasedOnAngle;

    private Stack<HandCardView> _handCardViewPool;
    private List<HandCardView> _cardsInHand;

    public void Init()
    {
        _handCardViewPool = CreateHandCardViewPool();
        _cardsInHand = new List<HandCardView>();
        _previewCardView.Hide();
    }

    private void Update()
    {
        for (int i = 0; i < _cardsInHand.Count; i++)
        {
            var cardPositionAndRotation = GetCardPositionAndRotation(i, _cardsInHand.Count, _cardsOffsetX,
                _cardsLoweringOffsetBasedOnAngle, _cardsOffsetRotationAngle, _edgeCardMaxRotationAngle,
                _numberOfCardsToStartRotate);

            _cardsInHand[i].SetTargetPositionAndRotation(cardPositionAndRotation);
        }
    }

    public void AddCards(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
        {
            AddCard(card);
        }
    }

    public void AddCard(Card card)
    {
        if (_cardsInHand.Count >= MaxNumberOfCardsInHand)
        {
            return;
        }

        AddHandCardView(card);
    }

    public void ShowPreviewCard(Card card, Vector3 position)
    {
        _previewCardView.UpdateView(card);
        _previewCardView.transform.position = position + new Vector3(0f, 100f, 0f);
        _previewCardView.Show();
    }

    public void HidePreviewCard()
    {
        _previewCardView.Hide();
    }

    private Stack<HandCardView> CreateHandCardViewPool()
    {
        var handCardViewPool = new Stack<HandCardView>(MaxNumberOfCardsInHand);
        for (var i = 0; i < MaxNumberOfCardsInHand; i++)
        {
            var handCardView = Instantiate(_handCardViewPrefab, _cardsContainer);
            handCardView.Init(this);
            handCardView.SetActive(false);

            handCardViewPool.Push(handCardView);
        }

        return handCardViewPool;
    }

    private void AddHandCardView(Card card)
    {
        var cardPrefab = _handCardViewPool.Pop();
        cardPrefab.UpdateView(card, _cardsInHand.Count);
        cardPrefab.SetActive(true);

        _cardsInHand.Add(cardPrefab);
    }

    private static Vector3 GetCardPositionAndRotation(int handCardIndex, int numberOfCardsInHand, float cardsOffsetX,
        float cardsOffsetY, float cardsOffsetRotationAngle, float edgeCardMaxRotationAngle,
        int numberOfCardsToStartRotate)
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