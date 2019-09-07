using System;
using System.Text;

namespace NMarkdown
{
    public class CodeBlock: Element
    {
        public string Language;
        public string Code;

        public CodeBlock(string language, string code)
        {
            Language = language;
            Code = code;
        }

        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append(Environment.NewLine);
            markdown.Append(Environment.NewLine);
            markdown.Append("``` ");
            markdown.Append(Language);
            markdown.Append(Environment.NewLine);
            markdown.Append(Code);
            markdown.Append(Environment.NewLine);
            markdown.Append("```");
            markdown.Append(Environment.NewLine);
        }
    }

    public static class CodeBlockDocumentExtensions
    {
        public static CodeBlock AddCode(this Document doc, string language, string code = "")
        {
            var ret = new CodeBlock(language, code);
            doc.SubElements.Add(ret);
            return ret;
        }

        public static CodeBlock AddC(this Document doc, string code = "") => AddCode(doc, "c", code);

        public static CodeBlock AddCSharp(this Document doc, string code = "") => AddCode(doc, "c#", code);

        public static CodeBlock AddJavaScript(this Document doc, string code = "") => AddCode(doc, "js", code);

        public static CodeBlock AddCss(this Document doc, string code = "") => AddCode(doc, "css", code);

        public static CodeBlock AddJava(this Document doc, string code = "") => AddCode(doc, "java", code);

        public static CodeBlock AddMarkdown(this Document doc, string code = "") => AddCode(doc, "markdown", code);

        public static CodeBlock AddXml(this Document doc, string code = "") => AddCode(doc, "xml", code);
    }
}
