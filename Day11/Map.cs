namespace Day11;

public class Map
{
  readonly string[] lines;
  readonly long expansion;
  readonly List<int> expandedColumns = new();
  readonly List<int> expandedRows = new();

  public Map(string[] lines, long expansion)
  {
    this.lines = lines;
    this.expansion = expansion-1;
  }

  public void Expand()
  {
    expandedColumns.AddRange(Enumerable.Range(0, lines[0].Length));
    for (var y = 0; y < lines.Length; y++)
    {
      var line = lines[y];
      var space = line.Select((x, i) => (Char: x, X: i)).Where(x => x.Char == '.').Select(x => x.X).ToList();
      if (space.Count == line.Length) expandedRows.Add(y);
      expandedColumns.RemoveAll(x => !space.Contains(x));
    }
  }

  public IEnumerable<Position> FindGalaxies()
  {
    for (var y = 0; y < lines.Length; y++)
    {
      var line = lines[y];
      var x = -1;
      while (true)
      {
        x = line.IndexOf('#', x + 1);
        if (x < 0) break;
        yield return new Position(x, y);
      }
    }
  }

  public long Distance(Position g1, Position g2)
  {
    var minX = Math.Min(g1.X, g2.X);
    var maxX = Math.Max(g1.X, g2.X);
    var minY = Math.Min(g1.Y, g2.Y);
    var maxY = Math.Max(g1.Y, g2.Y);
    return maxX - minX + maxY - minY
      + (expandedRows.Count(y => minY < y && y < maxY) + expandedColumns.Count(x => minX < x && x < maxX)) * expansion;
  }
}

public record Position(int X, int Y);
