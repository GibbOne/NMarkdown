using System.Text;

namespace NMarkdown
{
    public abstract class Element
    {
        public IElementContainer Parent { get; internal set; } = null;
        public Document Document { get; internal set; }

        public abstract void SerializeTo(StringBuilder markdown);
    }
}
