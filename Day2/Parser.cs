using System.Text.RegularExpressions;

namespace Day2;

public static partial class Parser
{
  static readonly Regex redRegex = RedRegex();
  static readonly Regex greenRegex = GreenRegex();
  static readonly Regex blueRegex = BlueRegex();

  static Grab ParseGrab(string str)
  {
    var redMatch = redRegex.Match(str);
    var red = redMatch.Success ? int.Parse(redMatch.Groups[1].ToString()) : 0;

    var greenMatch = greenRegex.Match(str);
    var green = greenMatch.Success ? int.Parse(greenMatch.Groups[1].ToString()) : 0;

    var blueMatch = blueRegex.Match(str);
    var blue = blueMatch.Success ? int.Parse(blueMatch.Groups[1].ToString()) : 0;

    return new Grab(red, green, blue);
  }

  public static Game ParseGame(string str)
  {
    var colonSplits = str.Split(':');
    return new Game(int.Parse(colonSplits[0][5..]), colonSplits[1].Split(';').Select(ParseGrab).ToList());
  }

    [GeneratedRegex("(\\d+) red")]
    private static partial Regex RedRegex();

    [GeneratedRegex("(\\d+) blue")]
    private static partial Regex BlueRegex();

    [GeneratedRegex("(\\d+) green")]
    private static partial Regex GreenRegex();
}
