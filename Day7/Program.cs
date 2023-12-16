using Day7;

var testInput = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483".Split(Environment.NewLine);

var test1 = Part1(testInput);
Console.WriteLine($"Test1: {test1}");

var part1 = Part1(File.ReadAllLines("input7.txt"));
Console.WriteLine($"Part1: {part1}");
return;

int Part1(IEnumerable<string> strings) =>
  strings.Select(Parser.ParseBet)
    .OrderBy(x => x.Hand)
    .Select((x, n) => (n + 1) * x.Amount).Sum();

