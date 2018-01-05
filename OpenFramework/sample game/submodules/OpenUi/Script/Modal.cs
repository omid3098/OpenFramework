using System;
using OpenUi;
using UnityEngine;

namespace OpenUi
{
    public class Modal<T> : ViewBase
        where T : struct, IConvertible
    {
        public T modalType;
        private RectTransform _rectTransform;
        [SerializeField] bool _hideIfClickOutside;

        #region Methods
        protected override void Awake()
        {
            base.Awake();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Hide(Action onComplete = null)
        {
            if (hideTransition != null) hideTransition.Play(_onPlayCallback: onComplete);
            else Debug.LogError("There is no hideTransition component on this window.");
        }

        public void Show(Action onComplete)
        {
            if (showTransition != null) showTransition.Play(_onPlayCallback: onComplete);
            else Debug.LogError("There is no showTransition component on this window.");
        }
        public void Show()
        {
            Show(null);
        }

        private void HideIfClickedOutside()
        {
            if (Input.GetMouseButton(0) &&
                    !RectTransformUtility.RectangleContainsScreenPoint(
                    _rectTransform,
                    Input.mousePosition,
                    Camera.main))
            {
                Hide();
            }
        }

        void Update()
        {
            if (_hideIfClickOutside) HideIfClickedOutside();
        }
        #endregion
    }
}