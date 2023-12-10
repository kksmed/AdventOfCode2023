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
        plantField.Seeds.AddRange(line.Split(" ").Skip(1).Select(long.Parse));
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

      currentMap.Add(new Range(numbers[0], numbers[1], (int)numbers[2]));
    }

    return plantField;
  }
}