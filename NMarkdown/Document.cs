using System.Text;

namespace NMarkdown
{
    public class Document : ElementContainer
    {
        public Document()
        {
            Document = this;
            Parent = null;
        }

        public Document(string header): this()
        {
            SubElements.Add(new Header(HeaderLevel.One, header));
        }

        public string GenerateMarkDown()
        {
            var md = new StringBuilder();
            this.SerializeTo(md);
            return md.ToString();
        }

        public void Add(Element element) => SubElements.Add(element);
    }
}
