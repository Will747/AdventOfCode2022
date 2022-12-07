namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly List<char>[] _stacks1;
    private readonly List<char>[] _stacks2;
    private readonly string[] _commands;

    public Day05()
    {
        var input = File.ReadAllText(InputFilePath).Split('\n');
    
        // Find the line number where the bottom of the stack starts
        var stackStartIdx = -1;
        var numOfStacks = 0;
        var i = 0;
        while(stackStartIdx == -1) 
        {
            if (input[i][1] == '1') 
            {
                stackStartIdx = i - 1;
                numOfStacks = input[i].Length / 4 + 1;
            }
            i++;
        }

        // Initialize all lists
        _stacks1 = new List<char>[numOfStacks];
        _stacks2 = new List<char>[numOfStacks];
        for (var x = 0; x < numOfStacks; x++) 
        {
            _stacks1[x] = new List<char>();
            _stacks2[x] = new List<char>();
        }

        // Generate stacks
        for (var x = stackStartIdx; x >= 0; x--) 
        {
            for (var y = 0; y < numOfStacks; y++) 
            {
                var item = input[x][y * 4 + 1];
                if (item != ' ') {
                    _stacks1[y].Add(item);
                    _stacks2[y].Add(item);
                }
            }
        }

        // Get all commands
        var commandStartIdx = stackStartIdx + 3;
        var numOfCommands = input.Length - commandStartIdx;
        _commands = new string[numOfCommands];
        for (var x = 0; x < _commands.Length; x++) 
        {
            _commands[x] = input[x + commandStartIdx];
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var stacks = _stacks1;
        foreach (var command in _commands) 
        {
            var components = command.Split(' ');
            var numToMove = Convert.ToInt32(components[1]);
            var removeFrom = Convert.ToInt32(components[3]) - 1;
            var moveTo = Convert.ToInt32(components[5]) - 1;

            var numInInitialStack = stacks[removeFrom].Count;
            var startStackIdx = numInInitialStack - numToMove;
            var itemsBeingMoved = stacks[removeFrom].GetRange(startStackIdx, numToMove);
            stacks[removeFrom].RemoveRange(startStackIdx, numToMove);

            for (var i = itemsBeingMoved.Count - 1; i >= 0; i--) 
            {
                stacks[moveTo].Add(itemsBeingMoved[i]);
            }
        }

        var message = stacks.Aggregate("", (current, stack) => current + stack[^1]);

        return new ValueTask<string>(message);
    }

    public override ValueTask<string> Solve_2()
    {
        var stacks = _stacks2;
        foreach (var command in _commands) 
        {
            var components = command.Split(' ');
            var numToMove = Convert.ToInt32(components[1]);
            var removeFrom = Convert.ToInt32(components[3]) - 1;
            var moveTo = Convert.ToInt32(components[5]) - 1;

            var numInInitialStack = stacks[removeFrom].Count;
            var startStackIdx = numInInitialStack - numToMove;
            var itemsBeingMoved = stacks[removeFrom].GetRange(startStackIdx, numToMove);
            stacks[removeFrom].RemoveRange(startStackIdx, numToMove);
            stacks[moveTo].AddRange(itemsBeingMoved);
        }

        var message = stacks.Aggregate("", (current, stack) => current + stack[^1]);

        return new ValueTask<string>(message);
    }
}
