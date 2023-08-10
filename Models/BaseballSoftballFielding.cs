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
}