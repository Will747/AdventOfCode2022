namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath).Split('\n');
    }

    public override ValueTask<string> Solve_1()
    {
        var highestCalories = 0;
        var totalElfCalories = 0;

        foreach (var input in _input)
        {
            if (input.Length > 1)
            {
                totalElfCalories += int.Parse(input);
            }
            else
            {
                if (highestCalories < totalElfCalories)
                {
                    highestCalories = totalElfCalories;
                }

                totalElfCalories = 0;
            }
        }

        return new ValueTask<string>(highestCalories.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var highestCalories = 0;
        var secondHighestCalories = 0;
        var thirdHighestCalories = 0;

        var totalElfCalories = 0;

        foreach (var input in _input)
        {
            if (input.Length > 1)
            {
                totalElfCalories += int.Parse(input);
            }
            else
            {
                if (highestCalories < totalElfCalories)
                {
                    var temp = highestCalories;
                    var temp2 = secondHighestCalories;
                    highestCalories = totalElfCalories;
                    secondHighestCalories = temp;
                    thirdHighestCalories = temp2;
                } 
                else if (secondHighestCalories < totalElfCalories)
                {
                    var temp = secondHighestCalories;
                    secondHighestCalories = totalElfCalories;
                    thirdHighestCalories = temp;
                } 
                else if (thirdHighestCalories < totalElfCalories)
                {
                    thirdHighestCalories = totalElfCalories;
                }

                totalElfCalories = 0;
            }
        }

        var sumOfTopThree = highestCalories + secondHighestCalories + thirdHighestCalories;

        return new ValueTask<string>(sumOfTopThree.ToString());
    }
}
