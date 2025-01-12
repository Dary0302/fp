namespace TagsCloudVisualization.Interfaces;

public interface ITextFilter
{
    public IEnumerable<string> ApplyFilter(IEnumerable<string> text);
}