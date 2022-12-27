namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;

    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    private static int GetValueOfChar(char value)
    {
        if (value > 'a')
        {
            return value - 'a' + 1;
        }

        return value - 'A' + 27;
    }
    
    private static char? FindDuplicateChar(string one, string two, string three)
    {
        char? duplicateChar = null;

        var i = 0;
        while (i < one.Length && !duplicateChar.HasValue)
        {
            var character = one.Substring(i, 1);
            if (two.Contains(character) && three.Contains(character))
            {
                duplicateChar = character[0];
            }

            i++;
        }

        return duplicateChar;
    }

    private static char? FindDuplicateChar(string one, string two)
    {
        char? duplicateChar = null;

        var i = 0;
        while (i < one.Length && !duplicateChar.HasValue)
        {
            var character = one.Substring(i, 1);
            if (two.Contains(character))
            {
                duplicateChar = character[0];
            }

            i++;
        }

        return duplicateChar;
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 0;

        foreach (var input in _input)
        {
            var charsPerCompartment = input.Length / 2;
            var compartmentOne = input[..charsPerCompartment];
            var compartmentTwo = input[charsPerCompartment..];

            var duplicateChar = 
                FindDuplicateChar(compartmentOne, compartmentTwo);

            if (duplicateChar.HasValue)
            {
                total += GetValueOfChar(duplicateChar.Value);
            }
        }
        
        return new ValueTask<string>(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var total = 0;

        var x = 0;
        while (x < _input.Length)
        {
            var elfOne = _input[x];
            var elfTwo = _input[x + 1];
            var elfThree = _input[x + 2];

            var duplicateChar = FindDuplicateChar(elfOne, elfTwo, elfThree);

            if (duplicateChar.HasValue)
            {
                total += GetValueOfChar(duplicateChar.Value);
            }

            x += 3;
        }
        
        return new ValueTask<string>(total.ToString());
    }
}
