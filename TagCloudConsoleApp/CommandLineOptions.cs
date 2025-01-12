using CommandLine;

namespace TagCloudConsoleApp;

public class CommandLineOptions

{
    [Option("pathToBoringWords", Default = "BoringWords.txt", HelpText = "Path to exclude boring words")]
    public string? PathToBoringWords { get; set; }

    [Option('t', "pathToText", Default = "input.txt", HelpText = "Path to text for the words cloud")]
    public string? PathToText { get; set; }

    [Option('s', "pathToSaveDirectory", Default = "Images", HelpText = "Path to directory to save image")]
    public string? PathToSaveDirectory { get; set; }

    [Option('n', "fileName", Default = "image", HelpText = "Name of the file to save")]
    public string? FileName { get; set; }

    [Option("fileFormat", Default = "png", HelpText = "Extension file to save. Example: png")]
    public string? FileFormat { get; set; }

    [Option("center X coordinate", Default = 750, HelpText = "Center X coordinate of the spiral.")]
    public int CenterX { get; set; }

    [Option("center Y coordinate", Default = 750, HelpText = "Center Y coordinate of the spiral.")]
    public int CenterY { get; set; }

    [Option("stepIncreasingAngle", Default = 40, HelpText = "Delta angle for the spiral.")]
    public double StepIncreasingAngle { get; set; }

    [Option("stepIncreasingRadius", Default = 2, HelpText = "Delta radius for the spiral.")]
    public double StepIncreasingRadius { get; set; }

    [Option("imageWidth", Default = 1500, HelpText = "Image width")]
    public int ImageWidth { get; set; }

    [Option("imageHeight", Default = 1500, HelpText = "Image height")]
    public int ImageHeight { get; set; }

    [Option('b', "backgroundColor", Default = "black", HelpText = "Image background color.")]
    public string? BackgroundColor { get; set; }

    [Option("color", Default = "peru", HelpText = "Color of the words.")]
    public string? Color { get; set; }

    [Option("font", Default = "Arial", HelpText = "Font of the words")]
    public string? Font { get; set; }

    [Option("minFontSize", Default = 8, HelpText = "Minimum word font size")]
    public int MinFontSize { get; set; }

    [Option("maxFontSize", Default = 100, HelpText = "Maximum word font size")]
    public int MaxFontSize { get; set; }
}