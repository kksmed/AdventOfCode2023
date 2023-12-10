using Day5;

var test = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

var testConversions = Parser.ParseAlmanac(test, Environment.NewLine).Convert();
Console.WriteLine("Test:");
Console.WriteLine(testConversions.Single(x => x.Location == 46));

var minLocation = Parser.ParseAlmanac(File.ReadAllText("input5.txt"), "\n").Convert().MinBy(x => x.Location);
Console.WriteLine($"Lowest location number: {minLocation.Location}");