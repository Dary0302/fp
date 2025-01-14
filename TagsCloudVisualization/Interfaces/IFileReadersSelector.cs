using TagsCloudVisualization.Models;

namespace TagsCloudVisualization.Interfaces;

public interface IFileReadersSelector
{
    public Result<ITextReader> SelectFileReader();
}