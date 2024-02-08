using System.Diagnostics;

var testInput = """
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

int Part1(IEnumerable<string> lines)
{
  var arrangements = lines.Select(x =>
    {
      var (springs, damagedSprings) = Parse(x);
      var springsEnumerable = springs.ToList();
      // Console.WriteLine($"Springs: {string.Join(",", springsEnumerable)}");
      var damagedEnumerable = damagedSprings.ToList();
      // Console.WriteLine($"Expected damaged: {string.Join(",", damagedEnumerable)}");
      var arrangements = FindArrangements(springsEnumerable, damagedEnumerable);
      // Console.WriteLine($"Arrangements: {arrangements}");
      return arrangements;
    }).ToList();
  return arrangements.Sum();
}

int Part2(string[] lines)
{
  var arrangements = lines.Select(x =>
    {
      var (springs, damagedSprings) = Parse(x);
      (springs, damagedSprings) = Expand(springs, damagedSprings);
      var springsEnumerable = springs.ToList();
      Console.WriteLine($"Springs: {string.Join(",", springsEnumerable)}");
      var damagedEnumerable = damagedSprings.ToList();
      Console.WriteLine($"Expected damaged: {string.Join(",", damagedEnumerable)}");
      var arrangements = FindArrangements(springsEnumerable, damagedEnumerable);
      Console.WriteLine($"Arrangements: {arrangements}");
      return arrangements;
    }).ToList();
  return arrangements.Sum();
}

(IEnumerable<Springs> springs, IEnumerable<int> damagedSprings) Expand(
  IEnumerable<Springs> springs,
  IEnumerable<int> damagedSprings)
{
  return (Repeat5AppendUnknown(springs), Repeat5(damagedSprings));

  IEnumerable<Springs> Repeat5AppendUnknown(IEnumerable<Springs> sps)
  {
    var springsList = sps.ToList();
    return springsList.Append(Springs.Unknown).Concat(springsList).Append(Springs.Unknown).Concat(springsList)
      .Append(Springs.Unknown).Concat(springsList).Append(Springs.Unknown).Concat(springsList);
  }

  IEnumerable<T> Repeat5<T>(IEnumerable<T> enumerable) => Enumerable.Repeat(enumerable, 5).SelectMany(x => x);
}

int FindArrangements(List<Springs> springs, ICollection<int> damagedSprings)
{
  var index = springs.IndexOf(Springs.Unknown);
  if (index == -1) return CheckSprings(springs, damagedSprings) ? 1 : 0;

  if (!CheckSprings(springs, damagedSprings)) return 0;

  springs[index] = Springs.Damaged;
  var firstArrangements = FindArrangements(springs, damagedSprings);
  springs[index] = Springs.Operational;
  var secondArrangements = FindArrangements(springs, damagedSprings);
  springs[index] = Springs.Unknown;
  return firstArrangements + secondArrangements;
}

bool CheckSprings(List<Springs> springs, ICollection<int> damagedSprings)
{
  var damagedIndex = springs.IndexOf(Springs.Damaged);
  if (!damagedSprings.Any()) return damagedIndex == -1;

  var unknownIndex = springs.IndexOf(Springs.Unknown);

  foreach (var expectedDamaged in damagedSprings)
  {
    if (damagedIndex == -1 && unknownIndex == -1) return false;

    var damaged = 1;
    var doContinue = true;

    while (doContinue)
    {
      var nextIndex = springs.IndexOf(Springs.Damaged, damagedIndex + 1);
      if (unknownIndex != -1 && (nextIndex > unknownIndex || nextIndex == -1)) return true;
      if (nextIndex == damagedIndex + 1)
      {
        damaged++;
        if (damaged > expectedDamaged) return false;
      }
      else
      {
        doContinue = false;
      }
      damagedIndex = nextIndex;
    }
    if (expectedDamaged != damaged) return false;
  }

  return damagedIndex == -1;
}

(IEnumerable<Springs> Springs, IEnumerable<int> DamagedSprings) Parse(string line)
{
  var splits = line.Split(' ');
  if (splits.Length != 2) throw new ArgumentException($"Unexpected format of '{line}'", nameof(line));

  return (splits[0].Select(
      x => x switch
        {
          '?' => Springs.Unknown,
          '.' => Springs.Operational,
          '#' => Springs.Damaged,
          _ => throw new ArgumentException($"Unknown spring: '{x}'")
        }),
    splits[1].Split(',').Select(int.Parse));
}

enum Springs
{
  Operational,
  Damaged,
  Unknown
}