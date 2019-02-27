using EngineGL.Utils;
using NUnit.Framework;

namespace EngineGL.Tests.Utils
{
    [TestFixture]
    public class DialogTests
    {
        [Test]
        public void OpenDialog()
        {
            Dialog.Open("Title", "Message", Dialog.DialogType.ICON_ERROR);
        }
    }
}