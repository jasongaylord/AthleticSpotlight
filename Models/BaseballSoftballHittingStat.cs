using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

public class BaseballSoftballHittingStat {
    /// <summary>
    /// Plate Appearances
    /// </summary>
    public int PA { get { return (AB + BB + HBP); } }
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
    public string AVG { get { return String.Format("{0:#,0.000}", ((double)this.H/(double)this.AB)); } }
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
    /// Strikeout
    /// </summary>
    public int SO { get; set; }
    /// <summary>
    /// Sacrifice Fly
    /// </summary>
    public int SF { get; set; }
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
}