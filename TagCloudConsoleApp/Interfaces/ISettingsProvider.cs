using TagsCloudVisualization.Models.Settings;

namespace TagCloudConsoleApp.Interfaces;

public interface ISettingsProvider
{
    public SettingsManager GetSettings();
}