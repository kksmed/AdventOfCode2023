
using Day2;

var max = new Grab(12, 13, 14);
var games = File.ReadLines("input.txt").Select(Parser.ParseGame).ToList();
var sum = games.Where(x => x.IsPossible(max)).Sum(x => x.GameNo);

Console.WriteLine($"Sum: {sum}");

var sum2 = games.Select(x => x.Min()).Sum(x => x.Red * x.Green * x.Blue);

Console.WriteLine($"Sum2: {sum2}");
