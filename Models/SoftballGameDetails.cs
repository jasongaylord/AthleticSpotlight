namespace AthleticSpotlight.Models;

public class SoftballGameDetails {
  public int Season { get; set; }
  public string Opponent { get; set; }
  public string Home { private get; set; }
  public bool IsHomeTeam { get { return this.Home.ToUpper() == "Y"; } }
  public string Win { private get; set; }
  public bool TeamWin { get { return this.Win.ToUpper() == "Y"; } }
  public int OpponentScore { get; set; }
  public int TeamScore { get; set; }
  public string Recap { get; set; }

  // public SoftballGameDetails() {
  //   Season = 0;
  //   Home = "Y";
  //   Win = "N";
  //   Opponent = "";
  //   OpponentScore = 0;
  //   TeamScore = 0;
  // }
}