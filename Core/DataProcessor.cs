using System.IO;
using System.Globalization;
using AthleticSpotlight.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Core;

public static class DataProcessor {
    public static List<Team>? Teams { get; set; }
    public static List<Season>? Seasons { get; set; }
    public static List<BaseballSoftballGame>? BaseballSoftballGames { get; set; }

    public static bool Run() {
        var directory = Directory.GetCurrentDirectory() + "\\Data\\";
        var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();

        Teams = new List<Team>();
        Seasons = new List<Season>();
        BaseballSoftballGames = new List<BaseballSoftballGame>();

        // Process Teams First
        var teamFileName = directory + "_teams.yml";
        var teamContents = File.ReadAllText(teamFileName);
        var teamSr = new StringReader(teamContents);
        var teamParser = new Parser(teamSr);
        teamParser.Consume<StreamStart>();
        while (teamParser.Accept<DocumentStart>()) {
            var team = deserializer.Deserialize<Team>(teamParser);
            Teams.Add(team);
        }

        // Process Seasons Second
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
        var allFiles = Directory.GetFiles(directory, "*.yml", SearchOption.AllDirectories);
        foreach (string f in allFiles)
        {
            var file = new FileInfo(f);

            var fileName = file.Name;
            var fileNameOnly = fileName.Replace(".yml", "");

            var contents = File.ReadAllText(f);
            var sr = new StringReader(contents);

            // Process Game Files
            DateTime dt;
            if (DateTime.TryParseExact(fileNameOnly, "yyyyMMddHHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
                var eventStruct = deserializer.Deserialize<EventStruct>(sr);
                var srProper = new StringReader(contents);

                var season = new Season();
                season = Seasons.SingleOrDefault(w => w.Id == eventStruct.Season);

                var team = new Team();
                team = Teams.SingleOrDefault(w => w.TeamId == season.TeamId);
                
                if (team.Sport == "Baseball" || team.Sport == "Softball") {
                    var baseballSoftballGame = deserializer.Deserialize<BaseballSoftballGame>(srProper);
                    BaseballSoftballGames.Add(baseballSoftballGame);
                }
            }
        }

        var games = BaseballSoftballGames; //.Where(w => w.Season == 1);

        var TotalStat = new BaseballSoftballStat();
        TotalStat.Hitting.AB = games.Sum(s => s.Stat.Hitting.AB);
        TotalStat.Hitting.H = games.Sum(s => s.Stat.Hitting.H);
        TotalStat.Hitting.R = games.Sum(s => s.Stat.Hitting.R);
        TotalStat.Hitting.RBI = games.Sum(s => s.Stat.Hitting.RBI);
        TotalStat.Hitting.BB = games.Sum(s => s.Stat.Hitting.BB);
        TotalStat.Hitting.HBP = games.Sum(s => s.Stat.Hitting.HBP);
        TotalStat.Hitting.SAC = games.Sum(s => s.Stat.Hitting.SAC);
        TotalStat.Hitting.SO = games.Sum(s => s.Stat.Hitting.SO);
        TotalStat.Hitting.KL = games.Sum(s => s.Stat.Hitting.KL);
        TotalStat.Hitting.D3 = games.Sum(s => s.Stat.Hitting.D3);
        TotalStat.Hitting.ROE = games.Sum(s => s.Stat.Hitting.ROE);
        TotalStat.Hitting.FC = games.Sum(s => s.Stat.Hitting.FC);
        TotalStat.Hitting.Doubles = games.Sum(s => s.Stat.Hitting.Doubles);
        TotalStat.Hitting.Triples = games.Sum(s => s.Stat.Hitting.Triples);
        TotalStat.Hitting.HR = games.Sum(s => s.Stat.Hitting.HR);
        TotalStat.Hitting.SB = games.Sum(s => s.Stat.Hitting.SB);
        TotalStat.Hitting.CS = games.Sum(s => s.Stat.Hitting.CS);
        TotalStat.Hitting.PIK = games.Sum(s => s.Stat.Hitting.PIK);
        TotalStat.Fielding.PO = games.Sum(s => s.Stat.Fielding.PO);
        TotalStat.Fielding.A = games.Sum(s => s.Stat.Fielding.A);
        TotalStat.Fielding.E = games.Sum(s => s.Stat.Fielding.E);
        TotalStat.Fielding.DP = games.Sum(s => s.Stat.Fielding.DP);
        TotalStat.Fielding.TP = games.Sum(s => s.Stat.Fielding.TP);
        TotalStat.Fielding.SB = games.Sum(s => s.Stat.Fielding.SB);
        TotalStat.Fielding.CS = games.Sum(s => s.Stat.Fielding.CS);
        TotalStat.Fielding.PIK = games.Sum(s => s.Stat.Fielding.PIK);
        TotalStat.Fielding.CI = games.Sum(s => s.Stat.Fielding.CI);
        TotalStat.Fielding.PB = games.Sum(s => s.Stat.Fielding.PB);
        TotalStat.Position.P = games.InningSum(s => s.Stat.Position.P);
        TotalStat.Position.C = games.InningSum(s => s.Stat.Position.C);
        TotalStat.Position.First = games.InningSum(s => s.Stat.Position.First);
        TotalStat.Position.Second = games.InningSum(s => s.Stat.Position.Second);
        TotalStat.Position.Third = games.InningSum(s => s.Stat.Position.Third);
        TotalStat.Position.SS = games.InningSum(s => s.Stat.Position.SS);
        TotalStat.Position.LF = games.InningSum(s => s.Stat.Position.LF);
        TotalStat.Position.CF = games.InningSum(s => s.Stat.Position.CF);
        TotalStat.Position.RF = games.InningSum(s => s.Stat.Position.RF);

        Console.WriteLine("Hitting");
        Console.WriteLine("PA: " + TotalStat.Hitting.PA);
        Console.WriteLine("AB: " + TotalStat.Hitting.AB);
        Console.WriteLine("H: " + TotalStat.Hitting.H);
        Console.WriteLine("RBI: " + TotalStat.Hitting.RBI);
        Console.WriteLine("R: " + TotalStat.Hitting.R);
        Console.WriteLine("AVG: " + TotalStat.Hitting.AVG);
        Console.WriteLine("SLG: " + TotalStat.Hitting.SLG);
        Console.WriteLine("BB: " + TotalStat.Hitting.BB);
        Console.WriteLine("HBP: " + TotalStat.Hitting.HBP);
        Console.WriteLine("OBP: " + TotalStat.Hitting.OBP);
        Console.WriteLine("OPS: " + TotalStat.Hitting.OPS);
        Console.WriteLine("SAC: " + TotalStat.Hitting.SAC);
        Console.WriteLine("ROE: " + TotalStat.Hitting.ROE);
        Console.WriteLine("FC: " + TotalStat.Hitting.FC);
        Console.WriteLine("SO: " + TotalStat.Hitting.SO);
        Console.WriteLine("KL: " + TotalStat.Hitting.KL);
        Console.WriteLine("D3: " + TotalStat.Hitting.D3);
        Console.WriteLine("TB: " + TotalStat.Hitting.TB);
        Console.WriteLine("2B: " + TotalStat.Hitting.Doubles);
        Console.WriteLine("3B: " + TotalStat.Hitting.Triples);
        Console.WriteLine("HR: " + TotalStat.Hitting.HR);
        Console.WriteLine("SB: " + TotalStat.Hitting.SB);
        Console.WriteLine("CS: " + TotalStat.Hitting.CS);
        Console.WriteLine("SBP: " + TotalStat.Hitting.SBP);
        Console.WriteLine("PIK: " + TotalStat.Hitting.PIK);

        Console.WriteLine();
        Console.WriteLine("Fielding");
        Console.WriteLine("TC: " + TotalStat.Fielding.TC);
        Console.WriteLine("PO: " + TotalStat.Fielding.PO);
        Console.WriteLine("A: " + TotalStat.Fielding.A);
        Console.WriteLine("E: " + TotalStat.Fielding.E);
        Console.WriteLine("FPCT: " + TotalStat.Fielding.FPCT);
        Console.WriteLine("DP: " + TotalStat.Fielding.DP);
        Console.WriteLine("TP: " + TotalStat.Fielding.TP);
        Console.WriteLine("SB: " + TotalStat.Fielding.SB);
        Console.WriteLine("CS: " + TotalStat.Fielding.CS);
        Console.WriteLine("CSP: " + TotalStat.Fielding.CSP);
        Console.WriteLine("PIK: " + TotalStat.Fielding.PIK);
        Console.WriteLine("CI: " + TotalStat.Fielding.CI);

        Console.WriteLine();
        Console.WriteLine("Position");
        Console.WriteLine("P: " + TotalStat.Position.P);
        Console.WriteLine("C: " + TotalStat.Position.C);
        Console.WriteLine("1B: " + TotalStat.Position.First);
        Console.WriteLine("2B: " + TotalStat.Position.Second);
        Console.WriteLine("SS: " + TotalStat.Position.SS);
        Console.WriteLine("3B: " + TotalStat.Position.Third);
        Console.WriteLine("LF: " + TotalStat.Position.LF);
        Console.WriteLine("CF: " + TotalStat.Position.CF);
        Console.WriteLine("RF: " + TotalStat.Position.RF);

        Console.WriteLine();
        Console.WriteLine("Games Played: " + games.Count());
        //Console.WriteLine(BaseballSoftballGames[4].GameDetail.Recap);

        return true;
    }

}