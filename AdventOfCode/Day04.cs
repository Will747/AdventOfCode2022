namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var total = 0;
        
        foreach (var input in _input)
        {
            var pairs = input.Split(',');
            var pairOne = pairs[0].Split('-');
            var pairTwo = pairs[1].Split('-');

            var pairOneStart = Convert.ToInt32(pairOne[0]);
            var pairOneEnd = Convert.ToInt32(pairOne[1]);
            var pairTwoStart = Convert.ToInt32(pairTwo[0]);
            var pairTwoEnd = Convert.ToInt32(pairTwo[1]);

            var containedInOne = pairOneStart <= pairTwoStart && pairOneEnd >= pairTwoEnd;
            var containedInTwo = pairOneStart >= pairTwoStart && pairOneEnd <= pairTwoEnd;
            
            if (containedInOne || containedInTwo)
            {
                total++;
            }
        }
        
        return new ValueTask<string>(total.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var total = 0;
        
        foreach (var input in _input)
        {
            var pairs = input.Split(',');
            var pairOne = pairs[0].Split('-');
            var pairTwo = pairs[1].Split('-');

            var pairOneStart = Convert.ToInt32(pairOne[0]);
            var pairOneEnd = Convert.ToInt32(pairOne[1]);
            var pairTwoStart = Convert.ToInt32(pairTwo[0]);
            var pairTwoEnd = Convert.ToInt32(pairTwo[1]);

            var containedInOne = pairOneStart <= pairTwoStart && pairTwoStart <= pairOneEnd;
            var containedInTwo = pairOneStart >= pairTwoStart && pairOneStart <= pairTwoEnd;

            if (containedInOne || containedInTwo)
            {
                total++;
            }
        }
        
        return new ValueTask<string>(total.ToString());
    }
}
