using Autofac;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualization.Selectors;

public class FileReadersSelector(TextReaderSettings textReaderSettings, IComponentContext componentContext) : IFileReadersSelector
{
    public ITextReader SelectFileReader()
    {
        if (!File.Exists(textReaderSettings.Path))
        {
            throw new FileNotFoundException("File not found", textReaderSettings.Path);
        }

        var extension = Path.GetExtension(textReaderSettings.Path).ToLower();

        if (!componentContext.IsRegisteredWithKey<ITextReader>(extension))
        {
            throw new NotSupportedException($"File type {extension} is not supported.");
        }

        return componentContext.ResolveKeyed<ITextReader>(extension);
    }
}