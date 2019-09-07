using System.Text;

namespace NMarkdown
{
    public class BlockQuote: ElementContainer
    {
        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append("> ");
            foreach (var element in SubElements)
            {
                element.SerializeTo(markdown);
                if (element is ParagraphSeparator)
                {
                    markdown.Append("> ");
                }
            }
        }

        private BlockQuote AddElement(Element element)
        {
            this.SubElements.Add(element);
            return this;
        }

        public BlockQuote Regular(string text) => AddElement(new Text(text, TextStyle.Regular));
        public BlockQuote Bold(string text) => AddElement(new Text(text, TextStyle.Bold));
        public BlockQuote Italic(string text) => AddElement(new Text(text, TextStyle.Italic));
        public BlockQuote BoldItalic(string text) => AddElement(new Text(text, TextStyle.Bold | TextStyle.Italic));

        private BlockQuote Link(string text, string url) => AddElement(new Link(text, url));
        private BlockQuote Image(string alternativeText, string url) => AddElement(new Image(alternativeText, url));
        private BlockQuote Code(string language, string code) => AddElement(new CodeBlock(language, code));
    }

    public static class BlockQuoteDocumentExtensions
    {
        public static BlockQuote Quote(this Document doc)
        {
            var ret = new BlockQuote();
            doc.SubElements.Add(ret);
            return ret;
        }
    }

    public static class BlockQuoteTextExtensions
    {
        public static BlockQuote Quote(this Text text)
        {
            var ret = new BlockQuote();
            text.Parent.SubElements.Add(ret);
            return ret;
        }
    }
}
