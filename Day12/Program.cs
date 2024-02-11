using System.Diagnostics;
using System.Text;

using Day12;

const string testInput = """
                         ???.### 1,1,3
                         .??..??...?##. 1,1,3
                         ?#?#?#?#?#?#?#? 1,3,1,6
                         ????.#...#... 4,1,1
                         ????.######..#####. 1,6,5
                         ?###???????? 3,2,1
                         """;
var sw = Stopwatch.StartNew();
var test1 = Part1(testInput.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test1)}: {test1} in ({sw.Elapsed})");

sw = Stopwatch.StartNew();
var part1 = Part1(File.ReadAllLines("input12.txt"));
Console.WriteLine($"{nameof(part1)}: {part1} in ({sw.Elapsed})");

sw = Stopwatch.StartNew();
var test2 = Part2(testInput.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test2)}: {test2} in ({sw.Elapsed})");

sw = Stopwatch.StartNew();
var part2 = Part2(File.ReadAllLines("input12.txt"));
Console.WriteLine($"{nameof(part2)}: {part2} in ({sw.Elapsed})");

return;

static long Part1(IEnumerable<string> lines)
{
  var arrangements = lines.Select(x =>
    {
      var (springs, damagedSprings) = Parser.Parse(x);
      return ArrangementsFinder.FindArrangementsWithCache(
        springs.TrimEnd('.'),
        new Stack<int>(damagedSprings.Reverse()));
    });
  return arrangements.Sum();
}

static long Part2(IEnumerable<string> lines)
{
  var arrangements = lines.Select(
    x =>
      {
        var (springs, damagedSprings) = Parser.Parse(x);
        (springs, damagedSprings) = Expand(springs, damagedSprings);
        return ArrangementsFinder.FindArrangementsWithCache(
          springs.TrimEnd('.'),
          new Stack<int>(damagedSprings.Reverse()));
      });
  return arrangements.Sum();
}

static (string Springs, List<int> DamagedSprings) Expand(
  string springs,
  IEnumerable<int> damagedSprings)
{
  return (Repeat5JoinedByUnknown(springs), Repeat5(damagedSprings).ToList());

  string Repeat5JoinedByUnknown(string str)
  {
    var sb = new StringBuilder(5 * str.Length + 4);
    sb.AppendJoin('?', Enumerable.Repeat(str, 5));
    return sb.ToString();
  }

  IEnumerable<T> Repeat5<T>(IEnumerable<T> enumerable) => Enumerable.Repeat(enumerable, 5).SelectMany(x => x);
}
