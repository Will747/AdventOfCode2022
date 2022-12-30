namespace AdventOfCode;

public class BlankDay : BaseDay
{
    private readonly string[] _input;
    
    public BlankDay()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>("");
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>("");
    }
}