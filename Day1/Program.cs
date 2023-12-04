using Day1;

var sum = File.ReadLines("input.txt").Select(DigitsExtractor.GetCalibrationValue).Sum();

Console.WriteLine($"Sum: {sum}");

