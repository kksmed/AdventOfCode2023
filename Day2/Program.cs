
using Day2;

var max = new Grab(12, 13, 14);
var sum = File.ReadLines("input.txt").Select(Parser.ParseGame).Where(x => x.IsPossible(max)).Sum(x => x.GameNo);

Console.WriteLine($"Sum: {sum}");