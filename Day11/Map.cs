namespace Day11;

public class Map
{
  string[] lines;

  public Map(string[] lines)
  {
    this.lines = lines;
  }

  public void Expand()
  {
    List<int> columnsToExpand = new(Enumerable.Range(0, lines[0].Length));
    List<int> rowsToExpand = new();
    for (var y = 0; y < lines.Length; y++)
    {
      var line = lines[y];
      var space = line.Select((x, i) => (Char: x, X: i)).Where(x => x.Char == '.').Select(x => x.X).ToList();
      if (space.Count == line.Length) rowsToExpand.Add(y);
      columnsToExpand = columnsToExpand.Intersect(space).ToList();
    }

    var newLines = lines.Select(x => x.ToList()).ToList();
    foreach (var rowToAdd in rowsToExpand.OrderDescending())
    {
      newLines.Insert(rowToAdd, newLines[rowToAdd]);
    }

    columnsToExpand = columnsToExpand.OrderDescending().ToList();
    foreach (var row in newLines)
    foreach (var columnToAdd in columnsToExpand)
      row.Insert(columnToAdd, '.');

    lines = newLines.Select(x => new string(x.ToArray())).ToArray();
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
}

public record Position(int X, int Y);
