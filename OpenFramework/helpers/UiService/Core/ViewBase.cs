using UnityEngine;

namespace OpenFramework.Helper.UiService
{
    [RequireComponent(typeof(ShowTransition))]
    [RequireComponent(typeof(HideTransition))]
    public class ViewBase : MonoService
    {
        protected ShowTransition showTransition;
        protected HideTransition hideTransition;

        protected virtual void Awake()
        {
            showTransition = GetComponent<ShowTransition>();
            hideTransition = GetComponent<HideTransition>();
        }
    }
}