using UnityEngine;
using UnityEngine.EventSystems;

namespace Springs
{
    public class SpringyScaleNudger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _frequency;
        [SerializeField] private float _damping;
        [SerializeField] private float _nudgeStrength;
        [SerializeField] private bool _nudgeOnPointerEnter;
        [SerializeField] private bool _nudgeOnPointerExit;

        private SpringyMotionParams _springyMotionParams;
        private float _initialScaleX;
        private float _initialScaleY;
        private float _targetDeltaScale;
        private float _deltaScale;
        private float _currentVelocity;

        private void Awake()
        {
            _springyMotionParams = new SpringyMotionParams();

            var localScale = _transform.localScale;
            _initialScaleX = localScale.x;
            _initialScaleY = localScale.y;
        }

        private void Update()
        {
            SpringyUtils.CalcDampedSpringMotionParams(ref _springyMotionParams, Time.deltaTime, _frequency, _damping);
            SpringyUtils.UpdateDampedSpringMotion(ref _deltaScale, ref _currentVelocity, _targetDeltaScale, _springyMotionParams);

            _transform.localScale = new Vector3(_initialScaleX + _deltaScale, _initialScaleY + _deltaScale, 1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_nudgeOnPointerEnter)
            {
                Nudge();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_nudgeOnPointerExit)
            {
                Nudge();
            }
        }

        private void Nudge()
        {
            _currentVelocity += _nudgeStrength;
        }
    }
}