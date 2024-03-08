using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Text _idText;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void UpdateView(Card card)
    {
        _idText.text = card.Id.ToString();
    }

    public void Show()
    {
        _canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0f;
    }
}