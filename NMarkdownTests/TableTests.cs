using NUnit.Framework;
using System.Text;
using FluentAssertions;
using NMarkdown;

namespace NMarkdownTests
{
    public class TableTests
    {
        [Test]
        public void ASimpleTableTest()
        {
            // ARRANGE
            int rows = 5;
            int columns = 3;
            var table = new Table(rows, columns);
            table.Columns[0].Text.RawText = "1";
            table.Columns[0].TextAlignment = TextAlignment.Left;
            table.Columns[1].Text.RawText = "2";
            table.Columns[1].TextAlignment = TextAlignment.Center;
            table.Columns[2].Text.RawText = "3";
            table.Columns[2].TextAlignment = TextAlignment.Right;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    table[r, c].RawText = $"{r}-{c}";
                }
            }
            StringBuilder md = new StringBuilder();

            // ACT
            table.SerializeTo(md);

            // ASSERT
            var tableMd =
@"|  1  |  2  |  3  |
|:--- |:---:| ---:|
| 0-0 | 0-1 | 0-2 |
| 1-0 | 1-1 | 1-2 |
| 2-0 | 2-1 | 2-2 |
| 3-0 | 3-1 | 3-2 |
| 4-0 | 4-1 | 4-2 |
";
            md.ToString().Should().Be(tableMd);
        }
    }
}
