using System.Text.RegularExpressions;

namespace Day8;

public static partial class Parser
{
  static readonly Regex directionsRegex = DirectionsRegex();
  static readonly Regex nodeRegex = NodeRegex();

  public static (string Directions, IEnumerable<Node> Nodes) ParseMap(string[] lines)
  {
    if (lines.Length < 3) throw new ArgumentException("Too few lines");

    return (ParseDirections(lines[0]), lines.Skip(2).Select(ParseNode));
  }

  static string ParseDirections(string str)
  {
    var match = directionsRegex.Match(str);
    if (!match.Success)
      throw new ArgumentException($"No directions! ({str})", nameof(str));

    return match.Groups[0].Captures.Single().Value;
  }

  static Node ParseNode(string str)
  {
    var match = nodeRegex.Match(str);
    if (!match.Success)
      throw new ArgumentException($"Unknown format: {str}", nameof(str));

    return new Node(
      match.Groups[1].Captures.Single().Value,
      match.Groups[2].Captures.Single().Value,
      match.Groups[3].Captures.Single().Value);
  }

    [GeneratedRegex(@"^(\w+) = \((\w+), (\w+)\)$")]
    private static partial Regex NodeRegex();

    [GeneratedRegex("(L|R)+")]
    private static partial Regex DirectionsRegex();
}

public record Node(string Id, string Left, string Right);