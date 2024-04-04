using UnityEngine;

namespace Springs.Constructor
{
    public class PositionSpringyChanger : BaseSpringyChanger
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _minPosition;
        [SerializeField] private Vector3 _maxPosition;
        [SerializeField] private bool _changeLocalPosition;

        protected override void UpdateSpringParameter(float currentSpringValue)
        {
            var newPosition = Vector3.LerpUnclamped(_minPosition, _maxPosition, currentSpringValue);

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