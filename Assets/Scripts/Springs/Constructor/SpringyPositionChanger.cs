using UnityEngine;

namespace Springs.Constructor
{
    [RequireComponent(typeof(SpringySpring))]
    public class SpringyPositionChanger : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _minPosition;
        [SerializeField] private Vector3 _maxPosition;
        [SerializeField] private bool _changeLocalPosition;

        private SpringySpring _springySpring;

        private void Awake()
        {
            _springySpring = GetComponent<SpringySpring>();

            UpdatePosition();
        }

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var springValue = _springySpring.CurrentSpringValue;
            var newPosition = Vector3.LerpUnclamped(_minPosition, _maxPosition, springValue);

            if (_changeLocalPosition)
            {
                _transform.localPosition = newPosition;
            }
            else
            {
                _transform.position = newPosition;
            }
        }
    }
}