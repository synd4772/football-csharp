using System.Drawing;

namespace Football;

public class Stadium
{
    public char _sym { get; private set; }
    public Team homeTeam { get; private set; }
    public Team awayTeam { get; private set; }

    public int[,] homeTeamPosition { get; private set; }
    public int[,] awayTeamPosition { get; private set; }

    public Stadium(int width, int height, char sym, Team homeTeam, Team awayTeam)
    {
        Width = width;
        Height = height;
        this._sym = sym;

        this.homeTeam = homeTeam;
        this.awayTeam = awayTeam;

        this.homeTeamPosition = new int[2, 2]
        {
            { 0, 3}, { 5, this.Height - 5}
        };
        this.awayTeamPosition = new int[2, 2]
        {
            { this.Width, this.Width - 3 }, { 5, this.Height - 5 }
        };

    }


    public int Width { get; }

    public int Height { get; }

    public bool IsIn(double x, double y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public void Draw()
    {
        Console.ForegroundColor = ConsoleColor.Gray;

        for (int i = 0; i <= this.Width; i++)
        {

            Console.SetCursorPosition(i, 0);
            Console.Write(this._sym);

            Console.SetCursorPosition(i, this.Height);
            Console.Write(this._sym);
        }

        for (int i = 0; i <= this.Height; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(this._sym);

            Console.SetCursorPosition(this.Width, i);
            Console.Write(this._sym);
        }
        
        ConsoleColor color = Console.BackgroundColor;

        for (int i = this.homeTeamPosition[1,0]; i <= this.homeTeamPosition[1, 1]; i++)
        {
            for (int j = this.homeTeamPosition[0, 0]; j <= this.homeTeamPosition[0, 1]; j++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(j, i);
                Console.Write(" ");
            }
        }
        for (int i = this.awayTeamPosition[1, 0]; i <= this.awayTeamPosition[1, 1]; i++)
        {
            for (int j = this.awayTeamPosition[0, 1]; j <= this.awayTeamPosition[0, 0]; j++)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(j, i);
                Console.Write(" ");
            }
        }
        Console.BackgroundColor = color;

    }

    public Team? IsInGates(int x, int y)
    {
        List<int[,]> positions = new List<int[,]>()
        {
            this.homeTeamPosition, this.awayTeamPosition
        };
        foreach (int[,] position in positions)
        {
            Console.SetCursorPosition(0, Height + 2);
            Console.Write($"{(position == this.homeTeamPosition ? "homeTeam" : "awayTeam")}{x} >= {position[0, 0]} && {x} <= {position[0, 1]} && {y} >= {position[1, 0]} && {y} <= {position[1, 1]} : {x >= position[0, 0] && x <= position[0, 1] && y >= position[1, 0] && y <= position[1, 1]}");
            Console.ReadKey();
            if (x >= position[0, 0] && x <= position[0, 1] && y >= position[1, 0] && y <= position[1, 1])
            {
                return position == this.homeTeamPosition ? this.homeTeam : this.awayTeam;
            }
        }
        return null;
    }
}