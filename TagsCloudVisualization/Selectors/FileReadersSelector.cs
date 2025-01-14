using Autofac;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Selectors;

public class FileReadersSelector(TextReaderSettings textReaderSettings, IComponentContext componentContext) : IFileReadersSelector
{
    public Result<ITextReader> SelectFileReader()
    {
        if (!File.Exists(textReaderSettings.Path))
        {
            return Result.Fail<ITextReader>($"The file was not found on the path: {textReaderSettings.Path}");
        }

        var extension = Path.GetExtension(textReaderSettings.Path).ToLower();

        if (!componentContext.IsRegisteredWithKey<ITextReader>(extension))
        {
            return Result.Fail<ITextReader>($"File type {extension} is not supported.");
        }

        return Result.Ok(componentContext.ResolveKeyed<ITextReader>(extension));
    }
}