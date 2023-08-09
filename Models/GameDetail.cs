namespace AthleticSpotlight.Models;

public class GameDetail {
  public string Opponent { get; set; }
  public string Home { private get; set; }
  public bool IsHomeTeam { get { return this.Home.ToUpper() == "Y"; } }
  public string Win { private get; set; }
  public bool TeamWin { get { return this.Win.ToUpper() == "Y"; } }
  public int OpponentScore { get; set; }
  public int TeamScore { get; set; }
  public string Recap { get; set; }

  public GameDetail() {
    Opponent = "";
    Home = "Y";
    Win = "N";
    OpponentScore = 0;
    TeamScore = 0;
    Recap = "";
  }
}