using System.Collections.Generic;

namespace NMarkdown
{
    public interface IElementContainer
    {
        IList<Element> SubElements { get; }
    }
}
