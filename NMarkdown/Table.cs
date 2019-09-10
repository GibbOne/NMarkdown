using System;
using System.Collections.Generic;
using System.Text;

namespace NMarkdown
{
    public class Table: Element
    {
        /// <summary>
        /// Columns and column headers. Data can be set by indexer [row, col].
        /// </summary>
        public IReadOnlyList<TableColumn> Columns { get; } = new List<TableColumn>();
        public int RowCount => cells.Count;
        public int ColumnCount => Columns.Count;

        private List<List<Text>> cells = new List<List<Text>>();

        /// <summary>
        /// Create a table
        /// </summary>
        /// <param name="rows">Number of data row (excluding header of columns)</param>
        /// <param name="columns">Number of columns</param>
        public Table(int rows, int columns)
        {
            cells = new List<List<Text>>();

            for (int i = 0; i < columns; i++)
            {
                (Columns as List<TableColumn>).Add(new TableColumn(i));
            }

            for (int r = 0; r < rows; r++)
            {
                var row = new List<Text>();
                cells.Add(row);
                for (int c = 0; c < columns; c++)
                {
                    row.Add(new Text());
                }
            }
        }

        /// <summary>
        /// Data of table
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Text this[int row, int col]
        {
            get => cells[row][col];
        }

        private string RemovePipeFromText(string originalText) => originalText.Replace("|", "/");

        public override void SerializeTo(StringBuilder markdown)
        {
            // generate header first row
            int[] columnWidths = new int[ColumnCount];
            foreach (var column in Columns)
            {
                markdown.Append("| ");
                var maxWidth = GetMaxWidthForColumn(column.Index);
                if ((maxWidth - column.Text.RawTextWidth) % 2 != 0) maxWidth++;
                columnWidths[column.Index] = maxWidth;
                markdown.Append(new string(' ', (maxWidth - column.Text.RawTextWidth) / 2));
                column.Text.RawText = RemovePipeFromText(column.Text.RawText);
                column.Text.SerializeTo(markdown);
                markdown.Append(new string(' ', (maxWidth - column.Text.RawTextWidth) / 2));
                markdown.Append(" ");
            }
            markdown.Append("|");
            markdown.Append(Environment.NewLine);

            // generate header second row
            foreach (var column in Columns)
            {
                markdown.Append("|");
                if (column.TextAlignment == TextAlignment.Left || column.TextAlignment == TextAlignment.Center)
                    markdown.Append(":");
                else markdown.Append(" ");
                markdown.Append(new string('-', columnWidths[column.Index]));
                if (column.TextAlignment == TextAlignment.Right || column.TextAlignment == TextAlignment.Center)
                    markdown.Append(":");
                else markdown.Append(" ");
            }
            markdown.Append("|");
            markdown.Append(Environment.NewLine);

            // generate data rows of table
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    markdown.Append("| ");
                    cells[r][c].RawText = RemovePipeFromText(cells[r][c].RawText);
                    cells[r][c].SerializeTo(markdown);
                    markdown.Append(new string(' ', columnWidths[c] - cells[r][c].RawTextWidth));
                    markdown.Append(" ");
                }
                markdown.Append("|");
                markdown.Append(Environment.NewLine);
            }
        }

        private int GetMaxWidthForColumn(int columnIndex)
        {
            var maxWidth = Columns[columnIndex].Text.RawTextWidth;
            for (int r = 0; r < RowCount; r++)
            {
                var columnWidth = cells[r][columnIndex].RawTextWidth;
                if (maxWidth < columnWidth)
                    maxWidth = columnWidth;
            }
            return maxWidth;
        }
    }

    public static class TableDocumentExtensions
    {
        public static Table Table(this Document document, string[] columnHeaders, TextAlignment[] columnAlignments, string[,] data)
        {
            if (columnAlignments.Length != columnHeaders.Length || columnAlignments.Length != data.GetLength(1))
                throw new Exception("Number of columns should be the same in all arguments");

            var ret = new Table(data.GetLength(0), data.GetLength(1));
            for (int c = 0; c < ret.ColumnCount; c++)
            {
                ret.Columns[c].TextAlignment = columnAlignments[c];
                ret.Columns[c].Text.RawText = columnHeaders[c];

                for (int r = 0; r < ret.RowCount; r++)
                {
                    ret[r, c].RawText = data[r, c];
                }
            }

            document.SubElements.Add(ret);
            return ret;
        }
    }
}
