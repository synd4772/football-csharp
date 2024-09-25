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
        // Kontrollib, kas uus positsioon on staadionil
        if (_game.Stadium.IsIn(newX, newY))
        {
            X = newX; // Kui positsioon on sobiv, uuendab X-koordinaati
            Y = newY; // Kui positsioon on sobiv, uuendab Y-koordinaati
        }
        else
        {
            _vx = 0; // Kui positsioon on vale, peatab X-kiirus
            _vy = 0; // Kui positsioon on vale, peatab Y-kiirus
        }
    }

}
