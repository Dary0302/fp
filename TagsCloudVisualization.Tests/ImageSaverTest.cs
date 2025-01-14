using FakeItEasy;
using NUnit.Framework;
using FluentAssertions;
using System.Runtime.InteropServices;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.Settings;
using TagsCloudVisualization.Savers;
using TagsCloudVisualization.Visualizatiuons;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class ImageSaverTest
{
    private CircularCloudLayouter cloudLayouter;
    private RectangleVisualizatiuon drawer;
    private ImageSaver imageSaver;

    [SetUp]
    public void SetUp()
    {
        #pragma warning disable CA1416
        var mockPositionGenerator = A.Fake<IPositionGenerator>();
        cloudLayouter = new CircularCloudLayouter(mockPositionGenerator);
        drawer = new RectangleVisualizatiuon(1500, 1500);
        imageSaver = new ImageSaver();
    }

    [TestCase(null)]
    [TestCase("")]
    public void CreateImage_OnInvalidParameters_ThrowsArgumentException(string filename)
    {
        drawer.CreateImage(cloudLayouter.Rectangles);
        var action = () => imageSaver.SaveImageToFile(drawer.Bitmap, new SaveSettings("Images", filename, "png"));
        action.Invoke().Error.Should().Be("Filename cannot be null or empty");
    }

    [TestCase("12\\")]
    [TestCase("@#$\\")]
    public void CreateImage_OnInvalidParameters_ThrowsDirectoryNotFoundException(string filename)
    {
        drawer.CreateImage(cloudLayouter.Rectangles);
        var action = () => imageSaver.SaveImageToFile(drawer.Bitmap, new SaveSettings("Images", filename, "png"));
        action.Should().Throw<DirectoryNotFoundException>();
    }

    [TestCase("abc|123")]
    [TestCase("123|abc")]
    [TestCase("123\n")]
    [TestCase("123\r")]
    public void CreateImage_OnInvalidParameters_ThrowsExternalException(string filename)
    {
        drawer.CreateImage(cloudLayouter.Rectangles);
        var action = () => imageSaver.SaveImageToFile(drawer.Bitmap, new SaveSettings("Images", filename, "png"));
        action.Should().Throw<ExternalException>();
        #pragma warning restore CA1416
    }
}