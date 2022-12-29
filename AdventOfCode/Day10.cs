namespace AdventOfCode;

public class Day10 : BaseDay
{ 
    private readonly string[] _input;
    private int _time;
    private int _x;
    private int _signalStrength;
    
    private string _crtView;
    private int _crtPos;

    public Day10()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    private void IncrementTime()
    {
        _time++;

        var i = _time - 20;
        if (_time == 20)
        {
            _signalStrength += _time * _x;
        } else if (i % 40 == 0)
        {
            _signalStrength += _time * _x;
        }
    }
    
    public override ValueTask<string> Solve_1()
    {
        _time = 0;
        _x = 1;
        _signalStrength = 0;

        foreach (var line in _input)
        {
            if (line.Equals("noop"))
            {
                IncrementTime();
            }
            else
            {
                var args = line.Split(' ');
                IncrementTime();
                IncrementTime();
                _x += Convert.ToInt32(args[1]);
            }
        }
        
        return new ValueTask<string>(_signalStrength.ToString());
    }

    private void IncrementTime2()
    {
        _time++;

        if (_crtPos == 40)
        {
            _crtPos = 0;
            _crtView += "\n";
        }

        if (_crtPos == _x || _crtPos == _x + 1 || _crtPos == _x - 1)
        {
            _crtView += "#";
        }
        else
        {
            _crtView += ".";
        }

        _crtPos++;
    }
    
    public override ValueTask<string> Solve_2()
    {
        _time = 0;
        _x = 1;
        _crtPos = 0;
        _crtView = "";

        foreach (var line in _input)
        {
            if (line.Equals("noop"))
            {
                IncrementTime2();
            }
            else
            {
                var args = line.Split(' ');
                IncrementTime2();
                IncrementTime2();
                _x += Convert.ToInt32(args[1]);
            }
        }

        return new ValueTask<string>(_crtView);
    }
}
