using System.Text;
using System.Drawing;
using TagCloudConsoleApp.Interfaces;
using TagsCloudVisualization.Models.Settings;

namespace TagCloudConsoleApp.SettingsProvider;

public class SettingsProvider(CommandLineOptions options) : ISettingsProvider
{
    public SettingsManager GetSettings()
    {
        return new(new BitmapGeneratorSettings(new(options.ImageWidth, options.ImageHeight),
                GetColor(options.BackgroundColor!),
                GetColor(options.Color!), new(options.Font!)),
            new SaveSettings(options.PathToSaveDirectory!, options.FileName!, options.FileFormat!),
            new SpiralGeneratorSettings(options.StepIncreasingAngle, options.StepIncreasingRadius,
                new Point(options.CenterX, options.CenterY)),
            new TextReaderSettings(options.PathToText!, Encoding.UTF8),
            new TextSettings(options.MinFontSize, options.MaxFontSize),
            new BoringWordsSettings(options.PathToBoringWords!));
    }

    private static Color GetColor(string color) => Color.FromName(color);
}