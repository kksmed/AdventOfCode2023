using Day11;

var testInput = """
                ...#......
                .......#..
                #.........
                ..........
                ......#...
                .#........
                .........#
                ..........
                .......#..
                #...#.....
                """;

var test1 = Part1(testInput.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test1)}: {test1}");

var part1 = Part1(File.ReadAllLines("input11.txt"));
Console.WriteLine($"{nameof(part1)}: {part1}");

var test10 = Part2(testInput.Split(Environment.NewLine), 10);
Console.WriteLine($"{nameof(test10)}: {test10}");

var test100 = Part2(testInput.Split(Environment.NewLine), 100);
Console.WriteLine($"{nameof(test100)}: {test100}");

var part2 = Part2(File.ReadAllLines("input11.txt"), 1000000);
Console.WriteLine($"{nameof(part2)}: {part2}");

return;

long Part1(string[] lines)
{
  var map = new Map(lines, 2);
  map.Expand();

  var galaxies = map.FindGalaxies().ToList();

  return AllPairs(galaxies)
    .Select(x => map.Distance(x.Item1, x.Item2))
    .Sum();
}

long Part2(string[] lines, long expansion)
{
  var map = new Map(lines, expansion);
  map.Expand();

  var galaxies = map.FindGalaxies().ToList();

  return AllPairs(galaxies)
    .Select(x => map.Distance(x.Item1, x.Item2))
    .Sum();
}

IEnumerable<(T, T)> AllPairs<T>(ICollection<T> elements) =>
  elements.SelectMany((_, i) => elements.Skip(i + 1), (x, y) => (x, y));

