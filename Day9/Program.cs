var testInput = """
                0 3 6 9 12 15
                1 3 6 10 15 21
                10 13 16 21 30 45
                """;

var test1 = Part1Sum(testInput.Split(Environment.NewLine));
Console.WriteLine($"Test 1: {test1}");

var part1 = Part1Sum(File.ReadAllLines("input9.txt"));
Console.WriteLine($"Part 1: {part1}");

var test2 = Part2Sum(testInput.Split(Environment.NewLine));
Console.WriteLine($"Test 2: {test2}");

var part2 = Part2Sum(File.ReadAllLines("input9.txt"));
Console.WriteLine($"Part 2: {part2}");

return;

List<int> Parse(string line) => line.Split(' ').Select(int.Parse).ToList();

List<int> FindDiffs(ICollection<int> collection) =>
  collection.SkipLast(1).Zip(collection.Skip(1)).Select(x => x.Second - x.First).ToList();

int Part1Sum(IEnumerable<string> lines)
{
  return lines
    .Select(Parse)
    .Select(PredictNext)
    .Sum();


  int PredictNext(ICollection<int> numbers)
  {
    var diffs = FindDiffs(numbers);
    if (diffs.All(x => x == 0)) return numbers.Last();

    return numbers.Last() + PredictNext(diffs);
  }
}

int Part2Sum(IEnumerable<string> lines)
{
  return lines
    .Select(Parse)
    .Select(PredictPrevious)
    .Sum();


  int PredictPrevious(ICollection<int> numbers)
  {
    var diffs = FindDiffs(numbers);
    if (diffs.All(x => x == 0)) return numbers.First();

    return numbers.First() - PredictPrevious(diffs);
  }
}