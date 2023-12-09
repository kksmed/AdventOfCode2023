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
    var numbers = Parser.ParseToNumbers(str);

    Assert.That(numbers.Sum(), Is.EqualTo(4361));
  }
}
