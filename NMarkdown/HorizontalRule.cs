using System;
using System.Text;

namespace NMarkdown
{
    public class HorizontalRule : Element
    {
        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append(Environment.NewLine);
            markdown.Append("***");
            markdown.Append(Environment.NewLine);
        }
    }

    public static class HorizontalRuleDocumentExtensions
    {
        public static HorizontalRule HorizontalRule(this Document doc)
        {
            var ret = new HorizontalRule();
            doc.SubElements.Add(ret);
            return ret;
        }
    }
}
