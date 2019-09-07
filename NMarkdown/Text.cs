using System;
using System.Text;

namespace NMarkdown
{
    [Flags]
    public enum TextStyle
    {
        Regular,
        Bold,
        Italic
    }

    public class Text: Element
    {
        public TextStyle Style;
        public string RawText = "";

        public Text(string rawText = "", TextStyle style = TextStyle.Regular)
        {
            RawText = rawText;
            Style = style;
        }

        public bool IsBold => (Style & TextStyle.Bold) == TextStyle.Bold;
        public bool IsItalic => (Style & TextStyle.Italic) == TextStyle.Italic;

        public override void SerializeTo(StringBuilder markdown)
        {
            var bold = IsBold;
            var italic = IsItalic;

            if (italic) markdown.Append("_");
            if (bold) markdown.Append("**");
            markdown.Append(RawText);
            if (bold) markdown.Append("**");
            if (italic) markdown.Append("_");
        }

        private Text Add(string rawText, TextStyle style)
        {
            var ret = new Text(rawText, style);
            Parent.SubElements.Add(ret);
            return ret;
        }

        public Text Regular(string text = "")
        {
            return Add(text, TextStyle.Regular);
        }

        public Text Bold(string text = "")
        {
            return Add(text, TextStyle.Bold);
        }

        public Text Italic(string text = "")
        {
            return Add(text, TextStyle.Italic);
        }

        public Text BoldItalic(string text = "")
        {
            return Add(text, TextStyle.Bold | TextStyle.Italic);
        }

        public int RawTextWidth
        {
            get
            {
                int additionalLen = 0;
                if (IsBold)
                    additionalLen += 4;
                if (IsItalic)
                    additionalLen += 2;
                return RawText.Length + additionalLen; 
            }
        }
    }

    public static class TextDocumentExtensions
    {
        public static Text Text(this Document doc, 
            string text = "", TextStyle style = TextStyle.Regular)
        {
            var ret = new Text(text, style);
            doc.SubElements.Add(ret);
            return ret;
        }
    }

    public static class TextLinkExtensions
    {
        private static Text Add(this Link reference,
            string text = "", TextStyle style = TextStyle.Regular)
        {
            var ret = new Text(text, style);
            reference.Parent.SubElements.Add(ret);
            return ret;
        }

        public static Text Regular(this Link reference, string text = "")
        {
            return reference.Add(text, TextStyle.Regular);
        }

        public static Text Bold(this Link reference, string text = "")
        {
            return reference.Add(text, TextStyle.Bold);
        }

        public static Text Italic(this Link reference, string text = "")
        {
            return reference.Add(text, TextStyle.Italic);
        }

        public static Text BoldItalic(this Link reference, string text = "")
        {
            return reference.Add(text, TextStyle.Bold | TextStyle.Italic);
        }
    }
}
