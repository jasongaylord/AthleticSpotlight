using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class EventStruct {
    public int Season { get; set; }

    public EventStruct() {
        Season = 0;
    }
}