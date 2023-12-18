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

var (testDistance, testP, testMap) = FindShortestPath(Parser.ParseMap(test.Split(Environment.NewLine)));
Console.WriteLine($"Test: {testDistance}");
PrintPath(testP, testMap);

var (part1Distance, part1P, part1Map) = FindShortestPath(Parser.ParseMap(File.ReadAllLines("input8.txt")));
Console.WriteLine($"Test: {part1Distance}");
PrintPath(part1P, part1Map);

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

(int, Dictionary<string, string> previous, Dictionary<string, Node> map) FindShortestPath(
  (string Directions, IEnumerable<Node> Nodes) data)
{
  var map = CreateMap(data.Nodes);
  var distances = map.Keys.ToDictionary(x => x, _ => int.MaxValue);
  distances[start] = 0;
  var previous = map.Keys.ToDictionary(x => x, _ => (string?)null);
  var visited = map.Keys.ToDictionary(x => x, _ => false);

  var queue = new Queue<string>();
  queue.Enqueue(start);
  while (queue.Count > 0)
  {
    var current = map[queue.Dequeue()];
    visited[current.Id] = true;

    VisitTurn(queue, previous, visited, distances, current.Id, current.Left);
    VisitTurn(queue, previous, visited, distances, current.Id, current.Right);
  }

  return (distances[end], previous, map)!;
}

void VisitTurn(
  Queue<string> queue,
  Dictionary<string, string?> previous,
  Dictionary<string, bool> visited,
  Dictionary<string, int> distances,
  string current,
  string next)
{
  if (visited[next] || queue.Contains(next)) return;

  queue.Enqueue(next);
  var d = distances[current] + 1;
  if (distances[next] <= d) return;

  distances[next] = d;
  previous[next] = current;
}
