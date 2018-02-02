using OpenFramework.Helper.UiService;
namespace OpenUi.Sample
{
    public class SampleUiManagerSetting : UiManagerSetting
    {
        public override string canvasPath { get { return "ui-canvas"; } }

        public override string windowPath { get { return "window"; } }

        public override string modalPath { get { return "modal"; } }

        public override string buttonPath { get { return "button"; } }
    }
}