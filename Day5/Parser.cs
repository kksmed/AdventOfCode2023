namespace Day5;

public static class Parser
{
  public static PlantField ParseAlmanac(string almanac, string newLine)
  {
    var plantField = new PlantField();
    Map? currentMap = null;
    var mapNo = 0;
    foreach (var line in almanac.Split(newLine))
    {
      if (plantField.Seeds.Count == 0)
      {
        long? start = null;
        foreach (var n in line.Split(" ").Skip(1))
        {
          if (start is null)
          {
            start = long.Parse(n);
            continue;
          }

          plantField.Seeds.Add(new(start.Value, long.Parse(n)));
          start = null;
        }

        if (start is not null) throw new InvalidOperationException("Mismatch in seed numbers.");
        continue;
      }

      if (line.Length == 0)
      {
        currentMap = null;
        continue;
      }

      if (line.EndsWith(" map:"))
      {
        currentMap = plantField.Maps[mapNo++];
        continue;
      }

      var numbers = line.Split(" ").Select(long.Parse).ToList();
      if (numbers.Count != 3)
        throw new ArgumentException($"Parse error in line - expecting 3 numbers, but was: {line}");

      if (currentMap == null)
        throw new InvalidOperationException($"No current map at: {line}");

      currentMap.Add(new RangeMap(numbers[0], numbers[1], (int)numbers[2]));
    }

    return plantField;
  }
}