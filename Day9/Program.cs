var testInput = """
                0 3 6 9 12 15
                1 3 6 10 15 21
                10 13 16 21 30 45
                """;

var test1 = Part1Sum(testInput.Split(Environment.NewLine));
Console.WriteLine($"Test 1: {test1}");

var part1 = Part1Sum(File.ReadAllLines("input9.txt"));
Console.WriteLine($"Part 1: {part1}");

return;

int Part1Sum(IEnumerable<string> lines)
{
  return lines
    .Select(Parse)
    .Select(PredictNext)
    .Sum();

  List<int> Parse(string line) => line.Split(' ').Select(int.Parse).ToList();

  int PredictNext(ICollection<int> numbers)
  {
    var diffs = numbers.SkipLast(1).Zip(numbers.Skip(1)).Select(x => x.Second - x.First).ToList();
    if (diffs.All(x => x == 0)) return numbers.Last();

    return numbers.Last() + PredictNext(diffs);
  }
}