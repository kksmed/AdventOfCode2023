namespace Day7;

public static class Parser
{
  public static Bet ParseBet(string line)
  {
    var split = line.Split(" ");
    if (split.Length != 2) throw new ArgumentException($"Unknown format: {line}", nameof(line));

    return new Bet(new Hand(split[0]), int.Parse(split[1]));
  }

  public static HandTypes ParseRank(string cards)
  {
    var grouped = cards.GroupBy(x => x).OrderByDescending(x => x.Count()).ToList();

    if (grouped.Count == 1) return HandTypes.FiveOfAKind;

    var mostOfAKind = grouped[0].Count();
    var secondMostOfAKind = grouped[1].Count();

    return mostOfAKind switch
      {
        4 => HandTypes.FourOfAKind,
        3 => secondMostOfAKind == 2 ? HandTypes.FullHouse : HandTypes.ThreeOfAKind,
        2 => secondMostOfAKind == 2 ? HandTypes.TwoPair : HandTypes.OnePair,
        _ => HandTypes.HighCard
      };
  }
}

public record Bet(Hand Hand, int Amount);