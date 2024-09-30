namespace Football;

// Mängu klass, mis haldab meeskondi, staadioni ja palli
public class Game
{
    public Team HomeTeam { get; } // Kodu meeskond
    public Team AwayTeam { get; } // Külalismeeskond
    public Stadium Stadium { get; } // Staadion, kus mäng toimub
    public Ball Ball { get; private set; } // Mängupall

    public Score Score { get; private set; }


    // Konstruktor, mis määrab kodu- ja külalismeeskonna ning staadioni
    public Game(Team homeTeam, Team awayTeam, Stadium stadium, Score score)
    {
        HomeTeam = homeTeam; // Kodu meeskonna määramine
        homeTeam.Game = this; // Seob meeskonna mänguga
        AwayTeam = awayTeam; // Külalismeeskonna määramine
        awayTeam.Game = this; // Seob meeskonna mänguga
        Stadium = stadium; // Staadioni määramine
        this.Score = score;
    }

    // Mängu alustamine
    public void Start()
    {
        // Loob palli algasukoha
        Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
        // Alustab mõlema meeskonna mängu
        HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height);
        AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height);
    }

    // Tagastab külalismeeskonna positsiooni
    private (double, double) GetPositionForAwayTeam(double x, double y)
    {
        return (Stadium.Width - x, Stadium.Height - y); // Peegeldab positsiooni
    }

    // Tagastab meeskonna positsiooni vastavalt sellele, kas see on kodumeeskond või külalismeeskond
    public (double, double) GetPositionForTeam(Team team, double x, double y)
    {
        return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y);
    }

    // Tagastab palli positsiooni meeskonna jaoks
    public (double, double) GetBallPositionForTeam(Team team)
    {
        return GetPositionForTeam(team, Ball.X, Ball.Y);
    }

    // Määrab palli kiirus meeskonna jaoks
    public void SetBallSpeedForTeam(Team team, double vx, double vy)
    {
        if (team == HomeTeam)
        {
            Ball.SetSpeed(vx, vy); // Kui see on kodumeeskond, määrab otse kiirus
        }
        else
        {
            Ball.SetSpeed(-vx, -vy); // Kui see on külalismeeskond, peegeldab kiirus
        }
    }

    // Mängu käivitamine, liikumine
    public void Move()
    {
        HomeTeam.Move(); // Liigutab kodumeeskonna mängijad
        AwayTeam.Move(); // Liigutab külalismeeskonna mängijad
        Ball.Move(); // Liigutab palli
    }
}
