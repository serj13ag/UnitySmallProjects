using UnityEngine;

namespace Springs.Constructor
{
    [RequireComponent(typeof(SpringySpring))]
    public class SpringyScaleChanger : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _minScale;
        [SerializeField] private Vector3 _maxScale;

        private SpringySpring _springySpring;

        private void Awake()
        {
            _springySpring = GetComponent<SpringySpring>();

            UpdateScale();
        }

        private void Update()
        {
            UpdateScale();
        }

        private void UpdateScale()
        {
            var springValue = _springySpring.CurrentSpringValue;
            var newScale = Vector3.LerpUnclamped(_minScale, _maxScale, springValue);
            _transform.localScale = newScale;
        }
    }
}