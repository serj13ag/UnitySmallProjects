using UnityEngine;

namespace Springs.Constructor
{
    public class RotationSpringyChanger : BaseSpringyChanger
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _minRotation;
        [SerializeField] private Vector3 _maxRotation;
        [SerializeField] private bool _changeLocalRotation;

        protected override void UpdateSpringParameter(float currentSpringValue)
        {
            var newRotation = Vector3.LerpUnclamped(_minRotation, _maxRotation, currentSpringValue);

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