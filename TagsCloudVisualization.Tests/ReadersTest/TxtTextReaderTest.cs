using System.Text;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualizationTests.ReadersTest;

[TestFixture]
public class TxtTextReaderTest
{
    private TextReaderSettings textReaderSettings;
    private TxtTextReader txtTextReader;
    
    [SetUp]
    public void SetUp()
    {
        textReaderSettings = new TextReaderSettings(@"TextTests\Txt.txt", Encoding.UTF8);
        txtTextReader = new TxtTextReader(textReaderSettings);
    }
    
    [Test]
    public void ReadText()
    {
        var result = txtTextReader.ReadText();
        result.Should().BeEquivalentTo(new List<string> { "test", "read", "text", "from", "txt", "file" });
    }
}