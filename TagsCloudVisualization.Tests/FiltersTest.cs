using FakeItEasy;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudVisualization.Filters;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models.Settings;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class FiltersTest
{
    private ITextReader textReaderMock;
    private BoringWordsSettings boringWordsSettingsMack;

    [SetUp]
    public void SetUp()
    {
        textReaderMock = A.Fake<ITextReader>();
        boringWordsSettingsMack = A.Fake<BoringWordsSettings>();
    }

    [Test]
    public void Filters_CorrectOperationWhenCombiningFilters()
    {
        A.CallTo(() => textReaderMock.ReadText(boringWordsSettingsMack.Path))
            .Returns(new List<string> { "a", "the", "hello" });

        var filters = new List<ITextFilter>
        {
            new BoringWordsTextFilter(boringWordsSettingsMack, textReaderMock), new LowerCaseTextFilter()
        };

        var words = new List<string> { "a", "the", "hello", "WoRlD", "hI" };
        var result = filters.Aggregate(words.AsEnumerable(),
            (currentWords, filter) => filter.ApplyFilter(currentWords));

        result.Should().BeEquivalentTo(new List<string> { "world", "hi" });
    }

    [TestCase("Hello", "World", "hello", "world")]
    [TestCase("HELLO", "WORLD", "hello", "world")]
    [TestCase("hello", "world", "hello", "world")]
    [TestCase("123", "456", "123", "456")]
    [TestCase("Hello", "123", "hello", "123")]
    [TestCase("Русский", "Текст", "русский", "текст")]
    [TestCase("русский", "текст", "русский", "текст")]
    public void LowerCaseTextFilter_CorrectOperationWithDifferentWords(
        string firstWord,
        string secondWord,
        string resultFirstWord,
        string resultSecondWord)
    {
        var filter = new LowerCaseTextFilter();
        var words = new List<string> { firstWord, secondWord };
        var result = filter.ApplyFilter(words);

        result.Should().BeEquivalentTo(new List<string> { resultFirstWord, resultSecondWord });
    }

    [TestCase("a", "the", "hello", "world")]
    [TestCase("123", "456", "hello", "world")]
    [TestCase("русский", "текст", "был", "убран")]
    public void BoringWordsTextFilter_CorrectOperationWithDifferentWords(
        string firstBoringWord,
        string secondBoringWord,
        string firstWord,
        string secondWord)
    {
        A.CallTo(() => textReaderMock.ReadText(boringWordsSettingsMack.Path))
            .Returns(new List<string> { firstBoringWord, secondBoringWord });

        var filter = new BoringWordsTextFilter(boringWordsSettingsMack, textReaderMock);
        var words = new List<string> { firstBoringWord, secondBoringWord, firstWord, secondWord };
        var result = filter.ApplyFilter(words);

        result.Should().BeEquivalentTo(new List<string> { firstWord, secondWord });
    }

    [TestCase("a", "the", "hello", "world", "ow")]
    [TestCase("123", "456", "hello", "world", "ow")]
    [TestCase("русский", "текст", "был", "точно", "убран")]
    public void BoringWordsTextFilter_AddBoringWords(
        string boringWord,
        string firstBoringWord,
        string secondBoringWord,
        string firstWord,
        string secondWord)
    {
        A.CallTo(() => textReaderMock.ReadText(boringWordsSettingsMack.Path))
            .Returns(new List<string> { boringWord });

        var filter = new BoringWordsTextFilter(boringWordsSettingsMack, textReaderMock);
        filter.AddBoringWords(new List<string> { firstBoringWord, secondBoringWord });
        var words = new List<string> { boringWord, firstBoringWord, secondBoringWord, firstWord, secondWord };
        var result = filter.ApplyFilter(words);

        filter.BoringWords.Should().BeEquivalentTo(new List<string> { boringWord, firstBoringWord, secondBoringWord });
        result.Should().BeEquivalentTo(new List<string> { firstWord, secondWord });
    }

    [TestCase("a", "hello", "world")]
    [TestCase("123", "hello", "world")]
    [TestCase("русский", "был", "убран")]
    public void BoringWordsTextFilter_AddBoringWord(string boringWord, string firstWord, string secondWord)
    {
        A.CallTo(() => textReaderMock.ReadText(boringWordsSettingsMack.Path))
            .Returns(new List<string>());

        var filter = new BoringWordsTextFilter(boringWordsSettingsMack, textReaderMock);
        filter.AddBoringWord(boringWord);
        var words = new List<string> { boringWord, firstWord, secondWord };
        var result = filter.ApplyFilter(words);

        filter.BoringWords.Should().BeEquivalentTo(new List<string> { boringWord });
        result.Should().BeEquivalentTo(new List<string> { firstWord, secondWord });
    }

    [TestCase("a", "the", "hello", "world")]
    [TestCase("123", "456", "hello", "world")]
    [TestCase("русский", "текст", "был", "убран")]
    public void BoringWordsTextFilter_RemoveBoringWord(
        string boringWord,
        string firstBoringWord,
        string firstWord,
        string secondWord)
    {
        A.CallTo(() => textReaderMock.ReadText(boringWordsSettingsMack.Path))
            .Returns(new List<string> { boringWord, firstBoringWord });

        var filter = new BoringWordsTextFilter(boringWordsSettingsMack, textReaderMock);
        filter.RemoveBoringWord(firstBoringWord);
        var words = new List<string> { boringWord, firstBoringWord, firstWord, secondWord };
        var result = filter.ApplyFilter(words);

        filter.BoringWords.Should().BeEquivalentTo(new List<string> { boringWord });
        result.Should().BeEquivalentTo(new List<string> { firstBoringWord, firstWord, secondWord });
    }

    [TestCase("a", "the", "hello", "world", "ow")]
    [TestCase("123", "456", "hello", "world", "ow")]
    [TestCase("русский", "текст", "был", "точно", "убран")]
    public void BoringWordsTextFilter_RemoveBoringWords(
        string boringWord,
        string firstBoringWord,
        string secondBoringWord,
        string firstWord,
        string secondWord)
    {
        A.CallTo(() => textReaderMock.ReadText(boringWordsSettingsMack.Path))
            .Returns(new List<string> { boringWord, firstBoringWord, secondBoringWord });

        var filter = new BoringWordsTextFilter(boringWordsSettingsMack, textReaderMock);
        filter.RemoveBoringWords(new List<string> { firstBoringWord, secondBoringWord });
        var words = new List<string> { boringWord, firstBoringWord, secondBoringWord, firstWord, secondWord };
        var result = filter.ApplyFilter(words);

        filter.BoringWords.Should().BeEquivalentTo(new List<string> { boringWord });
        result.Should().BeEquivalentTo(new List<string> { firstBoringWord, secondBoringWord, firstWord, secondWord });
    }

    [TestCase("a", "the", "hello", "world")]
    [TestCase("123", "456", "hello", "world")]
    [TestCase("русский", "текст", "не", "убран")]
    public void BoringWordsTextFilter_ClearBoringWords(
        string firstBoringWord,
        string secondBoringWord,
        string firstWord,
        string secondWord)
    {
        A.CallTo(() => textReaderMock.ReadText(boringWordsSettingsMack.Path))
            .Returns(new List<string> { firstBoringWord, secondBoringWord });

        var filter = new BoringWordsTextFilter(boringWordsSettingsMack, textReaderMock);
        filter.ClearBoringWords();
        var words = new List<string> { firstBoringWord, secondBoringWord, firstWord, secondWord };
        var result = filter.ApplyFilter(words);

        filter.BoringWords.Should().BeEmpty();
        result.Should().BeEquivalentTo(new List<string> { firstBoringWord, secondBoringWord, firstWord, secondWord });
    }
}