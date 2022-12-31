namespace AdventOfCode;

public class Day12 : BaseDay
{
    private readonly int[,] _heightMap;
    private readonly Vector _startPos;
    private readonly Vector _endPos;
    
    public Day12()
    {
        var input = File.ReadAllLines(InputFilePath);
        _heightMap = new int[input[0].Length, input.Length];
        
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'S')
                {
                    _startPos = new Vector(x, y);
                } else if (input[y][x] == 'E')
                {
                    _endPos = new Vector(x, y);
                    _heightMap[x, y] = 'z' - 'a';
                }
                else
                {
                    _heightMap[x, y] = input[y][x] - 'a';   
                }
            }
        }
    }

    private bool IsPossibleMove(Vector vec1, Vector vec2)
    {
        // Check vec2 is within the heightmap
        if (vec2.X >= 0
            && vec2.X < _heightMap.GetLength(0)
            && vec2.Y >= 0
            && vec2.Y < _heightMap.GetLength(1))
        {
            var diff = _heightMap[vec2.X, vec2.Y] - _heightMap[vec1.X, vec1.Y];
            if (diff >= -1)
            {
                return true;
            }
            
        }
        
        return false;
    }

    private int[,] RunBFS()
    {
        // Run Breadth-first-search on the height map starting from the EndPos working backwards
        var distance = new int[_heightMap.GetLength(0), _heightMap.GetLength(1)];
        
        // Set distance to all nodes to max
        for (var x = 0; x < distance.GetLength(0); x++)
        {
            for (var y = 0; y < distance.GetLength(1); y++)
            {
                distance[x, y] = int.MaxValue;   
            }
        }

        var queue = new Queue<Vector>();
        queue.Enqueue(_endPos);
        distance[_endPos.X, _endPos.Y] = 0;

        while (queue.Count != 0)
        {
            var node = queue.Dequeue();

            var connectedNodes = new List<Vector>();

            // Up
            var upVec = new Vector(node.X, node.Y - 1);
            if (IsPossibleMove(node, upVec)) {
                connectedNodes.Add(upVec);
            }
            
            // Down
            var downVec = new Vector(node.X, node.Y + 1);
            if (IsPossibleMove(node, downVec)) {
                connectedNodes.Add(downVec);
            }
            
            // Left
            var leftVec = new Vector(node.X - 1, node.Y);
            if (IsPossibleMove(node, leftVec)) {
                connectedNodes.Add(leftVec);
            }

            // Right
            var rightVec = new Vector(node.X + 1, node.Y);
            if (IsPossibleMove(node, rightVec)) {
                connectedNodes.Add(rightVec);
            }

            foreach (var connectedNode in connectedNodes)
            {
                var newDist = distance[node.X, node.Y] + 1;
                if (distance[connectedNode.X, connectedNode.Y] > newDist)
                {
                    distance[connectedNode.X, connectedNode.Y] = newDist;
                    queue.Enqueue(connectedNode);
                }
            }
        }

        return distance;
    }
    
    public override ValueTask<string> Solve_1()
    {
        var distance = RunBFS();
        return new ValueTask<string>(distance[_startPos.X, _startPos.Y].ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var distance = RunBFS();
        var shortestDistance = Int32.MaxValue;
        
        // Search through heightmap for points with height of 0
        for (var x = 0; x < distance.GetLength(0); x++)
        {
            for (var y = 0; y < distance.GetLength(1); y++)
            {
                if (_heightMap[x, y] == 0 && shortestDistance > distance[x, y])
                {
                    shortestDistance = distance[x, y];
                }
            }
        }
        
        return new ValueTask<string>(shortestDistance.ToString());
    }
}