using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class BaseballSoftballStat {
    public BaseballSoftballHittingStat Hitting { get; set; }

    public BaseballSoftballStat() {
        Hitting = new BaseballSoftballHittingStat();
    }
}