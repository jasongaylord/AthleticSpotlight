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

    public static bool Run() {
        var directory = Directory.GetCurrentDirectory() + "\\Data\\";
        var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();

        var Seasons = new List<Season>();

        foreach (string file in Directory.EnumerateFiles(directory, "*.yml"))
        {
            var fileName = file.Replace(directory, "").ToLower();
            var fileNameOnly = fileName.Replace(".yml", "");

            var contents = File.ReadAllText(file);
            var sr = new StringReader(contents);

            // Process Season.YML file
            if (fileName == "season.yml") {
                var seasonParser = new Parser(sr);

                seasonParser.Consume<StreamStart>();

                while (seasonParser.Accept<DocumentStart>()) {
                    var season = deserializer.Deserialize<Season>(seasonParser);
                    Seasons.Add(season);
                }
            }

            // Process Game Files
            DateTime dt;
            if (DateTime.TryParseExact(fileNameOnly, "yyyyddMMhhmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
                var eventStruct = deserializer.Deserialize<EventStruct>(sr);

                var season = new Season();
                season = Seasons.SingleOrDefault(w => w.Id == eventStruct.Season);
                
                Console.WriteLine("Season: " + eventStruct.Season.ToString() + " , Season Type: " + season.Sport);
            }


            Console.WriteLine(Seasons.Count());
            Console.WriteLine(file);
        }

        return true;
    }

}