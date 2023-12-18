using Day8;

const string test = @"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)";

const string start = "AAA";
const string end = "ZZZ";

var testDistance = FindDistance(Parser.ParseMap(test.Split(Environment.NewLine)));
Console.WriteLine($"Test: {testDistance}");

var part1Distance = FindDistance(Parser.ParseMap(File.ReadAllLines("input8.txt")));
Console.WriteLine($"Part 1: {part1Distance}");

return;

void PrintPath(Dictionary<string, string> prev, Dictionary<string, Node> dictionary)
{
  var directions = new Stack<char>();
  var current = end;
  while (current != start)
  {
    Console.Write($"{current} <- ");
    var to = current;
    current = prev[current];
    var node = dictionary[current];
    directions.Push(node.Left == to ? 'L' : 'R');
  }
  Console.WriteLine($"{start}.");
  Console.WriteLine(directions.ToArray());
}

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


