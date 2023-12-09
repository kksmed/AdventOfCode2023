// See https://aka.ms/new-console-template for more information

using Day3;

var lines = File.ReadAllLines("input3.txt");
var numbers = Parser.ParseToNumbers(lines);
var sum = numbers.Sum();
Console.WriteLine($"Day 3 part 1 sum = {sum}");
