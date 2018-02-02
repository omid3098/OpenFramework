using OpenFramework.Helper.UiService;
using OpenUi;

namespace OpenUi.Sample
{
    public class CloseParentModal : FormButton
    {
        protected override void Pressed()
        {
            transform.parent.GetComponent<Modal<SampleModalType>>().Hide();
        }
    }
}