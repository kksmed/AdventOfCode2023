namespace Day1.Test;

class DigitsExtractorTest
{
  [TestCase("1abc2", 12),
   TestCase("pqr3stu8vwx", 38),
   TestCase("a1b2c3d4e5f", 15),
   TestCase("treb7uchet)]", 77),
   TestCase("two1nine)]", 29),
   TestCase("eightwothree)]", 83),
   TestCase("abcone2threexyz)]", 13),
   TestCase("xtwone3four)]", 24),
   TestCase("4nineeightseven2)]", 42),
   TestCase("zoneight234)]", 14),
   TestCase("7pqrstsixteen)]", 76),
  ]
  public void GetCalibrationValue(string str, int expected)
  {
    var calibrationValue = DigitsExtractor.GetCalibrationValue(str);
    Assert.That(calibrationValue, Is.EqualTo(expected));
  }
}