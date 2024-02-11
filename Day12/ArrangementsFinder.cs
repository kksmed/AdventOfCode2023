namespace Day12;

public static class ArrangementsFinder
{
  static readonly Dictionary<(string, int), long> cache = new();

  public static long FindArrangementsWithCache(string springs, Stack<int> expectedDamagedSprings)
  {
    var trimmed = springs.TrimStart('.');

    var hashCode = new HashCode();
    foreach (var n in expectedDamagedSprings)
      hashCode.Add(n);
    var cacheKey = (trimmed, hashCode.ToHashCode());

    if (cache.TryGetValue(cacheKey, out var cached)) return cached;

    var firstNotDamaged = trimmed
      .Select((x, i) => (Char: x, I: i))
      .OfType<(char Char, int I)?>()
      .FirstOrDefault(x => x.HasValue && x.Value.Char != '#');

    var arrangements = 0L;
    if (!firstNotDamaged.HasValue)
    {
      if (expectedDamagedSprings.Count == 0 && trimmed.Length == 0
        || expectedDamagedSprings.Count == 1 && trimmed.Length == expectedDamagedSprings.Single())
        arrangements = 1;
    }
    else
    {
      switch (firstNotDamaged.Value.Char)
      {
        case '.':
          var currentGroup = firstNotDamaged.Value.I;
          if (expectedDamagedSprings.Count > 0 && currentGroup == expectedDamagedSprings.First())
          {
            expectedDamagedSprings.Pop();
            arrangements = FindArrangementsWithCache(trimmed[currentGroup..], expectedDamagedSprings);
            expectedDamagedSprings.Push(currentGroup);
          }

          break;
        case '?':
          var damaged = firstNotDamaged.Value.I;
          if (damaged > 0 && (expectedDamagedSprings.Count == 0 || damaged > expectedDamagedSprings.First()))
            break;

          var preUnknown = damaged > 0 ? trimmed[..damaged] : "";
          var ifDamaged = FindArrangementsWithCache(
            preUnknown + "#" + trimmed[(damaged + 1)..],
            expectedDamagedSprings);
          var ifOperational = FindArrangementsWithCache(
            preUnknown + "." + trimmed[(damaged + 1)..],
            expectedDamagedSprings);
          arrangements = ifDamaged + ifOperational;
          break;
        default: throw new ArgumentOutOfRangeException($"Unexpected character: '{firstNotDamaged.Value.Char}'");
      }
    }

    cache[cacheKey] = arrangements;
    return arrangements;
  }
}
