using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class BaseballSoftballHittingStat {
    /// <summary>
    /// Plate Appearances
    /// </summary>
    public int PA { get { return (AB + BB + HBP + SAC); } }
    /// <summary>
    /// At Bats
    /// </summary>
    public int AB { get; set; }
    /// <summary>
    /// Hits
    /// </summary>
    public int H { get; set; }
    /// <summary>
    /// Batting Average
    /// </summary>
    public string AVG { get { return String.Format("{0:#,0.000}", ((double)H/(double)AB)); } }
    /// <summary>
    /// On Base Percentage
    /// </summary>
    public string OBP { get { return String.Format("{0:#,0.000}", ((double)(H + BB + HBP + SAC)/(double)PA)); } }
    /// <summary>
    /// On Base Percentage + Slugging Percentage
    /// </summary>
    public string OPS { get { return String.Format("{0:#,0.000}", (Double.Parse(OBP)+Double.Parse(SLG))); } }
    /// <summary>
    /// Slugging Percentage
    /// </summary>
    public string SLG { get { return String.Format("{0:#,0.000}", ((double)TB/(double)AB)); } }
    /// <summary>
    /// Total bases (from hits)
    /// </summary>
    public int TB { get { return ((this.H - this.Doubles - this.Triples - this.HR) + (this.Doubles * 2) + (this.Triples * 3) + (this.HR * 4)); } }
    /// <summary>
    /// Runs
    /// </summary>
    public int R { get; set; }
    /// <summary>
    /// Runs Batted In
    /// </summary>
    public int RBI { get; set; }
    /// <summary>
    /// Walk
    /// </summary>
    public int BB { get; set; }
    /// <summary>
    /// Hit by pitch
    /// </summary>
    public int HBP { get; set; }
    /// <summary>
    /// Sacrifice
    /// </summary>
    public int SAC { get; set; }
    /// <summary>
    /// Strikeout
    /// </summary>
    public int SO { get; set; }
    /// <summary>
    /// Strikeout looking. These are already including in strikeouts. 
    /// </summary>
    public int KL { get; set; }
    /// <summary>
    /// Reached on a drop 3rd strike.
    /// </summary>
    public int D3 { get; set; }
    /// <summary>
    /// Reached on error
    /// </summary>
    public int ROE { get; set; }
    /// <summary>
    /// Fielder's Choice
    /// </summary>
    public int FC { get; set; }
    /// <summary>
    /// Doubles
    /// </summary>
    [YamlMember(Alias = "2B")]
    public int Doubles { get; set; }
    /// <summary>
    /// Triples
    /// </summary>
    [YamlMember(Alias = "3B")]
    public int Triples { get; set; }
    /// <summary>
    /// Homeruns
    /// </summary>
    public int HR { get; set;}
    /// <summary>
    /// Stolen bases
    /// </summary>
    public int SB { get; set; }
    /// <summary>
    /// Caught Stealing
    /// </summary>
    public int CS { get; set; }
    /// <summary>
    /// Stolen Base Percentage
    /// </summary>
    public string SBP { get { return String.Format("{0:#00.00}", ((double)(SB * 100)/(double)(SB + CS))); } }
    /// <summary>
    /// Picked Off
    /// </summary>
    public int PIK { get; set; }
}