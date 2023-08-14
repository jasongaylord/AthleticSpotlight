using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AthleticSpotlight.Models;

/// <summary>
/// Captures the number of innings at a position
/// </summary>
public class BaseballSoftballPositionStat {
    public decimal P { get; set; }
    public decimal C { get; set; }
    public decimal First { get; set; }
    public decimal Second { get; set; }
    public decimal Third { get; set; }
    public decimal SS { get; set; }
    public decimal LF { get; set; }
    public decimal CF { get; set; }
    public decimal RF { get; set; }
}