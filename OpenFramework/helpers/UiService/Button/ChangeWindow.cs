using System;
using OpenFramework;
using UnityEngine;
using UnityEngine.UI;

namespace OpenFramework.Helper.UiService
{
    public class GenericChangeWindowButton<TWin, TMod> : MonoService
        where TWin : struct, IConvertible
        where TMod : struct, IConvertible
    {
        private Button _button;
        [SerializeField] private TWin windowType;
        private UiService<TWin, TMod> _uiService;

        void Awake()
        {
            _button = GetComponent<Button>();
        }

        void OnEnable()
        {
            _button.onClick.AddListener(Pressed);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(Pressed);
        }

        void Pressed()
        {
            Debug.Log("Showing window: " + windowType);
            if (_uiService == null)
                _uiService = (UiService<TWin, TMod>)context.GetService<IUiService>();
            if (_uiService != null)
                _uiService.ChangeWindow(windowType);
            else
                Debug.Log("Uimanager is not set");
        }
    }
}
