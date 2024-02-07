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


return;

int Part1(string[] lines)
{
  var map = new Map(lines);
  map.Expand();

  var galaxies = map.FindGalaxies().ToList();
  // Console.WriteLine(string.Join(Environment.NewLine, galaxies));

  return AllPairs(galaxies)
    .Select(x => Distance(x.Item1, x.Item2))
    .Sum();
}

IEnumerable<(T, T)> AllPairs<T>(ICollection<T> elements) =>
  elements.SelectMany((_, i) => elements.Skip(i + 1), (x, y) => (x, y));

int Distance(Position g1, Position g2) =>
  Math.Abs(g1.X - g2.X) + Math.Abs(g1.Y - g2.Y); 