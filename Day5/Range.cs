namespace Day5;

public record Range(long DestinationStart, long SourceStart, int Length)
{
  public bool InRange(long source) => SourceStart <= source && source <= SourceStart + Length - 1;

  public long Convert(long source) => source + DestinationStart - SourceStart;

  public static readonly Range Default = new(-1, -1, 0);
}
