namespace Day4;

public static class Scorer
{
  public static int Score(IEnumerable<int> winningNumbers, IEnumerable<int> numbersOnCard)
  {
    var count = winningNumbers.Intersect(numbersOnCard).Count();
    return count < 3 ? count : 1 << (count - 1);
  }

  public static int CountTotalCards(
    List<(int CardNo, IEnumerable<int> WinningNumbers, IEnumerable<int> NumbersOnCard)> valueTuples)
  {
    var cardsTotal = 0;
    var multiplier = Enumerable.Repeat(1, valueTuples.Count).ToArray();
    for (var i = 0; i < valueTuples.Count; i++)
    {
      var card = valueTuples[i];
      var amount = multiplier[i];
      cardsTotal += amount;
      Console.Write($"Card {card.CardNo} (x{amount}):");
      var matches = card.WinningNumbers.Intersect(card.NumbersOnCard).ToList();
      Console.WriteLine($" matches: {matches.Count} ({string.Join(",", matches)})");

      for (var m = 1; m <= matches.Count; m++)
      {
        multiplier[i + m] += amount;
      }
    }

    return cardsTotal;
  }
}
