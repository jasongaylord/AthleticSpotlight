namespace AthleticSpotlight.Models;

public class GameDetail {
  public string Opponent { get; set; }
  public string Home { private get; set; }
  public bool IsHomeTeam { get { return this.Home.ToUpper() == "Y"; } }
  public string Result { private get; set; }
  public bool TeamWin { get { return this.Result.ToUpper() == "W"; } }
  public int OpponentScore { get; set; }
  public int TeamScore { get; set; }
  public string GameRecap { get; set; }
  public string PlayRecap { get; set; }

  public GameDetail() {
    Opponent = "";
    Home = "Y";
    Result = "T";
    OpponentScore = 0;
    TeamScore = 0;
    GameRecap = "";
    PlayRecap = "";
  }
}