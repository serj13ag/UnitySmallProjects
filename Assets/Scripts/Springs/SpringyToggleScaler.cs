using UnityEngine;
using UnityEngine.UI;

namespace Springs
{
    public class SpringyToggleScaler : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        [SerializeField] private float _scaleDeltaOnToggle;
        [SerializeField] private float _frequency;
        [SerializeField] private float _damping;

        private float _initialScaleX;
        private float _initialScaleY;

        private float _targetDeltaScale;

        private SpringyMotionParams _springyMotionParams;
        private float _currentVelocity;
        private float _deltaScale;

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void Awake()
        {
            _springyMotionParams = new SpringyMotionParams();

            var localScale = transform.localScale;
            _initialScaleX = localScale.x;
            _initialScaleY = localScale.y;
        }

        private void Update()
        {
            SpringyUtils.CalcDampedSpringMotionParams(ref _springyMotionParams, Time.deltaTime, _frequency, _damping);
            SpringyUtils.UpdateDampedSpringMotion(ref _deltaScale, ref _currentVelocity, _targetDeltaScale, _springyMotionParams);

            transform.localScale = new Vector3(_initialScaleX + _deltaScale, _initialScaleY + _deltaScale, 1f);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            _targetDeltaScale = isOn ? _scaleDeltaOnToggle : 0f;
        }
    }
}