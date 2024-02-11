namespace Day12;

public static class Parser
{
  public static (string Springs, IEnumerable<int> DamagedSprings) Parse(string line)
  {
    var splits = line.Split(' ');
    if (splits.Length != 2) throw new ArgumentException($"Unexpected format of '{line}'", nameof(line));

    return (splits[0],
      splits[1].Split(',').Select(int.Parse));
  }
}
