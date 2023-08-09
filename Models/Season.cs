using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class Season {
    public int Id { get; set; }
    public string Name { get; set; }
    public float SortableSeason {get; set; }
    public string Sport { get; set; }
    public string TeamType { get; set; }
    public string TeamAge { get; set; }

    public Season() {
        Id = 0;
        Name = "";
        SortableSeason = 0;
        Sport = "";
        TeamType = "";
        TeamAge = "";
    }
}