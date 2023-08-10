using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class BaseballSoftballFieldingStat {
    /// <summary>
    /// Put outs
    /// </summary>
    public int PO { get; set; }
    /// <summary>
    /// Assists
    /// </summary>
    public int A { get; set; }
    /// <summary>
    /// Errors
    /// </summary>
    public int E { get; set; }
    /// <summary>
    /// Double plays
    /// </summary>
    public int DP { get; set; }
    /// <summary>
    /// Triple plays
    /// </summary>
    public int TP { get; set; }
    /// <summary>
    /// Fielding Chances
    /// </summary>
    public int FC { get { return PO + A + E; } }
    /// <summary>
    /// For catcher's, stolen bases against
    /// </summary>
    public int SB { get; set; }
    /// <summary>
    /// For catcher's, caught stealing against (also included in Assists)
    /// </summary>
    public int CS { get; set; }
}