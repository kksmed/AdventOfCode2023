using Day8;

const string test1 = @"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)";

const string start = "AAA";
const string end = "ZZZ";
const char startSurfix = 'A';
const char endSurfix = 'Z';

var testDistance = FindDistance(Parser.ParseMap(test1.Split(Environment.NewLine)));
Console.WriteLine($"Test: {testDistance}");

var part1Distance = FindDistance(Parser.ParseMap(File.ReadAllLines("input8.txt")));
Console.WriteLine($"Part 1: {part1Distance}");

var test2 = @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";

var testDistance2 = FindGhostDistance(Parser.ParseMap(test2.Split(Environment.NewLine)));
Console.WriteLine($"Test2: {testDistance2}");

var part2Distance = FindGhostDistance(Parser.ParseMap(File.ReadAllLines("input8.txt")));
Console.WriteLine($"Part 2: {part2Distance}");

return;

Dictionary<string, Node> CreateMap(IEnumerable<Node> nodes) => nodes.ToDictionary(x => x.Id, x => x);

int FindDistance((string Directions, IEnumerable<Node> Nodes) data)
{
  var map = CreateMap(data.Nodes);
  var distances = map.Keys.ToDictionary(x => x, _ => int.MaxValue);
  distances[start] = 0;

  var queue = new Queue<char>(data.Directions);
  var current = map[start];
  var distance = 0;
  do
  {
    var turn = queue.Dequeue();
    Console.Write($"{current.Id} -({turn})-> ");
    current = map[turn == 'L' ? current.Left : current.Right];

    distance++;
    queue.Enqueue(turn);
  } while (current.Id != end);
  Console.WriteLine($"-> {current.Id}.");

  return distance;
}

int FindGhostDistance((string Directions, IEnumerable<Node> Nodes) data)
{
  var map = CreateMap(data.Nodes);
  var currents = map.Where(x => x.Key.EndsWith(startSurfix)).Select(x => x.Value).ToArray();
  var distance = 0;
  var t = 0;
  while (!currents.All(x => x.Id.EndsWith(endSurfix)))
  {
    var turn = data.Directions[t];
    for (var i = 0; i < currents.Length; i++)
    {
      var current = currents[i];
      currents[i] = map[turn == 'L' ? current.Left : current.Right];
    }

    distance++;
    t++;
    if (t == data.Directions.Length) t = 0;
  }

  return distance;
}

