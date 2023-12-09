// See https://aka.ms/new-console-template for more information

using Day3;

var str = File.ReadAllText("input.txt");
var numbers = Parser.ParseToNumbers(str);
var sum = numbers.Sum();
Console.WriteLine($"Sum: {sum}");
