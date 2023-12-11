namespace Day5;

public class Map
{
  readonly SortedList<long, RangeMap> ranges = new ();

  public void Add(RangeMap rangeMap) => ranges.Add(rangeMap.SourceStart, rangeMap);

  public IEnumerable<Range> Convert(Range source)
  {
    // Default Range will work just fine here - as it will convert to same value
    var startMap = ranges.Values.FirstOrDefault(x => x.InRange(source.Start) || x.SourceStart > source.Start);
    if (startMap is null || source.End < startMap.SourceStart)
    {
      yield return source;
      yield break;
    }

    if (!startMap.InRange(source.Start))
    {
      var withoutMap = startMap.SourceStart - source.Start;
      yield return source with { Length = withoutMap };
      source = new Range(source.Start + withoutMap, source.Length - withoutMap);
    }

    var allConverted = startMap.TryConvert(source, out var destination);

    yield return destination;
    if (allConverted) yield break;

    foreach (var splitRange in Convert(
      new Range(source.Start + destination.Length, source.Length - destination.Length)))
    {
      yield return splitRange;
    }
  }
}
