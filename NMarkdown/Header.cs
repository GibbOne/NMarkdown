using System;
using System.Text;

namespace NMarkdown
{
    public enum HeaderLevel: int
    {
        // numeric values should not be changed. It's used by serialization...
        One = 1,
        Two = 2,
        Three = 3
    }

    public class Header : Element
    {
        public HeaderLevel Level;
        public string Text;

        public Header(HeaderLevel level, string text)
        {
            Level = level;
            Text = text;
        }

        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append(Environment.NewLine);
            markdown.Append(new String('#', (int)Level));
            markdown.Append(" ");
            markdown.Append(Text);
            markdown.Append(Environment.NewLine);
            markdown.Append(Environment.NewLine);
        }
    }

    public static class HeaderDocumentExtensions
    {
        public static Header AddHeader(this Document doc,
            HeaderLevel level, string text = "")
        {
            var ret = new Header(level, text);
            doc.SubElements.Add(ret);
            return ret;
        }

        public static Header AddHeader1(this Document doc, string text = "")
        {
            return doc.AddHeader(HeaderLevel.One, text);
        }

        public static Header AddHeader2(this Document doc, string text = "")
        {
            return doc.AddHeader(HeaderLevel.Two, text);
        }

        public static Header AddHeader3(this Document doc, string text = "")
        {
            return doc.AddHeader(HeaderLevel.Three, text);
        }

    }
}
