using System.Text.RegularExpressions;

namespace Day6;

public static partial class Parser
{
  static readonly Regex regex = IntsRegex();

  public static RaceLong ParseRace(string[] lines)
  {
    if (lines.Length != 2) throw new ArgumentException("Expecting 2 lines!");

    var time = ParseInt(lines[0]);
    var distance = ParseInt(lines[1]);

    return new RaceLong(time, distance);
  }

  public static IEnumerable<Race> ParseRaces(string[] lines)
  {
    if (lines.Length != 2) throw new ArgumentException("Expecting 2 lines!");

    var times = ParseInts(lines[0]);
    var distances = ParseInts(lines[1]);

    return times.Zip(distances, (t, d) => new Race(t, d));
  }

  static IEnumerable<int> ParseInts(string str)
  {
    var match = regex.Match(str);
    if (!match.Success) throw new ArgumentException($"Unexpected format: {str}");

    return match.Groups[2].Captures.Select(x => int.Parse(x.Value));
  }

  static long ParseInt(string str)
  {
    var match = regex.Match(str);
    if (!match.Success) throw new ArgumentException($"Unexpected format: {str}");

    return long.Parse(string.Join(null, match.Groups[2].Captures.Select(x => x.Value)));
  }

    [GeneratedRegex(@"^\w+:( +(\d+))+")]
    private static partial Regex IntsRegex();
}

public record Race(int Time, int Distance);

public record RaceLong(long Time, long Distance);