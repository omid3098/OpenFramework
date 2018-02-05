namespace OpenFramework.Helper.UiService
{
    public class SampleUiSetting : UiManagerSetting
    {
        public override string canvasPath { get { return "ui/ui-canvas"; } }

        public override string windowPath { get { return "ui/window"; } }

        public override string modalPath { get { return "ui/modal"; } }

        public override string buttonPath { get { return "ui/button"; } }
    }
}
