using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class Season {
    public int Id { get; set; }
    public int TeamId { get; set; }
    public string Name { get; set; }
    public int Year {get; set; }

    public Season() {
        Id = 0;
        TeamId = 0;
        Name = "";
        Year = 2000;
    }
}