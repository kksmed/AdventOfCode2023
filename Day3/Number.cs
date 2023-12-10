// LEGAL INFORMATION:
// This source code is protected as intellectual property in accordance with the applicable user
// license and terms, which any user must accept prior to reading or handling the source code,
// cf.: http://www.edlund.dk/legal/TermsAndConditions.html

namespace Day3;

public class Number
{
  readonly List<char> digits = new();
  readonly int line, start;
  int end;

  public (int Line, int Start, int End) Positions => (line, start, end);

  public int Value => int.Parse(digits.ToArray());

  public bool Assigned { get; private set; }

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

  public void Assign(Symbol s)
  {
    if (Assigned)
      throw new InvalidOperationException($"Number {this} has already been assigned.");

    s.Numbers.Add(this);
    Assigned = true;
  }

  public override string ToString() => $"'{Value}' ({line}, {start}-{end})";
}
