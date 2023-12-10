namespace Day3.Test;

public class Tests
{
  [Test]
  public void Test()
  {
    var str = @"
467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";
    var (numbers, gears) = Parser.ParseToNumbers(str.Split(Environment.NewLine));

    Assert.That(numbers.Sum(), Is.EqualTo(4361));

    var sumOfGearRatios = gears.Sum(x => x.Numbers[0].Value * x.Numbers[1].Value);

    Assert.That(sumOfGearRatios, Is.EqualTo(467835));
  }
}
