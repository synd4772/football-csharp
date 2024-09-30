namespace Football;

// Palli klass, mis haldab palli positsiooni ja liikumist
public class Ball
{
    public double X { get; private set; } // Palli X-koordinaat
    public double Y { get; private set; } // Palli Y-koordinaat

    private double _vx, _vy; // Palli liikumise kiirus

    private Game _game; // Mäng, kus pall asub

    // Konstruktor, mis määrab palli algpositsiooni ja mängu
    public Ball(double x, double y, Game game)
    {
        _game = game; // Seob palli mänguga
        X = x; // Määrab X-koordinaadi
        Y = y; // Määrab Y-koordinaadi
        this.Draw();
    }

    // Määrab palli kiirus
    public void SetSpeed(double vx, double vy)
    {
        _vx = vx; // X suunaline kiirus
        _vy = vy; // Y suunaline kiirus
    }
    
    // Liigutab palli vastavalt kiirusest
    public void Move()
    {


        double newX = X + _vx; // Uus X-koordinaat
        double newY = Y + _vy; // Uus Y-koordinaat
        Console.SetCursorPosition((int)this.X, (int)this.Y);
        Console.Write(" ");        // Kontrollib, kas uus positsioon on staadionil
        Team? team = _game.Stadium.IsInGates((int)newX, (int)newY);
        if (team is not null)
        {
            this._game.Score.ChangeScore((Team)team);
            this.X = this._game.Stadium.Width / 2;
            this.Y = this._game.Stadium.Height / 2;
        }
        else if (_game.Stadium.IsIn(newX, newY))
        {
            X = newX; // Kui positsioon on sobiv, uuendab X-koordinaati
            Y = newY; // Kui positsioon on sobiv, uuendab Y-koordinaati
        }
        else
        {
            this.X = this._game.Stadium.Width / 2;
            this.Y = this._game.Stadium.Height / 2;
            this._game.Stadium.Draw();
        }

        this.Draw();
    }
    public void Draw()
    {
        ConsoleColor currColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Magenta;

        Console.SetCursorPosition((int)this.X, (int)this.Y);
        Console.Write("*");

        Console.ForegroundColor = currColor;
    }
}
