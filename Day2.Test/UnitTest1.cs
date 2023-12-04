namespace Day2.Test;

public class Test
{
  [Test]
  public void TestExample()
  {
    var testInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

    Grab max = new(12, 13, 14);
    var possibleGames = testInput.Split(Environment.NewLine).Select(Parser.ParseGame).Where(x => x.IsPossible(max))
      .ToList();

    var sum = possibleGames.Sum(x => x.GameNo);
    Assert.Multiple(
      () =>
        {
          Assert.That(possibleGames.Select(x => x.GameNo), Is.EquivalentTo(new[] { 1, 2, 5 }));

          Assert.That(sum, Is.EqualTo(8));
        });
  }
}
