using System;
using System.Collections;
using UnityEngine;
using OpenFramework.Helper.UiService;

namespace OpenUi.Sample
{
    public class SampleGame : MonoBehaviour
    {
        public static SampleUiManager uiManager;

        void Awake()
        {
            UiManagerSetting setting = new SampleUiManagerSetting();
            uiManager = new SampleUiManager(setting);
            uiManager.Init();
            StartCoroutine(ChangeToMainMenu());
        }

        private IEnumerator ChangeToMainMenu()
        {
            yield return new WaitForSeconds(1);
            uiManager.ChangeWindow(SampleWindowType.MainMenu);
        }
    }
}