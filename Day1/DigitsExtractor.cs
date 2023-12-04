namespace Day1;

public static class DigitsExtractor
{
  public static (int, int) GetDigits(string str)
  {
    var digits = str.Where(char.IsDigit).ToList();
    if (digits.Count < 2) throw new ArgumentException("Not two digits found.", nameof(str));

    return (digits.First(), digits.Last());
  }
}
