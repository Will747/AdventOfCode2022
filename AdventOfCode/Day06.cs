namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string _input;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private int FindMarker(int length) {
        for (var i = 0; i < _input.Length - 3; i++) {
            var characters = new char[length];
            for (var x = 0; x < length; x++) {
                characters[x] = _input[i + x];
            }

            // Check if there are no duplicate characters
            var matchingCharacter = false;
            for (var x = 0; x < length; x++) {
                for (var y = x + 1; y < length; y++) {
                    if (characters[x] == characters[y]) {
                        matchingCharacter = true;
                    }
                }
            }

            if (!matchingCharacter) {
                return i + length;
            }
        }

        return -1;
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>(FindMarker(4).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>(FindMarker(14).ToString());
    }
}
