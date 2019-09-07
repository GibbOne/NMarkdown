using System;
using System.Text;

namespace NMarkdown
{
    public class ParagraphSeparator : Element
    {
        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append(Environment.NewLine);
            markdown.Append(Environment.NewLine);
        }
    }

    public static class ParagraphSeparatorDocumentExtensions
    {
        public static void NewParagraph(this Document doc)
        {
            doc.SubElements.Add(new ParagraphSeparator());
        }
    }

    public static class ParagraphSeparatorTextExtensions
    {
        public static void NewParagraph(this Text text)
        {
            text.Parent.SubElements.Add(new ParagraphSeparator());
        }
    }
}
