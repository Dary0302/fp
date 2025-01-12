namespace TagsCloudVisualization.Models.Settings;

public record SettingsManager(
    BitmapGeneratorSettings BitmapGeneratorSettings,
    SaveSettings SaveSettings,
    SpiralGeneratorSettings SpiralGeneratorSettings,
    TextReaderSettings TextReaderSettings,
    TextSettings TextSettings,
    BoringWordsSettings BoringWordsSettings);