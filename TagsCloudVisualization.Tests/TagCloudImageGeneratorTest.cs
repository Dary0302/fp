using System.Drawing;
using System.Text;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Filters;
using TagsCloudVisualization.Generators;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.CloudLayouters;
using TagsCloudVisualization.Models.Settings;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Savers;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class TagCloudImageGeneratorTests
{
    [Test]
    public void GenerateCloud_ShouldMatchReferenceImage()
    {
        var saver = new ImageSaver();
        var saveSettings = new SaveSettings("ImagesTests", "test", "png");
        var textSettings = new TextSettings(8, 100);
        var textReaderSettings = new TextReaderSettings("TextTests/input.txt", Encoding.UTF8);
        var bitmapGenerator = new BitmapGenerator(
            new CircularCloudLayouter(
                new ArchimedeanSpiralPositionGenerator(new SpiralGeneratorSettings(40, 2, new Point(750, 750)))),
            new BitmapGeneratorSettings(new Size(1500, 1500), Color.Black, Color.Peru, FontFamily.GenericSansSerif));
        var txtTextReader = new TxtTextReader(textReaderSettings);
        var fileReadersSelectorMock = A.Fake<IFileReadersSelector>();
        A.CallTo(() => fileReadersSelectorMock.SelectFileReader()).Returns(txtTextReader);
        var filters = new List<ITextFilter> { new LowerCaseTextFilter() };

        var generator = new TagCloudImageGenerator(saver, saveSettings, textSettings, fileReadersSelectorMock,
            bitmapGenerator, filters);

        generator.GenerateCloud();

        #pragma warning disable CA1416
        var generatedImage = new Bitmap(@"ImagesTests\correctImage.png");
        var referenceImage = new Bitmap(@"ImagesTests\test.png");
        #pragma warning restore CA1416

        CompareImages(generatedImage, referenceImage).Should().BeTrue();
    }

    private static bool CompareImages(Bitmap img1, Bitmap img2)
    {
        #pragma warning disable CA1416
        if (img1.Width != img2.Width || img1.Height != img2.Height)
            return false;

        for (var y = 0; y < img1.Height; y++)
        {
            for (var x = 0; x < img1.Width; x++)
            {
                if (img1.GetPixel(x, y) != img2.GetPixel(x, y))
                {
                    return false;
                }
            }
        }
        #pragma warning restore CA1416
        return true;
    }
}