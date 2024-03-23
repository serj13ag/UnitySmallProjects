using UnityEngine;
using UnityEngine.EventSystems;

namespace Springs
{
    public class SpringySlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _backgroundRaycastRect;

        [SerializeField] private RectTransform _handleSlideArea;
        [SerializeField] private RectTransform _handle;

        [SerializeField] private float _handleScaleDelta;
        [SerializeField] private float _handleSpringFrequency;
        [SerializeField] private float _handleSpringDamping;

        private bool _isDragging;

        private SpringyMotionParams _handleSpringyMotionParams;
        private float _handleTargetDeltaScale;
        private float _handleCurrentVelocity;
        private float _handleDeltaScale;
        private float _handleInitialScaleX;
        private float _handleInitialScaleY;

        private void Awake()
        {
            _handleSpringyMotionParams = new SpringyMotionParams();

            var handleLocalScale = _handle.localScale;
            _handleInitialScaleX = handleLocalScale.x;
            _handleInitialScaleY = handleLocalScale.y;
        }

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

            HandleSpringyScale();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            _handleTargetDeltaScale = _handleScaleDelta;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            _handleTargetDeltaScale = 0f;
        }

        private void HandleSpringyScale()
        {
            SpringyUtils.CalcDampedSpringMotionParams(ref _handleSpringyMotionParams, Time.deltaTime, _handleSpringFrequency, _handleSpringDamping);
            SpringyUtils.UpdateDampedSpringMotion(ref _handleDeltaScale, ref _handleCurrentVelocity, _handleTargetDeltaScale, _handleSpringyMotionParams);

            _handle.localScale = new Vector3(_handleInitialScaleX + _handleDeltaScale, _handleInitialScaleY + _handleDeltaScale, 1f);
        }
    }
}