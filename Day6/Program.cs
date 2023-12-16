using Day6;

var testInput = """
           Time:      7  15   30
           Distance:  9  40  200
           """.Split(Environment.NewLine);

var testProduct = Part1(testInput);

Console.WriteLine($"Test: {testProduct}");

var inputLines = File.ReadAllLines("input6.txt");
var part1 = Part1(inputLines);
Console.WriteLine($"Part1: {part1}");

var test2 = Part2(testInput);
Console.WriteLine($"Test2: {test2}");

var part2 = Part2(inputLines);
Console.WriteLine($"Part2: {part2}");
return;

long Part2(string[] lines)
{
  var race = Parser.ParseRace(lines);
  // = (Time +/- sqrt(Time*Time - 4*Distance))/2
  var discriminant = race.Time * race.Time - 4 * race.Distance;
  var discriminantSqrt = Math.Sqrt(discriminant);

  var solution1 = (race.Time - discriminantSqrt) / 2.0;
  var minWin = (long)Math.Floor(solution1) + 1;
  var solution2 = (race.Time + discriminantSqrt) / 2.0;
  var maxWin = (long)Math.Ceiling(solution2) - 1;

  return maxWin - minWin + 1;
}

int Part1(string[] lines) => Parser.ParseRaces(lines).Select(AmountOfWaysToWin).Aggregate(1, (x,y) => x * y);

int AmountOfWaysToWin(Race race)
{
  var amounts = 0;
  for (var i = 1; i < race.Time - 1; i++)
  {
    var distance = i * (race.Time - i);
    if (distance > race.Distance)
      amounts++;
  }

  return amounts;
}