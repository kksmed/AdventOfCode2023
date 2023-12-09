namespace Day3;

public static class Parser
{
  public static IEnumerable<int> ParseToNumbers(string[] lines)
  {
    var allNumbers = new List<Number>();

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
            allNumbers.Add(currentNumber);
            numbersCurrentLine.Add(currentNumber);
            if (currentSymbol is not null ||
              symbolsPreviousLine.Any(x => x.Position.Column == column - 1 || x.Position.Column == column ))
            {
              currentNumber.Assign();
              currentSymbol = null;

            }
          }
          else
          {
            currentNumber.Add(c);
          }

          if (symbolsPreviousLine.Any(x => x.Position.Column == column + 1))
            currentNumber.Assign();

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
          currentNumber.Assign();
          currentNumber = null;
        }
        foreach (var number in numbersPreviousLine.Where(x => x.IsAdjacentTo(column)))
            number.Assign();

        currentSymbol = new Symbol((lineNo, column), c);
        symbolsCurrentLine.Add(currentSymbol);
      }

      numbersPreviousLine = numbersCurrentLine;
      symbolsPreviousLine = symbolsCurrentLine;
    }

    Console.WriteLine("Unassigned:");
    foreach (var number in allNumbers.Where(x => !x.Assigned))
    {
      Console.WriteLine(number);
    }
    return allNumbers.Where(x => x.Assigned).Select(x => x.Value);
  }
}

record Symbol((int Line, int Column) Position, char SymbolChar);