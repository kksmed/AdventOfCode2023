using System.Diagnostics;
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

  static (string Springs, List<int> DamagedSprings) Parse(string line)
  {
    var splits = line.Split(' ');
    if (splits.Length != 2) throw new ArgumentException($"Unexpected format of '{line}'", nameof(line));

    return (splits[0],
      splits[1].Split(',').Select(int.Parse).ToList());
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
        var sw = Stopwatch.StartNew();
        var (springs, damagedSprings) = Parse(x);
        Console.WriteLine($"Parsed: {springs} - ({string.Join(",", damagedSprings)}) in {sw.Elapsed}");
        (springs, damagedSprings) = Expand(springs, damagedSprings);
        Console.WriteLine($"Expanded in {sw.Elapsed}");
        var regex = FindRegex(damagedSprings);
        Console.WriteLine($"Build regex in {sw.Elapsed}");
        var arrangements = regex.Match(springs).Success ? FindArrangements(springs.ToCharArray(), regex) : 0;
        Console.WriteLine($"Arrangements ({arrangements}) in {sw.Elapsed}");
        return arrangements;
      }).ToList();
    return arrangements.Sum();
  }

  static (string Springs, List<int> DamagedSprings) Expand(
    string springs,
    IEnumerable<int> damagedSprings)
  {
    return (Repeat5AppendUnknown(springs), Repeat5(damagedSprings).ToList());

    string Repeat5AppendUnknown(string str)
    {
      var sb = new StringBuilder(5 * str.Length + 4);
      sb.AppendJoin('?', Enumerable.Repeat(str, 5));
      return sb.ToString();
    }

    IEnumerable<T> Repeat5<T>(IEnumerable<T> enumerable) => Enumerable.Repeat(enumerable, 5).SelectMany(x => x);
  }

  static readonly Dictionary<(string, int), long> cache = new();

  public static long Part1WithCache(IEnumerable<string> lines)
  {
    var arrangements = lines.Select(x =>
      {
        var sw = Stopwatch.StartNew();
        var (springs, damagedSprings) = Parse(x);
        // Console.WriteLine($"Parsed: {springs} - ({string.Join(",", damagedSprings)}) in {sw.Elapsed}");
        damagedSprings.Reverse();
        var arrangements = FindArrangementsWithCache(springs.TrimEnd('.'), new Stack<int>(damagedSprings));
        // Console.WriteLine($"Arrangements ({arrangements}) in {sw.Elapsed}");
        return arrangements;
      });
    return arrangements.Sum();
  }

  public static long Part2WithCache(IEnumerable<string> lines)
  {
    var arrangements = lines.Select(x =>
      {
        var sw = Stopwatch.StartNew();
        var (springs, damagedSprings) = Parse(x);
        // Console.WriteLine($"Parsed: {springs} - ({string.Join(",", damagedSprings)}) in {sw.Elapsed}");
        (springs, damagedSprings) = Expand(springs, damagedSprings);
        // Console.WriteLine($"Expanded in {sw.Elapsed}");
        damagedSprings.Reverse();
        var arrangements = FindArrangementsWithCache(springs.TrimEnd('.'), new Stack<int>(damagedSprings));
        // Console.WriteLine($"Arrangements ({arrangements}) in {sw.Elapsed}");
        return arrangements;
      });
    return arrangements.Sum();
  }

  static long FindArrangementsWithCache(string springs, Stack<int> expectedDamagedSprings)
  {
    // Console.WriteLine($"Testing '{springs}' ({string.Join(",", expectedDamagedSprings)})");
    var trimmed = springs.TrimStart('.');
    // if (trimmed.Length != springs.Length) Console.WriteLine($"Trimmed to: '{trimmed}'");

    var hashCode = new HashCode();
    foreach (var n in expectedDamagedSprings)
      hashCode.Add(n);
    var cacheKey = (trimmed, hashCode.ToHashCode());

    if (cache.TryGetValue(cacheKey, out var cached))
    {
      // Console.WriteLine($"Cache hit!");
      return cached;
    }

    var firstNotDamaged = trimmed
      .Select((x, i) => (Char: x, I: i))
      .OfType<(char Char, int I)?>()
      .FirstOrDefault(x => x.HasValue && x.Value.Char != '#');

    var arrangements = 0L;
    if (!firstNotDamaged.HasValue)
    {
      if (expectedDamagedSprings.Count == 0 && trimmed.Length == 0
      || expectedDamagedSprings.Count == 1 && trimmed.Length == expectedDamagedSprings.Single())
        arrangements = 1;
    }
    else
    {
      switch (firstNotDamaged.Value.Char)
      {
        case '.':
          var currentGroup = firstNotDamaged.Value.I;
          if (expectedDamagedSprings.Count > 0 && currentGroup == expectedDamagedSprings.First())
          {
            expectedDamagedSprings.Pop();
            arrangements = FindArrangementsWithCache(trimmed[currentGroup..], expectedDamagedSprings);
            expectedDamagedSprings.Push(currentGroup);
          }
          break;
          case '?':
            var damaged = firstNotDamaged.Value.I;
            if (damaged > 0 && (expectedDamagedSprings.Count == 0 || damaged > expectedDamagedSprings.First()))
            {
              // Console.WriteLine("Impossible!");
              break;
            }

            var preUnknown = damaged > 0 ? trimmed[..damaged] : "";
            var ifDamaged = FindArrangementsWithCache(
              preUnknown + "#" + trimmed[(damaged + 1)..],
              expectedDamagedSprings);
            var ifOperational = FindArrangementsWithCache(
              preUnknown + "." + trimmed[(damaged + 1)..],
              expectedDamagedSprings);
            arrangements = ifDamaged + ifOperational;
            break;
          default: throw new ArgumentOutOfRangeException($"Unexpected character: '{firstNotDamaged.Value.Char}'");
      }
    }

    // Console.WriteLine($"Arrangements for '{trimmed}' ({string.Join(",", expectedDamagedSprings)}) = {arrangements}");

    cache[cacheKey] = arrangements;
    return arrangements;
  }
}
