using UnityEngine;
using UnityEngine.UI;

namespace Springs
{
    public class SpringyButtonScaler : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private float _scaleDeltaOnClick;
        [SerializeField] private float _frequency;
        [SerializeField] private float _damping;

        private float _initialScaleX;
        private float _initialScaleY;

        private SpringyMotionParams _springyMotionParams;
        private float _currentVelocity;
        private float _deltaScale;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void Awake()
        {
            _springyMotionParams = new SpringyMotionParams();

            var localScale = transform.localScale;
            _initialScaleX = localScale.x;
            _initialScaleY = localScale.y;
        }

        private void Update()
        {
            SpringyUtils.CalcDampedSpringMotionParams(ref _springyMotionParams, Time.deltaTime, _frequency, _damping);
            SpringyUtils.UpdateDampedSpringMotion(ref _deltaScale, ref _currentVelocity, 0f, _springyMotionParams);

            transform.localScale = new Vector3(_initialScaleX + _deltaScale, _initialScaleY + _deltaScale, 1f);
        }

        private void OnButtonClick()
        {
            _deltaScale = _scaleDeltaOnClick;
        }
    }
}