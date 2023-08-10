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
        foreach (string f in allFiles) // Directory.EnumerateFiles(directory, "*.yml")
        {
            var file = new FileInfo(f);

            var fileName = file.Name; // file.Replace(directory, "").ToLower();
            var fileNameOnly = fileName.Replace(".yml", "");

            var contents = File.ReadAllText(f); // File.ReadAllText(file)
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

        var TotalStat = new BaseballSoftballStat();
        TotalStat.Hitting.AB = BaseballSoftballGames.Sum(s => s.Stat.Hitting.AB);
        TotalStat.Hitting.H = BaseballSoftballGames.Sum(s => s.Stat.Hitting.H);
        TotalStat.Hitting.R = BaseballSoftballGames.Sum(s => s.Stat.Hitting.R);
        TotalStat.Hitting.RBI = BaseballSoftballGames.Sum(s => s.Stat.Hitting.RBI);
        TotalStat.Hitting.BB = BaseballSoftballGames.Sum(s => s.Stat.Hitting.BB);
        TotalStat.Hitting.HBP = BaseballSoftballGames.Sum(s => s.Stat.Hitting.HBP);
        TotalStat.Hitting.SAC = BaseballSoftballGames.Sum(s => s.Stat.Hitting.SAC);
        TotalStat.Hitting.SO = BaseballSoftballGames.Sum(s => s.Stat.Hitting.SO);
        TotalStat.Hitting.KL = BaseballSoftballGames.Sum(s => s.Stat.Hitting.KL);
        TotalStat.Hitting.D3 = BaseballSoftballGames.Sum(s => s.Stat.Hitting.D3);
        TotalStat.Hitting.ROE = BaseballSoftballGames.Sum(s => s.Stat.Hitting.ROE);
        TotalStat.Hitting.FC = BaseballSoftballGames.Sum(s => s.Stat.Hitting.FC);
        TotalStat.Hitting.Doubles = BaseballSoftballGames.Sum(s => s.Stat.Hitting.Doubles);
        TotalStat.Hitting.Triples = BaseballSoftballGames.Sum(s => s.Stat.Hitting.Triples);
        TotalStat.Hitting.HR = BaseballSoftballGames.Sum(s => s.Stat.Hitting.HR);
        TotalStat.Hitting.SB = BaseballSoftballGames.Sum(s => s.Stat.Hitting.SB);
        TotalStat.Hitting.CS = BaseballSoftballGames.Sum(s => s.Stat.Hitting.CS);
        TotalStat.Hitting.PIK = BaseballSoftballGames.Sum(s => s.Stat.Hitting.PIK);
        TotalStat.Fielding.PO = BaseballSoftballGames.Sum(s => s.Stat.Fielding.PO);
        TotalStat.Fielding.A = BaseballSoftballGames.Sum(s => s.Stat.Fielding.A);
        TotalStat.Fielding.E = BaseballSoftballGames.Sum(s => s.Stat.Fielding.E);
        TotalStat.Fielding.DP = BaseballSoftballGames.Sum(s => s.Stat.Fielding.DP);
        TotalStat.Fielding.TP = BaseballSoftballGames.Sum(s => s.Stat.Fielding.TP);
        TotalStat.Fielding.SB = BaseballSoftballGames.Sum(s => s.Stat.Fielding.SB);
        TotalStat.Fielding.CS = BaseballSoftballGames.Sum(s => s.Stat.Fielding.CS);
        TotalStat.Fielding.PIK = BaseballSoftballGames.Sum(s => s.Stat.Fielding.PIK);
        TotalStat.Fielding.CI = BaseballSoftballGames.Sum(s => s.Stat.Fielding.CI);
        TotalStat.Fielding.PB = BaseballSoftballGames.Sum(s => s.Stat.Fielding.PB);
        TotalStat.Position.P = BaseballSoftballGames.Sum(s => s.Stat.Position.P);
        TotalStat.Position.C = BaseballSoftballGames.Sum(s => s.Stat.Position.C);
        TotalStat.Position.First = BaseballSoftballGames.Sum(s => s.Stat.Position.First);
        TotalStat.Position.Second = BaseballSoftballGames.Sum(s => s.Stat.Position.Second);
        TotalStat.Position.Third = BaseballSoftballGames.Sum(s => s.Stat.Position.Third);
        TotalStat.Position.SS = BaseballSoftballGames.Sum(s => s.Stat.Position.SS);
        TotalStat.Position.LF = BaseballSoftballGames.Sum(s => s.Stat.Position.LF);
        TotalStat.Position.CF = BaseballSoftballGames.Sum(s => s.Stat.Position.CF);
        TotalStat.Position.RF = BaseballSoftballGames.Sum(s => s.Stat.Position.RF);

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
        Console.WriteLine("Games Played: " + BaseballSoftballGames.Count());
        //Console.WriteLine(BaseballSoftballGames[4].GameDetail.Recap);

        return true;
    }

}