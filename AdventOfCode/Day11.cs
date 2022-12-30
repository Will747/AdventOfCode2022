namespace AdventOfCode;

public class Day11 : BaseDay
{
    private struct Monkey
    {
        public List<long> Items;
        public string Operation;
        public int DivisibleBy;
        public int TrueIdx;
        public int FalseIdx;
        public int ItemsThrown;
    }

    private readonly int _mod;
    private readonly Monkey[] _monkeys;
    private readonly Monkey[] _monkeys2;
    
    public Day11()
    {
        var lines = File.ReadAllLines(InputFilePath);
        var numOfMonkeys = (lines.Length + 1) / 7;
        _mod = 1;
        _monkeys = new Monkey[numOfMonkeys];

        // Read all monkeys
        for (var i = 0; i < lines.Length; i += 7)
        {
            var monkeyIdx = i / 7;
            
            // Read initial items for this monkey
            var itemsLine = lines[i + 1].Split(' ');
            var items = new List<long>();
            for (var x = 4; x < itemsLine.Length; x++)
            {
                items.Add(Convert.ToInt32(itemsLine[x].Trim(',')));
            }

            _monkeys[monkeyIdx].Items = items;
            
            // Read operation
            var operationLine = lines[i + 2].Split(':');
            _monkeys[monkeyIdx].Operation = operationLine[1].Trim();
            
            // Read test
            var testLine = lines[i + 3].Split(' ');
            _monkeys[monkeyIdx].DivisibleBy = Convert.ToInt32(testLine[5]);
            
            // Read if true line
            var trueLine = lines[i + 4].Split(' ');
            _monkeys[monkeyIdx].TrueIdx = Convert.ToInt32(trueLine[9]);
            
            // Read if false line
            var falseLine = lines[i + 5].Split(' ');
            _monkeys[monkeyIdx].FalseIdx = Convert.ToInt32(falseLine[9]);

            _mod *= _monkeys[monkeyIdx].DivisibleBy;
        }

        // Duplicate monkeys for the second problem
        _monkeys2 = new Monkey[numOfMonkeys];
        for (var i = 0; i < _monkeys.Length; i++)
        {
            _monkeys2[i].Items = new List<long>();
            foreach (var item in _monkeys[i].Items)
            {
                _monkeys2[i].Items.Add(item);
            }

            _monkeys2[i].DivisibleBy = _monkeys[i].DivisibleBy;
            _monkeys2[i].Operation = _monkeys[i].Operation;
            _monkeys2[i].TrueIdx = _monkeys[i].TrueIdx;
            _monkeys2[i].FalseIdx = _monkeys[i].FalseIdx;
        }
    }

    private static long ExecuteOperation(string op, long old)
    {
        var parts = op.Split(' ');

        var result = old;
        var secondParam = parts[4] == "old" ? old : Convert.ToInt32(parts[4]);

        switch (parts[3])
        {
            case "*":
                result *= secondParam;
                break;
            default:
                result += secondParam;
                break;
        }

        return result;
    }
    
    private void RunRound()
    {
        for (var i = 0; i < _monkeys.Length; i++)
        {
            foreach (var item in _monkeys[i].Items)
            {
                var newItem = ExecuteOperation(_monkeys[i].Operation, item) / 3;

                var throwToIdx =
                    newItem % _monkeys[i].DivisibleBy == 0 ?
                        _monkeys[i].TrueIdx : _monkeys[i].FalseIdx;

                _monkeys[throwToIdx].Items.Add(newItem);
                _monkeys[i].ItemsThrown++;
            }
            
            _monkeys[i].Items.Clear();
        }
    }
    
    public override ValueTask<string> Solve_1()
    {
        for (var i = 0; i < 20; i++)
        {
            RunRound();   
        }

        var highestNumThrown = 0;
        var secondHighestNumThrown = 0;
        foreach (var monkey in _monkeys)
        {
            if (highestNumThrown < monkey.ItemsThrown)
            {
                secondHighestNumThrown = highestNumThrown;
                highestNumThrown = monkey.ItemsThrown;
            }
            else if (secondHighestNumThrown < monkey.ItemsThrown)
            {
                secondHighestNumThrown = monkey.ItemsThrown;
            }
        }
        
        return new ValueTask<string>((highestNumThrown * secondHighestNumThrown).ToString());
    }
    
    private void RunRound2()
    {
        for (var i = 0; i < _monkeys2.Length; i++)
        {
            foreach (var item in _monkeys2[i].Items)
            {
                var newItem = ExecuteOperation(_monkeys2[i].Operation, item) % _mod;

                var throwToIdx =
                    newItem % _monkeys2[i].DivisibleBy == 0 ?
                        _monkeys2[i].TrueIdx : _monkeys[i].FalseIdx;

                _monkeys2[throwToIdx].Items.Add(newItem);
                _monkeys2[i].ItemsThrown++;
            }
            
            _monkeys2[i].Items.Clear();
        }
    }

    public override ValueTask<string> Solve_2()
    {
        for (var i = 0; i < 10000; i++)
        {
            RunRound2();
        }

        var highestNumThrown = 0;
        var secondHighestNumThrown = 0;
        foreach (var monkey in _monkeys2)
        {
            if (highestNumThrown < monkey.ItemsThrown)
            {
                secondHighestNumThrown = highestNumThrown;
                highestNumThrown = monkey.ItemsThrown;
            }
            else if (secondHighestNumThrown < monkey.ItemsThrown)
            {
                secondHighestNumThrown = monkey.ItemsThrown;
            }
        }
        
        return new ValueTask<string>(((long)highestNumThrown * secondHighestNumThrown).ToString());
    }
}