using OpenUi;
using UnityEngine;

namespace OpenUi.Sample
{
    public class ChangeWindowButton : UiButton
    {
        [SerializeField] private SampleWindowType windowType;
        private UiManager<SampleWindowType, SampleModalType> uiManager;
        override protected void Pressed()
        {
            base.Pressed();
            Debug.Log("Showing window: " + windowType);
            if (uiManager == null) uiManager = SampleGame.uiManager;
            if (uiManager != null) uiManager.ChangeWindow(windowType);
            else Debug.Log("Uimanager is not set");
        }
    }
}