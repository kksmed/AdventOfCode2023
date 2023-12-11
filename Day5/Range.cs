namespace Day5;

public record Range(long Start, long Length)
{
  public long End => Start + Length - 1;
}
