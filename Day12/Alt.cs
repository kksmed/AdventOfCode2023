using System.Text;
using System.Text.RegularExpressions;

namespace Day12;

public static class Alt
{
  public static int Part1(IEnumerable<string> lines)
  {
    var arrangements = lines.Select(x =>
      {
        var (springs, damagedSprings) = Parse(x);
        var regex = FindRegex(damagedSprings);
        var arrangements = regex.Match(springs).Success ? FindArrangements(springs.ToCharArray(), regex) : 0;
        return arrangements;
      }).ToList();
    return arrangements.Sum();
  }

  static (string Springs, IEnumerable<int> DamagedSprings) Parse(string line)
  {
    var splits = line.Split(' ');
    if (splits.Length != 2) throw new ArgumentException($"Unexpected format of '{line}'", nameof(line));

    return (splits[0],
      splits[1].Split(',').Select(int.Parse));
  }

  static Regex FindRegex(IEnumerable<int> damagedSprings)
  {
    var sb = new StringBuilder("^[.?]*");
    sb.AppendJoin("[.?]+", damagedSprings.Select(x => $"[#?]{{{x}}}"));
    sb.Append("[.?]*$");
    return new Regex(sb.ToString());
  }

  static int FindArrangements(char[] springs, Regex regex)
  {
    var index = Array.IndexOf(springs, '?');
    if (index == -1) return 1;

    springs[index] = '#';
    var firstArrangements = regex.Match(new string(springs)).Success ? FindArrangements(springs, regex) : 0;
    springs[index] = '.';
    var secondArrangements = regex.Match(new string(springs)).Success ? FindArrangements(springs, regex) : 0;
    springs[index] = '?';
    return firstArrangements + secondArrangements;
  }

  public static int Part2(IEnumerable<string> lines)
  {
    var arrangements = lines.Select(x =>
      {
        var (springs, damagedSprings) = Parse(x);
        (springs, damagedSprings) = Expand(springs, damagedSprings);
        var regex = FindRegex(damagedSprings);
        var arrangements = regex.Match(springs).Success ? FindArrangements(springs.ToCharArray(), regex) : 0;
        return arrangements;
      }).ToList();
    return arrangements.Sum();
  }

  static (string Springs, IEnumerable<int> DamagedSprings) Expand(
    string springs,
    IEnumerable<int> damagedSprings)
  {
    return (Repeat5AppendUnknown(springs), Repeat5(damagedSprings));

    string Repeat5AppendUnknown(string str)
    {
      var sb = new StringBuilder(5 * str.Length + 4);
      sb.AppendJoin('?', Enumerable.Repeat(str, 5));
      return sb.ToString();
    }

    IEnumerable<T> Repeat5<T>(IEnumerable<T> enumerable) => Enumerable.Repeat(enumerable, 5).SelectMany(x => x);
  }
}
