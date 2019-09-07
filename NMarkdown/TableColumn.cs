namespace NMarkdown
{
    public enum TextAlignment
    {
        Left = 0,
        Center = 1,
        Right
    }

    public class TableColumn
    {
        public int Index { get; }
        public TextAlignment TextAlignment;
        public Text Text = new Text();

        public TableColumn(int index)
        {
            Index = index;
        }
    }
}
