var testInput1 = """
                 .....
                 .S-7.
                 .|.|.
                 .L-J.
                 .....
                 """;
var test1 = Part1(testInput1.Split(Environment.NewLine));
Console.WriteLine($"Test1: {test1}");

var testInput2 = """
                 ..F7.
                 .FJ|.
                 SJ.L7
                 |F--J
                 LJ...
                 """;
var test2 = Part1(testInput2.Split(Environment.NewLine));
Console.WriteLine($"Test2: {test2}");

var part1 = Part1(File.ReadAllLines("input10.txt"));
Console.WriteLine($"Part1: {part1}");


return;

int Part1(string[] lines) => new Map(lines).GetFarthestDistance();