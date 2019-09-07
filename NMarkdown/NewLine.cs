using System;
using System.Collections.Generic;
using System.Text;

namespace NMarkdown
{
    public class NewLine : Element
    {
        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append(Environment.NewLine);
        }
    }

    public static class NewLineOnSourceDocumentExtensions
    {
        public static void NewLine(this Document doc)
        {
            doc.SubElements.Add(new NewLine());
        }
    }

    public static class NewLineOnSourceTextExtensions
    {
        public static void NewLine(this Text text)
        {
            text.Parent.SubElements.Add(new NewLine());
        }
    }
}