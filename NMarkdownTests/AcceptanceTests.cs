using FluentAssertions;
using NMarkdown;
using NUnit.Framework;
using System;
using System.IO;

namespace NMarkdownTests
{
    public class AcceptanceTests
    {
        [Test]
        public void CreateReadmeTest()
        {
            // ARRANGE
            var doc = new Document("NMarkdown");
            doc.Text("It's a quite simple fluent library in ").Bold("C#").Regular(" to compose ").Bold("programmatically").Regular(" a mark down document.");
            doc.NewLine();

            doc.AddHeader2("An example");
            doc.Text("This code builds this document without the example paragraph (otherwise it will produce an infinite recursion :-)).");
            doc.AddCSharp(
@"var doc = new Document(""NMarkdown"");
doc.Text(""It's a quite simple fluent library in "").Bold(""C#"").Regular("" to compose "")
    .Bold(""programmatically"").Regular("" a mark down document."")
    .NewLine();

doc.AddHeader2(""License"");
doc.Text(""NMarkdown is released under the "").Link(""MIT license"", ""License.txt"")
    .Regular(""."").NewLine();
doc.Text(""This license allow the use of NMarkdown in free and commercial applications and libraries without restrictions."")
    .NewLine();");

            doc.AddHeader2("License");
            doc.Text("NMarkdown is released under the ").Link("MIT license", "License.txt").Regular(".").NewLine();
            doc.Text("This license allow the use of NMarkdown in free and commercial applications and libraries without restrictions.");
            doc.NewLine();

            // ACT
            var md = doc.GenerateMarkDown();

            // ASSERT
            md.Should().Be(File.ReadAllText("../../../../NMarkdown/Readme.md"));
        }

        [Test]
        public void TableTest()
        {
            // ARRANGE
            var doc = new Document();
            doc.Table(
                new string[] { "A", "B", "C" },
                new TextAlignment[] { TextAlignment.Left, TextAlignment.Center, TextAlignment.Right },
                new string[,] { { "1", "1", "1" }, { "2", "2", "2" } });

            // ACT
            var md = doc.GenerateMarkDown();

            // ASSERT
            md.Should().Be(
@"| A | B | C |
|:- |:-:| -:|
| 1 | 1 | 1 |
| 2 | 2 | 2 |
"
);
        }
    }
}
