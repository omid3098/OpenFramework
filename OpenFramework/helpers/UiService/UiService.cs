namespace OpenFramework.Helper.UiService
{
    using System;
    using System.Collections;
    using OpenFramework;
    using UnityEngine;

    public abstract class UiService<TWin, TMod> : IUiService
        where TWin : struct, IConvertible
        where TMod : struct, IConvertible
    {
        private UiManager<TWin, TMod> _uiService;

        public GameContext context { get; set; }
        public bool ready { get; set; }
        public Canvas canvas { get; set; }
        private UiManagerSetting setting;
        
        public IEnumerator Init()
        {
            setting = SetupUiSetting();
            _uiService = new UiManager<TWin, TMod>(setting);
            _uiService.Init();
            _uiService.canvas.transform.SetParent(context.transform, false);
            canvas = _uiService.canvas;
            PostInit();
            yield return 0;
        }
        protected abstract void PostInit();

        protected abstract UiManagerSetting SetupUiSetting();

        public Window<TWin, TMod> ChangeWindow(TWin type, Action OnComplete = null)
        {
            return _uiService.ChangeWindow(type, OnComplete);
        }

        public Modal<TMod> HideModal(TMod type, Action OnComplete = null)
        {
            return _uiService.HideModal(type);
        }

        public Modal<TMod> ShowModal(TMod type, Action OnComplete = null)
        {
            return _uiService.ShowModal(type, OnComplete);
        }

        public void StartService()
        {
            throw new System.NotImplementedException();
        }

        public void StopService()
        {
            throw new System.NotImplementedException();
        }
    }
}
