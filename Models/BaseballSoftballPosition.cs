using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

/// <summary>
/// Captures the number of innings at a position
/// </summary>
public class BaseballSoftballPositionStat {
    public double P { get; set; }
    public double C { get; set; }
    public double First { get; set; }
    public double Second { get; set; }
    public double Third { get; set; }
    public double SS { get; set; }
    public double LF { get; set; }
    public double CF { get; set; }
    public double RF { get; set; }
}