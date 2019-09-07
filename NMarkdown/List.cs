using System;
using System.Text;

namespace NMarkdown
{
    public class List: ElementContainer
    {

        public override void SerializeTo(StringBuilder markdown)
        {
            var md = new StringBuilder();
            int itemCounter = 0;
            var nestingStringPrefix = new string(' ', GetNestingLevel());
            foreach (var element in SubElements)
            {
                itemCounter++;
                md.Append(nestingStringPrefix);
                if (element is ListItem listItemElement && listItemElement.UseNumber)
                {
                    md.Append(itemCounter);
                    md.Append(". ");
                }
                else
                {
                    md.Append("- ");
                }
                element.SerializeTo(md);
                md.Append(Environment.NewLine);
            }
        }

        private int GetNestingLevel()
        {
            int nesting = 0;
            Element element = this;
            while (true)
            {
                if (element.Parent is Element parent)
                {
                    element = parent;
                    nesting++;
                }
                else
                    break;
            }
            return nesting;
        }
    }
}
