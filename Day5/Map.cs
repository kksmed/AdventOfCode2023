namespace Day5;

public class Map
{
  readonly SortedList<long, Range> ranges = new ();

  public void Add(Range range) => ranges.Add(range.SourceStart, range);

  public long Convert(long source) =>
    // Default Range will work just fine here - as it will convert to same value
    ranges.FirstOrDefault(x => x.Value.InRange(source), new KeyValuePair<long, Range>(-1, Range.Default)).Value
      .Convert(source);
}
