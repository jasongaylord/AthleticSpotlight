using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class BaseballSoftballGame {
    public int Season { get; set; }
    public DateTime GameTime { get; set; }
    [YamlMember(Alias = "Detail")]
    public GameDetail GameDetail { get; set; }

    [YamlMember(Alias = "Stats")]
    public BaseballSoftballStat Stat { get; set; }

    public BaseballSoftballGame() {
        GameTime = new DateTime();
        GameDetail = new GameDetail();
        Stat = new BaseballSoftballStat();
    }
}