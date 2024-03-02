using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _scaleOnHover;

    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale = transform.localScale;
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _initialScale;
    }
}