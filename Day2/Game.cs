namespace Day2;

public record Game(int GameNo, List<Grab> Grabs)
{
  public bool IsPossible(Grab max) => Grabs.All(x => x.Red <= max.Red && x.Green <= max.Green && x.Blue <= max.Blue);
}