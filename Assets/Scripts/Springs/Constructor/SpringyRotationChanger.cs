using UnityEngine;

namespace Springs.Constructor
{
    [RequireComponent(typeof(SpringySpring))]
    public class SpringyRotationChanger : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _minRotation;
        [SerializeField] private Vector3 _maxRotation;
        [SerializeField] private bool _changeLocalRotation;

        private SpringySpring _springySpring;

        private void Awake()
        {
            _springySpring = GetComponent<SpringySpring>();

            UpdateRotation();
        }

        private void Update()
        {
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            var springValue = _springySpring.CurrentSpringValue;
            var newRotation = Vector3.LerpUnclamped(_minRotation, _maxRotation, springValue);

            if (_changeLocalRotation)
            {
                _transform.localRotation = Quaternion.Euler(newRotation);
            }
            else
            {
                _transform.rotation = Quaternion.Euler(newRotation);
            }
        }
    }
}