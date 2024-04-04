using UnityEngine;

namespace Springs.Constructor
{
    public class ScaleSpringyChanger : BaseSpringyChanger
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _minScale;
        [SerializeField] private Vector3 _maxScale;

        protected override void UpdateSpringParameter(float currentSpringValue)
        {
            var newScale = Vector3.LerpUnclamped(_minScale, _maxScale, currentSpringValue);
            _transform.localScale = newScale;
        }
    }
}