using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Show()
    {
        _canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0f;
    }
}