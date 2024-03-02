using UnityEngine;

public class CardHand : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardsContainer;

    [SerializeField] private float _cardsOffsetX;

    [SerializeField] private int _numberOfCards;

    private GameObject[] _cards;

    private void Start()
    {
        _cards = new GameObject[_numberOfCards];

        for (var i = 0; i < _numberOfCards; i++)
        {
            var card = Instantiate(_cardPrefab, _cardsContainer);

            var cardPosition = GetCardPosition(i, _numberOfCards, _cardsOffsetX);

            card.transform.localPosition = cardPosition;

            _cards[i] = card;
        }
    }

    private void Update()
    {
        for (var i = 0; i < _cards.Length; i++)
        {
            var card = _cards[i];
            var cardPosition = GetCardPosition(i, _numberOfCards, _cardsOffsetX);
            card.transform.localPosition = cardPosition;
        }
    }

    private static Vector3 GetCardPosition(int handCardIndex, int cardsInHand, float cardsOffsetX)
    {
        var centerIndex = cardsInHand / 2f - 0.5f;

        var offsetFromCenter = handCardIndex - centerIndex;
        var positionX = offsetFromCenter * cardsOffsetX;

        return new Vector3(positionX, 0f, 0f);
    }
}