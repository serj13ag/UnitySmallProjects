using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private CardHand _cardHand;
    [SerializeField] private int _cardsInStartHand;

    private int _nextCardId;

    private void Awake()
    {
        _cardHand.Init();
    }

    private void Start()
    {
        var startCards = new Card[_cardsInStartHand];

        for (var i = 0; i < _cardsInStartHand; i++)
        {
            startCards[i] = CreateCard();
        }

        _cardHand.AddCards(startCards);
    }

    private Card CreateCard()
    {
        return new Card(_nextCardId++);
    }
}