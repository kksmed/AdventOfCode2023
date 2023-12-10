using System.Text.RegularExpressions;

namespace Day4;

public partial class Parser
{
  readonly int expectedWinningNumbers;
  readonly int expectedNumbersOnCard;

  public Parser(int expectedWinningNumbers, int expectedNumbersOnCard)
  {
    this.expectedWinningNumbers = expectedWinningNumbers;
    this.expectedNumbersOnCard = expectedNumbersOnCard;
  }

  public  (int CardNo, IEnumerable<int> WinningNumbers, IEnumerable<int> NumbersOnCard) ParseLine(string line)
  {
    var match = ScratchcardRegex().Match(line);

    if (!match.Success)
      throw new ArgumentException($"Could not parse line: {line}");

    var cardNos = GetNumbers(match.Groups[1], 1, "card number");
    var cardNo = cardNos.Single();

    var winningNumbers = GetNumbers(match.Groups[2], expectedWinningNumbers, "winning numbers");
    var numbersOnCard = GetNumbers(match.Groups[3], expectedNumbersOnCard, "numbers on card");

    return (cardNo, winningNumbers, numbersOnCard);
  }

  static IEnumerable<int> GetNumbers(Group matchGroup, int expectedAmount, string name)
  {
    if (matchGroup.Captures.Count != expectedAmount) throw new ArgumentException($"Mismatch on {name}.");
    return matchGroup.Captures.Select(x => int.Parse(x.Value));
  }

  [GeneratedRegex(@"^Card +(\d+):( +\d+)+ \|( +\d+)+ *$")]
    private static partial Regex ScratchcardRegex();
}
