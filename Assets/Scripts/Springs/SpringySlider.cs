using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Springs
{
    public class SpringySlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _backgroundRaycastRect;

        [SerializeField] private RectTransform _handleSlideArea;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private Image _handleImage;

        [SerializeField] private float _handleScaleDelta;
        [SerializeField] private float _handleSpringFrequency;
        [SerializeField] private float _handleSpringDamping;
        [SerializeField] private Color _handleDraggingColor;

        [SerializeField] private int _slots;

        private bool _isDragging;

        private SpringyMotionParams _handleScaleSpringyMotionParams;
        private float _handleScaleCurrentVelocity;
        private float _handleTargetDeltaScale;
        private float _handleDeltaScale;
        private float _handleInitialScaleX;
        private float _handleInitialScaleY;

        private Color _handleInitialColor;

        private SpringyMotionParams _handlePositionSpringyMotionParams;
        private float _handlePositionCurrentVelocity;
        private float _handleTargetPositionX;
        private float _handlePositionX;

        private void Awake()
        {
            _handleScaleSpringyMotionParams = new SpringyMotionParams();
            _handlePositionSpringyMotionParams = new SpringyMotionParams();

            var handleLocalScale = _handle.localScale;
            _handleInitialScaleX = handleLocalScale.x;
            _handleInitialScaleY = handleLocalScale.y;

            _handleInitialColor = _handleImage.color;

            _handleTargetPositionX = _handle.localPosition.x;

            if (_slots > 0)
            {
                SnapToSlot();
            }
        }

        private void Update()
        {
            if (_isDragging)
            {
                SnapToMouse();
            }

            HandleSpringyPosition();
            HandleSpringyScale();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            _handleTargetDeltaScale = _handleScaleDelta;

            _handleImage.color = _handleDraggingColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            _handleTargetDeltaScale = 0f;

            _handleImage.color = _handleInitialColor;

            if (_slots > 0)
            {
                SnapToSlot();
            }
        }

        private void SnapToMouse()
        {
            var mousePositionX = GetMousePositionXInRaycastRect();
            var halfWidth = _backgroundRaycastRect.rect.width / 2f;
            var lerpedPositionX = Mathf.InverseLerp(-halfWidth, halfWidth, mousePositionX);

            var handleAreaHalfWidth = _handleSlideArea.rect.width / 2f;
            var handlePositionX = Mathf.Lerp(-handleAreaHalfWidth, handleAreaHalfWidth, lerpedPositionX);

            _handleTargetPositionX = handlePositionX;
        }

        private void SnapToSlot()
        {
            float slotPositionX;

            if (_slots == 1)
            {
                slotPositionX = 0f;
            }
            else
            {
                var mousePositionX = GetMousePositionXInRaycastRect();
                var halfWidth = _handleSlideArea.rect.width / 2f;
                var currentPos = Mathf.InverseLerp(-halfWidth, halfWidth, mousePositionX);

                var slotWidth = 1f / (_slots - 1);
                var slotRounded = (float)Math.Round(currentPos / slotWidth);
                slotPositionX = Mathf.Lerp(-halfWidth, halfWidth, slotRounded * slotWidth);
            }

            _handleTargetPositionX = slotPositionX;
        }

        private void HandleSpringyPosition()
        {
            SpringyUtils.CalcDampedSpringMotionParams(ref _handlePositionSpringyMotionParams, Time.deltaTime, _handleSpringFrequency, _handleSpringDamping);
            SpringyUtils.UpdateDampedSpringMotion(ref _handlePositionX, ref _handlePositionCurrentVelocity, _handleTargetPositionX, _handleScaleSpringyMotionParams);

            _handle.localPosition = new Vector3(_handlePositionX, 0f, 0f);
        }

        private void HandleSpringyScale()
        {
            SpringyUtils.CalcDampedSpringMotionParams(ref _handleScaleSpringyMotionParams, Time.deltaTime, _handleSpringFrequency, _handleSpringDamping);
            SpringyUtils.UpdateDampedSpringMotion(ref _handleDeltaScale, ref _handleScaleCurrentVelocity, _handleTargetDeltaScale, _handleScaleSpringyMotionParams);

            _handle.localScale = new Vector3(_handleInitialScaleX + _handleDeltaScale, _handleInitialScaleY + _handleDeltaScale, 1f);
        }

        private float GetMousePositionXInRaycastRect()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundRaycastRect, Input.mousePosition, null, out var localPoint);
            return localPoint.x;
        }
    }
}