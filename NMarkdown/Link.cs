using System;
using System.Text;

namespace NMarkdown
{
    public class Link : Element
    {
        private readonly string Url;

        public Link(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public string Text { get; }

        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append("[");
            markdown.Append(Text);
            markdown.Append("](");
            markdown.Append(Url);
            markdown.Append(")");
        }
    }

    public static class LinkTextExtensions
    {
        public static Link Link(this Text text, string label, string url)
        {
            var ret = new Link(label, url);
            text.Parent.SubElements.Add(ret);
            return ret;
        }
    }
}
