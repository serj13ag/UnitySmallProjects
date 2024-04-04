using UnityEngine;

namespace Springs.Constructor
{
    [RequireComponent(typeof(SpringySpring))]
    public abstract class BaseSpringyChanger : MonoBehaviour
    {
        private SpringySpring _springySpring;

        private void Awake()
        {
            _springySpring = GetComponent<SpringySpring>();

            UpdateSpringParameter(_springySpring.CurrentSpringValue);
        }

        private void Update()
        {
            UpdateSpringParameter(_springySpring.CurrentSpringValue);
        }

        protected abstract void UpdateSpringParameter(float currentSpringValue);
    }
}