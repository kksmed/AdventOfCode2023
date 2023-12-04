namespace Day1;

public static class DigitsExtractor
{
  static (string Word, string Number)[] digitWords =
    {
      ("one", "1one"),
      ("two", "2two"),
      ("three", "three"),
      ("four", "4four"),
      ("five", "5five"),
      ("six", "6six"),
      ("seven", "7seven"),
      ("eight", "8eight"),
      ("nine", "9nine")
    };

  internal static string ReplaceDigitWords(string str) => digitWords.Aggregate(
    str,
    (current, p) => current.Replace(p.Word, p.Number.ToString()));

  public static int GetCalibrationValue(string str)
  {
    var strWithDigits = ReplaceDigitWords(str);
    var digits = strWithDigits.Where(char.IsDigit).ToList();
    if (digits.Count < 1) throw new ArgumentException("No digits found.", nameof(str));

    return int.Parse(new string(new[] { digits.First(), digits.Last() }));
  }
}
