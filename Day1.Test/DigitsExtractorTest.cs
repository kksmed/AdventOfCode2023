namespace Day1.Test;

class DigitsExtractorTest
{

  [TestCase("1abc2", 1,2),
   TestCase("pqr3stu8vwx", 3, 8),
   TestCase("a1b2c3d4e5f", 1, 5),
  TestCase("treb7uchet)]", 7, 7)]
  public void GetDigits(string str, int expectedFirst, int expectedLast)
    {
        var (first, last) = DigitsExtractor.GetDigits(str);
        Assert.Multiple(() =>
        {
            Assert.That(first, Is.EqualTo(expectedFirst));
            Assert.That(last, Is.EqualTo(expectedLast));
        });
    }
}