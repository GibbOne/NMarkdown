using System;
using System.Collections.Generic;
using System.Text;

namespace NMarkdown
{
    public class InLineCode: Element
    {
        public string Language;
        public string Code;

        public override void SerializeTo(StringBuilder markdown)
        {
            markdown.Append("``` ");
            markdown.Append(Language);
            markdown.Append(Environment.NewLine);
            markdown.Append(Code);
            markdown.Append(Environment.NewLine);
            markdown.Append("```");
            markdown.Append(Environment.NewLine);
        }
    }
}
