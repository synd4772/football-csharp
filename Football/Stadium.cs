namespace Football;

public class Stadium
{
    public char _sym { get; private set; }

    public Stadium(int width, int height, char sym)
    {
        Width = width;
        Height = height;
        this._sym = sym;
    }

    public int Width { get; }

    public int Height { get; }

    public bool IsIn(double x, double y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public void Draw()
    {
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
    }
}