namespace AdventOfCode;

public class Day09 : BaseDay
{
    private readonly string[] _input;

    public Day09()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    private struct Vector
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

    private static bool MoveTail(Vector head, ref Vector tail)
    {
        var dist = new Vector
        {
            X = head.X - tail.X,
            Y = head.Y - tail.Y
        };

        var bChanged = false;
        
        if (Math.Abs(dist.X) > 1 || Math.Abs(dist.Y) > 1)
        {
            if (head.X != tail.X)
            {
                tail.X += dist.X / Math.Abs(dist.X);
                bChanged = true;
            }

            if (head.Y != tail.Y)
            {
                tail.Y += dist.Y / Math.Abs(dist.Y);
                bChanged = true;
            }
        }

        return bChanged;
    }

    private Vector GetInstruction(string instruction)
    {
        var diff = new Vector(0, 0);

        switch(instruction) {
            case "U":
                diff.Y = 1;
                break;
            case "D":
                diff.Y = -1;
                break;
            case "L":
                diff.X = -1;
                break;
            case "R":
                diff.X = 1;
                break;
        }

        return diff;
    }
    
    public override ValueTask<string> Solve_1()
    {
        var visitedPositions = new HashSet<string>();

        var head = new Vector(0, 0);
        var tail = new Vector(0, 0);
        visitedPositions.Add(tail.X + "," + tail.Y);
        
        foreach(var line in _input) {
            var instruction = line.Split(' ');

            var numOfMoves = Convert.ToInt32(instruction[1]);
            var diff = GetInstruction(instruction[0]);

            for (var i = 0; i < numOfMoves; i++) {
                // Move the head
                head.X += diff.X;
                head.Y += diff.Y;
                
                // Move the tail
                if (MoveTail(head, ref tail))
                {
                    visitedPositions.Add(tail.ToString());
                }
            }
        }

        return new ValueTask<string>(visitedPositions.Count.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var visitedPositions = new HashSet<string>();
        var knots = new Vector[10];
        visitedPositions.Add(knots[9].X + "," + knots[9].Y);

        foreach (var line in _input)
        {
            var instruction = line.Split(' ');

            var numOfMoves = Convert.ToInt32(instruction[1]);
            var diff = GetInstruction(instruction[0]);

            for (var i = 0; i < numOfMoves; i++)
            {
                // Move the head
                knots[0].X += diff.X;
                knots[0].Y += diff.Y;

                // Move all other knots
                for (var x = 1; x < knots.Length - 1; x++)
                {
                    MoveTail(knots[x - 1], ref knots[x]);
                }
                
                // Move last knot
                if (MoveTail(knots[^2], ref knots[^1]))
                {
                    visitedPositions.Add(knots[^1].ToString());
                }
            }
        }

        return new ValueTask<string>(visitedPositions.Count.ToString());
    }
}