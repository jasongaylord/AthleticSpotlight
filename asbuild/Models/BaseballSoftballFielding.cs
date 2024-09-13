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
    public int TC { get { return PO + A + E; } }
    /// <summary>
    /// Fielding Percentage
    /// </summary>
    public string FPCT { get { return String.Format("{0:#,0.000}", ((double)(PO + A)/(double)TC)); } }
    /// <summary>
    /// For catcher's, stolen bases against
    /// </summary>
    public int SB { get; set; }
    /// <summary>
    /// For catcher's, caught stealing against (also included in Assists)
    /// </summary>
    public int CS { get; set; }
    /// <summary>
    /// Caught stealing percentage
    /// </summary>
    public string CSP { get { return String.Format("{0:#00.00}", ((double)(CS*100)/(double)(SB + CS))); }}
    /// <summary>
    /// Picked off
    /// </summary>
    public int PIK { get; set; }
    /// <summary>
    /// Advancement by catcher interference
    /// </summary>
    public int CI { get; set; }
    /// <summary>
    /// Pass balls
    /// </summary>
    public int PB { get; set; }
}