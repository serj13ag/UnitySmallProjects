using UnityEngine;
using UnityEngine.EventSystems;

namespace Springs
{
    public class SpringySlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _backgroundRaycastRect;

        [SerializeField] private RectTransform _handleSlideArea;
        [SerializeField] private RectTransform _handle;

        private bool _isDragging;

        private void Update()
        {
            if (_isDragging)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundRaycastRect, Input.mousePosition, null, out var localPoint);

                var rectPositionX = localPoint.x;
                var halfWidth = _backgroundRaycastRect.rect.width / 2f;
                var lerpedPositionX = Mathf.InverseLerp(-halfWidth, halfWidth, rectPositionX);

                var handleAreaHalfWidth = _handleSlideArea.rect.width / 2f;
                var handlePositionX = Mathf.Lerp(-handleAreaHalfWidth, handleAreaHalfWidth, lerpedPositionX);

                _handle.localPosition = new Vector3(handlePositionX, 0f, 0f);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
        }
    }
}