using UnityEngine;

namespace Springs.Constructor
{
    [RequireComponent(typeof(SpringySpring))]
    public abstract class BaseSpringyChanger : MonoBehaviour
    {
        [SerializeField] private bool _mirrorFromCenter;

        private SpringySpring _springySpring;

        private void Awake()
        {
            _springySpring = GetComponent<SpringySpring>();

            UpdateSpringParameter(GetCurrentSpringValue());
        }

        private void Update()
        {
            UpdateSpringParameter(GetCurrentSpringValue());
        }

        protected abstract void UpdateSpringParameter(float currentSpringValue);

        private float GetCurrentSpringValue()
        {
            var currentSpringValue = _springySpring.CurrentSpringValue;

            if (_mirrorFromCenter)
            {
                currentSpringValue = currentSpringValue < 0.5f
                    ? Mathf.InverseLerp(0f, 0.5f, currentSpringValue)
                    : 1f - Mathf.InverseLerp(0.5f, 1f, currentSpringValue);
            }

            return currentSpringValue;
        }
    }
}