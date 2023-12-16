using Day7;

var testInput = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483".Split(Environment.NewLine);

var test2 = Part2(testInput);
Console.WriteLine($"Test2: {test2}");

var part2 = Part2(File.ReadAllLines("input7.txt"));
Console.WriteLine($"Part1: {part2}");
return;

int Part2(IEnumerable<string> strings) =>
  strings.Select(Parser.ParseBet)
    .OrderBy(x => x.Hand)
    .Select((x, n) => (n + 1) * x.Amount).Sum();

