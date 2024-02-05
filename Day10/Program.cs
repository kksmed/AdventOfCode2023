using Day10;

var testInput1 = """
                 .....
                 .S-7.
                 .|.|.
                 .L-J.
                 .....
                 """;
var test1 = Part1(testInput1.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test1)}: {test1}");

var testInput2 = """
                 ..F7.
                 .FJ|.
                 SJ.L7
                 |F--J
                 LJ...
                 """;
var test2 = Part1(testInput2.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test2)}: {test2}");

var part1 = Part1(File.ReadAllLines("input10.txt"));
Console.WriteLine($"{nameof(part1)}: {part1}");

var testInput3 = """
             ...........
             .S-------7.
             .|F-----7|.
             .||.....||.
             .||.....||.
             .|L-7.F-J|.
             .|..|.|..|.
             .L--J.L--J.
             ...........
             """;

var test3 = Part2(testInput3.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test3)}: {test3}");

var testInput3B = """
           ..........
           .S------7.
           .|F----7|.
           .||OOOO||.
           .||OOOO||.
           .|L-7F-J|.
           .|II||II|.
           .L--JL--J.
           ..........
           """;

var test3B = Part2(testInput3B.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test3B)}: {test3B}");

var testInput4 = """
                 .F----7F7F7F7F-7....
                 .|F--7||||||||FJ....
                 .||.FJ||||||||L7....
                 FJL7L7LJLJ||LJ.L-7..
                 L--J.L7...LJS7F-7L7.
                 ....F-J..F7FJ|L7L7L7
                 ....L7.F7||L7|.L7L7|
                 .....|FJLJ|FJ|F7|.LJ
                 ....FJL-7.||.||||...
                 ....L---J.LJ.LJLJ...
                 """;

var test4 = Part2(testInput4.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test4)}: {test4}");

var testInput5 = """
                 FF7FSF7F7F7F7F7F---7
                 L|LJ||||||||||||F--J
                 FL-7LJLJ||||||LJL-77
                 F--JF--7||LJLJ7F7FJ-
                 L---JF-JLJ.||-FJLJJ7
                 |F|F-JF---7F7-L7L|7|
                 |FFJF7L7F-JF7|JL---7
                 7-L-JL7||F7|L7F-7F7|
                 L.L7LFJ|||||FJL7||LJ
                 L7JLJL-JLJLJL--JLJ.L
                 """;

var test5 = Part2(testInput5.Split(Environment.NewLine));
Console.WriteLine($"{nameof(test5)}: {test5}");

var part2 = Part2(File.ReadAllLines("input10.txt"));
Console.WriteLine($"Part4: {part2}");

return;

int Part1(string[] lines) => new Map(lines).GetFarthestDistance();

int Part2(string[] lines) => new Map(lines).GetEnclosedTiles();