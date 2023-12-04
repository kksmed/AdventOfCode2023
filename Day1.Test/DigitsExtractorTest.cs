namespace Day1.Test;

class DigitsExtractorTest
{
  [TestCase("1abc2", 12),
   TestCase("pqr3stu8vwx", 38),
   TestCase("a1b2c3d4e5f", 15),
   TestCase("treb7uchet)]", 77)]
  public void GetCalibrationValue(string str, int expected)
  {
    var calibrationValue = DigitsExtractor.GetCalibrationValue(str);
    Assert.That(calibrationValue, Is.EqualTo(expected));
  }
}
