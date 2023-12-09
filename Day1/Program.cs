using Day1;

var sum = File.ReadLines("input1.txt").Select(DigitsExtractor.GetCalibrationValue).Sum();

Console.WriteLine($"Sum: {sum}");

