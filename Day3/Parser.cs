namespace Day3;

public static class Parser
{
  public static (IEnumerable<int> PartNumbers, IEnumerable<Symbol> Gears) ParseToNumbers(string[] lines)
  {
    var allNumbers = new List<Number>();
    var allSymbols = new List<Symbol>();

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
            allNumbers.Add(currentNumber);

            if (currentSymbol is not null)
            {
              currentNumber.Assign(currentSymbol);
              currentSymbol = null;
            }

            foreach (var symbol in symbolsPreviousLine.Where(
              x => x.Position.Column == column - 1 || x.Position.Column == column))
              currentNumber.Assign(symbol);
          }
          else
          {
            currentNumber.Add(c);
          }

          foreach (var symbol in symbolsPreviousLine.Where(x => x.Position.Column == column + 1))
            currentNumber.Assign(symbol);

          continue;
        }

        if (c == '.')
        {
          currentSymbol = null;
          currentNumber = null;
          continue;
        }

        currentSymbol = new Symbol((lineNo, column), c);
        symbolsCurrentLine.Add(currentSymbol);
        allSymbols.Add(currentSymbol);

        if (currentNumber is not null)
        {
          currentNumber.Assign(currentSymbol);
          currentNumber = null;
        }
        foreach (var number in numbersPreviousLine.Where(x => x.IsAdjacentTo(column)))
            number.Assign(currentSymbol);
      }

      numbersPreviousLine = numbersCurrentLine;
      symbolsPreviousLine = symbolsCurrentLine;
    }

    return (allNumbers.Where(x => x.Assigned).Select(x => x.Value),
      allSymbols.Where(x => x is { SymbolChar: '*', Numbers.Count: 2 }));
  }
}

public record Symbol((int Line, int Column) Position, char SymbolChar)
{
  public List<Number> Numbers { get; } = new();
}

