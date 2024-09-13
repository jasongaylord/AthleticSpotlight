using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class Team {
    public int TeamId { get; set; }
    public string Name { get; set; }
    public string Sport { get; set; }
    public string TeamType { get; set; }
    public string TeamAge { get; set; }

    public Team() {
        TeamId = 0;
        Name = "";
        Sport = "";
        TeamType = "";
        TeamAge = "";
    }
}