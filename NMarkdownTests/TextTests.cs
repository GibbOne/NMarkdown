using FluentAssertions;
using NMarkdown;
using NUnit.Framework;

namespace NMarkdownTests
{
    public class TextTests
    {
        [Test]
        public void NullTextWidthTest()
        {
            // ARRANGE
            var text = new Text();
            text.RawText = null;

            // ACT
            text.RawTextWidth.Should().Be(0);
        }
    }
}
