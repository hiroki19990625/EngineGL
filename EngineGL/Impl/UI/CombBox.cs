using System.Linq;
using EngineGL.Core;

namespace EngineGL.Impl.UI
{
    public class CombBox: IElement
    {
        public string[] SelectList { get; }
        public string Selected { get; set; }

        public void Append(string select)
        {
            SelectList.Append(select);
        }
    }
}