namespace Day7;

public record Hand : IComparable<Hand>
{
  public const char Joker = 'J';
  readonly bool withJokers;
  string Cards { get; }

  HandTypes Rank { get; }

  public Hand(string cards, bool withJokers)
  {
    this.withJokers = withJokers;
    Cards = cards;
    Rank = Parser.ParseRank(cards, withJokers);
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

  int CompareCards(char x, char y) => CardRank(x).CompareTo(CardRank(y));

  int CardRank(char c)
  {
    if (withJokers && c == Joker) return 1;

    return c switch
      {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => 11,
        'T' => 10,
        '9' => 9,
        '8' => 8,
        '7' => 7,
        '6' => 6,
        '5' => 5,
        '4' => 4,
        '3' => 3,
        '2' => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, "Unknown card")
      };
  }
}
