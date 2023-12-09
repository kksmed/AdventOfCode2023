namespace Day3;

public static class Parser
{
  public static IEnumerable<int> ParseToNumbers(string str)
  {
    var lines = str.Split(Environment.NewLine);

    var partNumbers = new List<Number>();

    var numbersPreviousLine = new List<Number>(0);
    var symbolsPreviousLine = new List<Symbol>(0);
    for(var lineNo = 0; lineNo < lines.Length; lineNo++)
    {
      var line = lines[lineNo];
      var numbersCurrentLine = new List<Number>();
      var symbolsCurrentLine = new List<Symbol>();
      Number? currentNumber = null;
      Symbol? currentSymbol = null;
      for (var column = 0; column < line.Length; column++)
      {
        var c = line[column];
        if (char.IsDigit(c))
        {
          if (currentNumber is null)
          {
            currentNumber = new Number(lineNo, column, c);
            numbersCurrentLine.Add(currentNumber);
            if (currentSymbol is not null ||
              symbolsPreviousLine.Any(x => x.Position.Column == column - 1 || x.Position.Column == column ))
            {
              partNumbers.Add(currentNumber);
              currentSymbol = null;

            }
          }
          else
          {
            currentNumber.Add(c);
          }

          if (symbolsPreviousLine.Any(x => x.Position.Column == column + 1))
            partNumbers.Add(currentNumber);

          continue;
        }

        if (c == '.')
        {
          currentSymbol = null;
          currentNumber = null;
          continue;
        }

        if (currentNumber is not null)
        {
          partNumbers.Add(currentNumber);
          currentNumber = null;
        }
        else
        {
          var number = numbersPreviousLine.FirstOrDefault(x => x.IsAdjacentTo(column));
          if (number is not null)
            partNumbers.Add(number);
        }

        currentSymbol = new Symbol((lineNo, column), c);
        symbolsCurrentLine.Add(currentSymbol);
      }

      numbersPreviousLine = numbersCurrentLine;
      symbolsPreviousLine = symbolsCurrentLine;
    }

    return partNumbers.Select(x => x.Value);
  }
}

class Number
{
  readonly List<char> digits = new();
  readonly int line, start;
  int end;

  public (int Line, int Start, int End) Positions => (line, start, end);

  public int Value => int.Parse(digits.ToArray());

  public Number(int line, int startPosition, char startDigit)
  {
    this.line = line;
    start = startPosition;
    end = startPosition;
    digits.Add(startDigit);
  }

  public void Add(char digit)
  {
    end++;
    digits.Add(digit);
  }

  public bool IsAdjacentTo(int column) => column >= start - 1 && column <= end + 1;
}

record Symbol((int Line, int Column) Position, char SymbolChar);