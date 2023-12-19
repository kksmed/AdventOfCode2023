using Day8;

const string test1 = """
                     RL

                     AAA = (BBB, CCC)
                     BBB = (DDD, EEE)
                     CCC = (ZZZ, GGG)
                     DDD = (DDD, DDD)
                     EEE = (EEE, EEE)
                     GGG = (GGG, GGG)
                     ZZZ = (ZZZ, ZZZ)
                     """;

const string start = "AAA";
const string end = "ZZZ";
const char startSuffix = 'A';
const char endSuffix = 'Z';

var testDistance = FindDistance(Parser.ParseMap(test1.Split(Environment.NewLine)), GetStartNode, IsEnd);
Console.WriteLine($"Test: {testDistance}");

var part1Distance = FindDistance(Parser.ParseMap(File.ReadAllLines("input8.txt")), GetStartNode, IsEnd);
Console.WriteLine($"Part 1: {part1Distance}");

const string test2 = """
                     LR

                     11A = (11B, XXX)
                     11B = (XXX, 11Z)
                     11Z = (11B, XXX)
                     22A = (22B, XXX)
                     22B = (22C, 22C)
                     22C = (22Z, 22Z)
                     22Z = (22B, 22B)
                     XXX = (XXX, XXX)
                     """;

var testDistance2 = FindDistance(
  Parser.ParseMap(test2.Split(Environment.NewLine)),
  GetAllStartNodes,
  IsAllAtEnd);
Console.WriteLine($"Test2: {testDistance2}");

var part2Distance = FindDistance(Parser.ParseMap(File.ReadAllLines("input8.txt")), GetAllStartNodes, IsAllAtEnd);
Console.WriteLine($"Part 2: {part2Distance}");

return;

Node[] GetStartNode(IEnumerable<Node> x) => x.Where(n => n.Id == start).ToArray();

bool IsEnd(Node[] x) => x.Single().Id == end;

Node[] GetAllStartNodes(IEnumerable<Node> x) => x.Where(n => n.Id.EndsWith(startSuffix)).ToArray();

bool IsAllAtEnd(Node[] x) => x.All(n => n.Id.EndsWith(endSuffix));

long FindDistance((string Directions, IEnumerable<Node> Nodes) data, Func<IEnumerable<Node>, Node[]> startFun, Func<Node[], bool> endFun)
{
  var map = data.Nodes.ToDictionary(x => x.Id, x => x);
  var currents = startFun(map.Values);
  var starts = currents.ToArray();
  var logs = starts.Select(x => new Log(x)).ToArray();
  var distance = 0L;
  while (!endFun(currents))
  {
    var turn = data.Directions[(int)(distance % data.Directions.Length)];
    for (var i = 0; i < currents.Length; i++)
    {
      var current = currents[i];
      currents[i] = map[turn == 'L' ? current.Left : current.Right];
    }

    distance++;

    foreach (var (node, i) in currents.Select((x, i) => (Node: x, I: i)).Where(x => x.Node.Id.EndsWith(endSuffix)))
    {
      var log = logs[i];
      if (log.Start2End.HasValue)
      {
        log.End2End ??= distance - log.Start2End;
      }
      else
      {
        log.Start2End = distance;
      }

      log.Ends.Add(node);
    }

    if (logs.All(x => x.End2End.HasValue))
    {
      return logs.Aggregate(1L, (total, log) => FindLcm(total, log.Start2End!.Value));
    }
  }

  return distance;
}

static long FindGcf(long a, long b)
{
  while (b != 0)
  {
    var temp = b;
    b = a % b;
    a = temp;
  }

  return a;
}

static long FindLcm(long a, long b)
{
  return a / FindGcf(a, b) * b;
}


record Log(Node Start)
{
  public long? Start2End { get; set; }
  public long? End2End { get; set; }
  public List<Node> Ends { get; } = new();
}