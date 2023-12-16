namespace Day7;

public record Hand : IComparable<Hand>
{
  string Cards { get; }

  HandTypes Rank { get; }

  public Hand(string cards)
  {
    this.Cards = cards;
    Rank = Parser.ParseRank(cards);
  }

  public int CompareTo(Hand? other)
  {
    if (other is null)
      return -1;

    var compareRank = Rank.CompareTo(other.Rank);
    return compareRank != 0 ? compareRank : CompareHands(other.Cards);
  }

  int CompareHands(string otherCards) =>
    Cards.Select((c, i) => CompareCards(c, otherCards[i])).FirstOrDefault(x => x != 0, 0);

  static int CompareCards(char x, char y) => CardRank(x).CompareTo(CardRank(y));

  static int CardRank(char c)
  {
    return c switch
      {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'T' => 10,
        '9' => 9,
        '8' => 8,
        '7' => 7,
        '6' => 6,
        '5' => 5,
        '4' => 4,
        '3' => 3,
        '2' => 2,
        'J' => 1,
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, "Unknown card")
      };
  }
}
