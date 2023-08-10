using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class BaseballSoftballStat {
    public BaseballSoftballHittingStat Hitting { get; set; }
    public BaseballSoftballFieldingStat Fielding { get; set; }
    public BaseballSoftballPositionStat Position { get; set; }

    public BaseballSoftballStat() {
        Hitting = new BaseballSoftballHittingStat();
        Fielding = new BaseballSoftballFieldingStat();
        Position = new BaseballSoftballPositionStat();
    }
}