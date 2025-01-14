using Autofac;
using CommandLine;
using TagCloudConsoleApp;
using TagCloudConsoleApp.SettingsProvider;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Filters;
using TagsCloudVisualization.Generators;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Models;
using TagsCloudVisualization.Models.Settings;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Savers;
using TagsCloudVisualization.Selectors;
using TagsCloudVisualization.Visualizatiuons;

var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
var settingsProvider = new SettingsProvider(options);
var settings = settingsProvider.GetSettings();
var builder = new ContainerBuilder();

RegisterServices(builder);
RegisterSettings(builder, settings);
RegisterFileReaders(builder);

var build = builder.Build();
var tagCloudImageGenerator = build.Resolve<TagCloudImageGenerator>();
tagCloudImageGenerator
    .GenerateCloud()
    .OnFail(Console.WriteLine);

return;

void RegisterServices(ContainerBuilder containerBuilder)
{
    containerBuilder.RegisterType<FileReadersSelector>().AsSelf();
    containerBuilder.RegisterType<ArchimedeanSpiralPositionGenerator>().As<IPositionGenerator>().SingleInstance();
    containerBuilder.RegisterType<RectangleVisualizatiuon>().As<IRectangleDraftsman>().SingleInstance();
    containerBuilder.RegisterType<FileReadersSelector>().As<IFileReadersSelector>().SingleInstance();
    containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
    containerBuilder.RegisterType<BitmapGenerator>().As<IBitmapGenerator>().SingleInstance();
    containerBuilder.RegisterType<BoringWordsTextFilter>().As<ITextFilter>().SingleInstance();
    containerBuilder.RegisterType<LowerCaseTextFilter>().As<ITextFilter>().SingleInstance();
    containerBuilder.RegisterType<TxtTextReader>().As<ITextReader>().SingleInstance();
    containerBuilder.RegisterType<ImageSaver>().As<IImageSaver>().SingleInstance();
    containerBuilder.RegisterType<TagCloudImageGenerator>().AsSelf();
}

void RegisterSettings(ContainerBuilder containerBuilder, SettingsManager settingsManager)
{
    containerBuilder.RegisterInstance(settingsManager.SaveSettings);
    containerBuilder.RegisterInstance(settingsManager.TextReaderSettings);
    containerBuilder.RegisterInstance(settingsManager.BitmapGeneratorSettings);
    containerBuilder.RegisterInstance(settingsManager.SpiralGeneratorSettings);
    containerBuilder.RegisterInstance(settingsManager.TextSettings);
    containerBuilder.RegisterInstance(settingsManager.BoringWordsSettings);
}

void RegisterFileReaders(ContainerBuilder containerBuilder)
{
    containerBuilder.RegisterType<TxtTextReader>().Keyed<ITextReader>(".txt");
    containerBuilder.RegisterType<DocTextReader>().Keyed<ITextReader>(".doc");
    containerBuilder.RegisterType<DocxTextReader>().Keyed<ITextReader>(".docx");
}