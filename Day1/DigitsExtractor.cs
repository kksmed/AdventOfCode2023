using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Day1.Test")]

namespace Day1;

public static class DigitsExtractor
{
  static (string Search, int Digit)[] searchDigits =
    {
      ("0", 0),
      ("1", 1),
      ("2", 2),
      ("3", 3),
      ("4", 4),
      ("5", 5),
      ("6", 6),
      ("7", 7),
      ("8", 8),
      ("9", 9),
      ("one", 1),
      ("two", 2),
      ("three", 3),
      ("four", 4),
      ("five", 5),
      ("six", 6),
      ("seven", 7),
      ("eight", 8),
      ("nine", 9)
    };

  public static int GetCalibrationValue(string str)
  {
    var first = searchDigits.Select(x => (Index: str.IndexOf(x.Search, StringComparison.Ordinal), x.Digit))
      .Where(x => x.Index >= 0).MinBy(x => x.Index).Digit;
    var last = searchDigits.Select(x => (Index: str.LastIndexOf(x.Search, StringComparison.Ordinal), x.Digit))
      .Where(x => x.Index >= 0).MaxBy(x => x.Index).Digit;
    return first * 10 + last;
  }
}
