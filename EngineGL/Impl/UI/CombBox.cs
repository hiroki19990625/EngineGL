using System.Linq;

namespace EngineGL.Impl.UI
{
    public class CombBox : Element
    {
        public string[] SelectList { get; }
        public string Selected { get; set; }

        public void Append(string select)
        {
            SelectList.Append(select);
        }
    }
}