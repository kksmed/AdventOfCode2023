namespace Day1;

public static class DigitsExtractor
{
  public static int GetCalibrationValue(string str)
  {
    var digits = str.Where(char.IsDigit).ToList();
    if (digits.Count < 1) throw new ArgumentException("No digits found.", nameof(str));

    return int.Parse(new string(new[] { digits.First(), digits.Last() }));
  }
}
