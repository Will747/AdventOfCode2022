namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    private int OpponentCode(char code) {
        switch (code) {
            case 'A':
            return 1; // rock
            case 'B':
            return 2; // paper
            case 'C':
            return 3; // scissors
            default:
            return 0;
        }
    }

    private int PlayerCode(char code) {
        switch (code) {
            case 'X':
            return 1; // rock
            case 'Y':
            return 2; // paper
            case 'Z':
            return 3; // scissors
            default:
            return 0;
        }
    }

    public override ValueTask<string> Solve_1()
    {
        int totalScore = 0;

        foreach (var input in _input)
        {
            int opponentValue = OpponentCode(input.First());
            int playerValue = PlayerCode(input.Last());

            bool won = false;
            // opponent == rock && player == paper
            won = opponentValue == 1 && playerValue == 2;

            // opponent == paper && player == scissors
            won |= opponentValue == 2 && playerValue == 3;

            // oponent == scissors && player == rock
            won |= opponentValue == 3 && playerValue == 1;

            if (won) {
                totalScore += 6;
            } 
            else if (opponentValue == playerValue)
            {
                totalScore += 3;
            }

            totalScore += playerValue;
        }
        return new ValueTask<string>(totalScore.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int totalScore = 0;

        foreach (var input in _input)
        {
            int opponentValue = OpponentCode(input.First());
            char roundEnd = input.Last();

            int move = -1;
            if (roundEnd == 'X') {
                // Lose
                move = opponentValue - 1;
                if (move < 1) {
                    move = 3;
                }
            }
            else if (roundEnd == 'Y') 
            {
                // Draw
                move = opponentValue;
                totalScore += 3;
            }
            else if (roundEnd == 'Z') 
            {
                // Win
                move = opponentValue + 1;
                if (move > 3) {
                    move = 1;
                }

                totalScore += 6;
            }

            totalScore += move;
        }
        return new ValueTask<string>(totalScore.ToString());
    }
}
