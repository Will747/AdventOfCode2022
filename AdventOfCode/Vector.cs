namespace AdventOfCode;

public struct Vector
{
    public int X;
    public int Y;

    public Vector(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return X + "," + Y;
    }
}