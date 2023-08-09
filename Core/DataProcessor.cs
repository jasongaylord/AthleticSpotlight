using System.IO;
using AthleticSpotlight.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Core;

public static class DataProcessor {
    public static List<Season> Seasons { get; set; }

    public static bool Run() {
        var directory = Directory.GetCurrentDirectory() + "\\Data\\";
        var deserializer = new DeserializerBuilder().Build();

        var Seasons = new List<Season>();

        foreach (string file in Directory.EnumerateFiles(directory, "*.yml"))
        {
            var fileName = file.Replace(directory, "").ToLower();
            var fileNameOnly = fileName.Replace(".yml", "");

            if (fileName == "season.yml") {
                var seasonContents = File.ReadAllText(file);
                var seasonParser = new Parser(new StringReader(seasonContents));

                seasonParser.Consume<StreamStart>();

                while (seasonParser.Accept<DocumentStart>()) {
                    var season = deserializer.Deserialize<Season>(seasonParser);
                    Seasons.Add(season);
                }
            }

            

            Console.WriteLine(Seasons.Count());
            Console.WriteLine(file);
        }

        return true;
    }

}