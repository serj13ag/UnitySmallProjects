using UnityEngine;
using UnityEngine.UI;

namespace Springs.Constructor
{
    [RequireComponent(typeof(SpringySpring))]
    public class SpringyImageAlphaChanger : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _minAlpha;
        [SerializeField] private float _maxAlpha;

        private SpringySpring _springySpring;

        private void Awake()
        {
            _springySpring = GetComponent<SpringySpring>();

            UpdateAlpha();
        }

        private void Update()
        {
            UpdateAlpha();
        }

        private void UpdateAlpha()
        {
            var springValue = _springySpring.CurrentSpringValue;

            var newColor = _image.color;
            newColor.a = Mathf.Lerp(_minAlpha, _maxAlpha, springValue);

            _image.color = newColor;
        }
    }
}