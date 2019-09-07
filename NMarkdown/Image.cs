using System;
using System.Text;

namespace NMarkdown
{
    public class Image : Element
    {
        public Image(string alternativeText, string url)
        {
            AlternativeText = alternativeText;
            Url = url;
        }

        public string Url;

        public string AlternativeText;

        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append("![");
            markdown.Append(AlternativeText);
            markdown.Append("](");
            markdown.Append(Url);
            markdown.Append(")");
            markdown.Append(Environment.NewLine);
        }
    }

    public static class ImageTextExtensions
    {
        public static Image Image(this Text text, string alternativeText, string url)
        {
            var ret = new Image(alternativeText, url);
            text.Parent.SubElements.Add(ret);
            return ret;
        }
    }

    public static class ImageDocumentExtensions
    {
        public static Image Image(this Document doc, string alternativeText, string url)
        {
            var ret = new Image(alternativeText, url);
            doc.SubElements.Add(ret);
            return ret;
        }
    }
}
