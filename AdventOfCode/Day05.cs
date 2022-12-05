namespace AdventOfCode;

public class Day05 : BaseDay
{
    private List<char>[] stacks1;
    private List<char>[] stacks2;
    private readonly string[] _commands;

    public Day05()
    {
        String[] input = File.ReadAllText(InputFilePath).Split('\n');
    
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
        stacks1 = new List<char>[numOfStacks];
        stacks2 = new List<char>[numOfStacks];
        for (int x = 0; x < numOfStacks; x++) 
        {
            stacks1[x] = new List<char>();
            stacks2[x] = new List<char>();
        }

        // Generate stacks
        for (int x = stackStartIdx; x >= 0; x--) 
        {
            for (int y = 0; y < numOfStacks; y++) 
            {
                char item = input[x][y * 4 + 1];
                if (item != ' ') {
                    stacks1[y].Add(item);
                    stacks2[y].Add(item);
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
        var stacks = stacks1;
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

        var message = "";
        foreach (var stack in stacks) 
        {
            message += stack[stack.Count - 1];
        }

        return new ValueTask<string>(message);
    }

    public override ValueTask<string> Solve_2()
    {
        var stacks = stacks2;
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

        var message = "";
        foreach (var stack in stacks) 
        {
            message += stack[stack.Count - 1];
        }

        return new ValueTask<string>(message);
    }
}
