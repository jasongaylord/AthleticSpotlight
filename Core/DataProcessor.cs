using System.IO;
using System.Globalization;
using AthleticSpotlight.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Core;

public static class DataProcessor {
    public static List<Season> Seasons { get; set; }
    public static List<BaseballSoftballGame> BaseballSoftballGames { get; set; }

    public static bool Run() {
        var directory = Directory.GetCurrentDirectory() + "\\Data\\";
        var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();

        Seasons = new List<Season>();
        BaseballSoftballGames = new List<BaseballSoftballGame>();

        // Process Seasons First
        var seasonFileName = directory + "_season.yml";
        var seasonContents = File.ReadAllText(seasonFileName);
        var seasonSr = new StringReader(seasonContents);
        var seasonParser = new Parser(seasonSr);
        seasonParser.Consume<StreamStart>();
        while (seasonParser.Accept<DocumentStart>()) {
            var season = deserializer.Deserialize<Season>(seasonParser);
            Seasons.Add(season);
        }


        // Loop through other files
        foreach (string file in Directory.EnumerateFiles(directory, "*.yml"))
        {
            var fileName = file.Replace(directory, "").ToLower();
            var fileNameOnly = fileName.Replace(".yml", "");

            var contents = File.ReadAllText(file);
            var sr = new StringReader(contents);

            // Process Game Files
            DateTime dt;
            if (DateTime.TryParseExact(fileNameOnly, "yyyyddMMhhmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
                var eventStruct = deserializer.Deserialize<EventStruct>(sr);
                var srProper = new StringReader(contents);

                var season = new Season();
                season = Seasons.SingleOrDefault(w => w.Id == eventStruct.Season);
                
                if (season.Sport == "Baseball" || season.Sport == "Softball") {
                    var baseballSoftballGame = deserializer.Deserialize<BaseballSoftballGame>(srProper);
                    BaseballSoftballGames.Add(baseballSoftballGame);
                }
            }
        }

        return true;
    }

}