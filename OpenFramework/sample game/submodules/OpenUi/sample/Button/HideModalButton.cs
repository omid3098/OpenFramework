using OpenUi;
using UnityEngine;

namespace OpenUi.Sample
{
    public class HideModalButton : UiButton
    {
        [SerializeField] private SampleModalType modalType;
        private UiManager<SampleWindowType, SampleModalType> uiManager;

        override protected void Pressed()
        {
            base.Pressed();
            if (uiManager == null) uiManager = SampleGame.uiManager;
            if (uiManager != null) uiManager.HideModal(modalType);
            else Debug.Log("Uimanager is not set");
        }
    }
}