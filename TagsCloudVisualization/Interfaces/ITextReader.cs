namespace TagsCloudVisualization.Interfaces;

public interface ITextReader
{
    public IEnumerable<string> ReadText();
    public IEnumerable<string> ReadText(string path);
}