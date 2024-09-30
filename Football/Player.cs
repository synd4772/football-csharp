using System;
using System.Drawing;

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

    public ConsoleColor _color { get; private set; }

    public char _sym { get; private set; }

    // Konstruktor, mis määrab mängija nime
    public Player(string name)
    {
        Name = name;
    }
    public void SetSymbol(char sym)
    {
        this._sym = sym;
    }

    // Üks teine konstruktor, mis määrab nime, asukoha ja meeskonna
    public Player(string name, double x, double y, Team team, char sym)
    {
        Name = name;
        X = x;
        Y = y;
        Team = team;
        this._sym = sym;
    }

    // Määrab mängija positsiooni
    public void SetPosition(double x, double y)
    {
        X = x;
        Y = y;
        this.Draw();
    }

    public void SetColor(ConsoleColor color)
    {
        this._color = color;
    }

    public void Draw()
    {
        ConsoleColor currColor = Console.ForegroundColor;
        Console.ForegroundColor = _color;
        try
        {
            Console.SetCursorPosition((int)this.X, (int)this.Y);

            Console.Write(this._sym);
            Console.ForegroundColor = currColor;
        }
        catch (Exception e)
        {
            Console.SetCursorPosition(Team.Game.Stadium.Height + 3, 0);
            Console.WriteLine($"{this.X}, {this.Y}");
        }


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
        try
        {
            Console.SetCursorPosition((int)this.X, (int)this.Y);
            Console.Write(" ");
        }
        catch
        {

        }
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
        while (true)
        {
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2) || Team.Game.Stadium.IsInGates((int)newAbsolutePosition.Item1, (int)newAbsolutePosition.Item2) is null || X <= 0 || Y <= 0)
            {
                X = newX; // Uus positsioon
                Y = newY; // Uus positsioon
                break;
            }
            else
            {
                Console.SetCursorPosition(Team.Game.Stadium.Width + 1, 4);
                Console.Write($"{this.Name} is out");
                Team.Game.Stadium.Draw();
                Random rnd = new Random();
                if (rnd.Next(2) == 2)
                {
                    X += rnd.Next(2);
                }
                else if(rnd.Next(1) == 1)
                {
                    X -= rnd.Next(2);
                }
                continue;
                
            }
        }

        // Kontrollib, kas uus positsioon on staadionil

        this.Draw();
    }
}
