using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _scaleOnHover;

    private Vector3 _initialScale;
    private int _initialSiblingIndex;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _initialSiblingIndex = transform.GetSiblingIndex();
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetPositionAndRotation(Vector3 positionAndRotation)
    {
        transform.localPosition = new Vector3(positionAndRotation.x, positionAndRotation.y, 0f);
        transform.localRotation = Quaternion.Euler(0f, 0f, positionAndRotation.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _initialScale * _scaleOnHover;
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _initialScale;
        transform.SetSiblingIndex(_initialSiblingIndex);
    }
}