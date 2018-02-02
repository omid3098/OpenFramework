namespace OpenFramework.Helper.UiService
{
    public abstract class UiButton : ViewBase
    {
        UnityEngine.UI.Button button;
        override protected void Awake()
        {
            base.Awake();
            button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(Pressed);
        }

        protected abstract void Pressed();
    }
}