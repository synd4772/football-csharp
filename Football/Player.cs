using System;

namespace Football;

// Mängija klass, mis sisaldab mängija atribuute ja käitumist
public class Player
{
    // Mängija nimi
    public string Name { get; }
    // Mängija X- ja Y-koordinaadid
    public double X { get; private set; }
    public double Y { get; private set; }

    private double _vx, _vy; // Mängija liikumise kiirus
    public Team? Team { get; set; } = null; // Mängija meeskond

    private const double MaxSpeed = 5; // Maksimaalne kiirus
    private const double MaxKickSpeed = 25; // Maksimaalne löögikiirus
    private const double BallKickDistance = 10; // Distants, millest mängija saab palli lüüa

    private Random _random = new Random(); // Juhuslike arvude genereerija

    // Konstruktor, mis määrab mängija nime
    public Player(string name)
    {
        Name = name;
    }

    // Üks teine konstruktor, mis määrab nime, asukoha ja meeskonna
    public Player(string name, double x, double y, Team team)
    {
        Name = name;
        X = x;
        Y = y;
        Team = team;
    }

    // Määrab mängija positsiooni
    public void SetPosition(double x, double y)
    {
        X = x;
        Y = y;
    }

    // Tagastab mängija absoluutse positsiooni
    public (double, double) GetAbsolutePosition()
    {
        return Team!.Game.GetPositionForTeam(Team, X, Y);
    }

    // Arvutab kauguse pallist
    public double GetDistanceToBall()
    {
        var ballPosition = Team!.GetBallPosition(); // Saab palli positsiooni
        var dx = ballPosition.Item1 - X; // Kaugus X suunas
        var dy = ballPosition.Item2 - Y; // Kaugus Y suunas
        return Math.Sqrt(dx * dx + dy * dy); // Pythagorase teoreemiga arvutus
    }

    // Liigub palli suunas
    public void MoveTowardsBall()
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X; // X-suunalise kauguse arvutus
        var dy = ballPosition.Item2 - Y; // Y-suunalise kauguse arvutus
        var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed; // Suhe maksimaalse kiiruseni
        _vx = dx / ratio; // Uus X-kiirus
        _vy = dy / ratio; // Uus Y-kiirus
    }

    // Mängija liikumise meetod
    public void Move()
    {
        // Kui mängija ei ole lähim palli mängija, peatab liikumise
        if (Team.GetClosestPlayerToBall() != this)
        {
            _vx = 0;
            _vy = 0;
        }

        // Kui mängija on piisavalt lähedal pallile, lööb ta palli
        if (GetDistanceToBall() < BallKickDistance)
        {
            Team.SetBallSpeed(
                MaxKickSpeed * _random.NextDouble(), // Juhuslik X-kiirus
                MaxKickSpeed * (_random.NextDouble() - 0.5) // Juhuslik Y-kiirus
                );
        }

        // Uute koordinaatide arvutamine
        var newX = X + _vx;
        var newY = Y + _vy;
        var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);

        // Kontrollib, kas uus positsioon on staadionil
        if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
        {
            X = newX; // Uus positsioon
            Y = newY; // Uus positsioon
        }
        else
        {
            _vx = _vy = 0; // Kui positsioon on vale, peatab liikumise
        }
    }
}
