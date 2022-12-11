namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly int[,] _input;

    public Day08()
    {
        String[] input = File.ReadAllText(InputFilePath).Split('\n');
        int xSize = input[0].Length;

        _input = new int[xSize, input.Count()];
        
        for (var y = 0; y < input.Count(); y++) {
            for (var x = 0; x < xSize; x++) {
                _input[x, y] = input[y][x];
            }
        }
    }

    private bool IsVisibleTree(int x, int y, int xDirection, int yDirection) {
        int heightOfTree = _input[x, y];
        bool visible = true;
        x += xDirection;
        y += yDirection;
        while (visible && x >= 0 && x < _input.GetLength(0) && y >= 0 && y < _input.GetLength(1)) {
            if (_input[x, y] >= heightOfTree)
            {
                visible = false;
            }
            x += xDirection;
            y += yDirection;
        }

        return visible;
    }

    private int getVisibleDistance(int x, int y, int xDirection, int yDirection) {
        int heightOfTree = _input[x, y];
        int distance = 0;
        bool visible = true;
        x += xDirection;
        y += yDirection;
        while (visible && x >= 0 && x < _input.GetLength(0) && y >= 0 && y < _input.GetLength(1)) {
            if (_input[x, y] >= heightOfTree)
            {
                visible = false;
            }
            distance++;
            x += xDirection;
            y += yDirection;
        }

        return distance;
    }


    public override ValueTask<string> Solve_1()
    {
        int total = 0;
        // Count number of tree on the edges of the forest
        total += _input.GetLength(0) * 2;
        total += (_input.GetLength(1) - 2) * 2;

        // Check all central trees (Trees that aren't on the edge)
        for (var x = 1; x < _input.GetLength(0) - 1; x++) {
            for (var y = 1; y < _input.GetLength(1) - 1; y++) {
                bool visible =
                // Check if visible from any direction
                IsVisibleTree(x, y, 0, 1) // UP
                || IsVisibleTree(x, y, 0, -1) // DOWN
                || IsVisibleTree(x, y, 1, 0) // RIGHT
                || IsVisibleTree(x, y, -1, 0); //LEFT

                if (visible) {
                    total++;
                }
            }
        }
        return new ValueTask<string>(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int bestScenicScore = 0;

        for (var x = 0; x < _input.GetLength(0); x++) {
            for (var y = 0; y < _input.GetLength(1); y++) {
                int score =
                // Check if visible from any direction
                getVisibleDistance(x, y, 0, 1) // UP
                * getVisibleDistance(x, y, 0, -1) // DOWN
                * getVisibleDistance(x, y, 1, 0) // RIGHT
                * getVisibleDistance(x, y, -1, 0); //LEFT

                if (score > bestScenicScore) {
                    bestScenicScore = score;
                }
            }
        }

        return new ValueTask<string>(bestScenicScore.ToString());
    }
}
