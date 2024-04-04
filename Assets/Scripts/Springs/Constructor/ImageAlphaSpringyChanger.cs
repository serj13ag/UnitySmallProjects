using UnityEngine;
using UnityEngine.UI;

namespace Springs.Constructor
{
    public class ImageAlphaSpringyChanger : BaseSpringyChanger
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _minAlpha;
        [SerializeField] private float _maxAlpha;

        protected override void UpdateSpringParameter(float currentSpringValue)
        {
            var newColor = _image.color;
            newColor.a = Mathf.Lerp(_minAlpha, _maxAlpha, currentSpringValue);

            _image.color = newColor;
        }
    }
}