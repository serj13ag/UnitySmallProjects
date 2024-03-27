using UnityEngine;

namespace Springs.Constructor
{
    public class SpringySpring : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] private float _targetSpringValue = 0.5f;
        [SerializeField] private float _frequency = 20f;
        [SerializeField] private float _damping = 0.5f;

        private SpringyMotionParams _springyMotionParams;
        private float _currentVelocity;
        private float _currentSpringValue;

        public float CurrentSpringValue => _currentSpringValue;

        private void Awake()
        {
            _springyMotionParams = new SpringyMotionParams();

            _currentSpringValue = _targetSpringValue;
        }

        private void Update()
        {
            SpringyUtils.CalcDampedSpringMotionParams(ref _springyMotionParams, Time.deltaTime, _frequency, _damping);
            SpringyUtils.UpdateDampedSpringMotion(ref _currentSpringValue, ref _currentVelocity, _targetSpringValue, _springyMotionParams);
        }
    }
}