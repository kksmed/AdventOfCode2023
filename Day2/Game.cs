namespace Day2;

public record Game(int GameNo, List<Grab> Grabs)
{
  public bool IsPossible(Grab max) => Grabs.All(x => x.Red <= max.Red && x.Green <= max.Green && x.Blue <= max.Blue);

  public Grab Min() => Grabs.Aggregate(
    new Grab(0, 0, 0),
    (min, x) => new Grab(Math.Max(min.Red, x.Red), Math.Max(min.Green, x.Green), Math.Max(min.Blue, x.Blue)));
}