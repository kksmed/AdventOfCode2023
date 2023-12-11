using System.Diagnostics.CodeAnalysis;

namespace Day5;

public record RangeMap(long DestinationStart, long SourceStart, long Length) : Range(SourceStart, Length)
{
  public bool InRange(long source) => Start <= source && source <= End;

  public bool TryConvert(Range source, out Range destination)
  {

    if (!InRange(source.Start))
    {
      destination = source;
      return true;
    }

    destination = source with { Start = Convert(source.Start) };
    if (InRange(source.End)) return true;

    destination = destination with { Length = End - source.Start + 1 };
    return false;
  }

  public long Convert(long source) => source + DestinationStart - SourceStart;

  public static readonly RangeMap Default = new(-1, -1, 0);
}
