using System.Diagnostics.CodeAnalysis;

class Map
{
  readonly string[] lines;
  readonly int maxY;
  readonly int maxX;

  public Map(string[] lines)
  {
    this.lines = lines;
    maxY = lines.Length - 1;
    maxX = lines[0].Length - 1;
  }

  public int GetFarthestDistance()
  {
    var s = FindStart();
    var (end1, end2) = FindConnections(s);
    var distance = 1;
    while (true)
    {
      end1 = GoNext(end1);
      if (end1.Position == end2.Position) return distance;
      distance++;
      end2 = GoNext(end2);
      if (end1.Position == end2.Position) return distance;
    }
  }

  Pipe GoNext(Pipe pipe) =>
    pipe.Direction switch
      {
        Direction.Up => pipe.Form switch
          {
            '|' => Go(pipe, Direction.Up),
            '7' => Go(pipe, Direction.Left),
            'F' => Go(pipe, Direction.Right),
            _ => throw new InvalidOperationException($"Invalid pipe: {pipe}")
          },
        Direction.Down => pipe.Form switch
          {
            '|' => Go(pipe, Direction.Down),
            'J' => Go(pipe, Direction.Left),
            'L' => Go(pipe, Direction.Right),
            _ => throw new InvalidOperationException($"Invalid pipe: {pipe}")
          },
        Direction.Left => pipe.Form switch
          {
            '-' => Go(pipe, Direction.Left),
            'L' => Go(pipe, Direction.Up),
            'F' => Go(pipe, Direction.Down),
            _ => throw new InvalidOperationException($"Invalid pipe: {pipe}")
          },
        Direction.Right => pipe.Form switch
          {
            '-' => Go(pipe, Direction.Right),
            'J' => Go(pipe, Direction.Up),
            '7' => Go(pipe, Direction.Down),
            _ => throw new InvalidOperationException($"Invalid pipe: {pipe}")
          },
        _ => throw new ArgumentOutOfRangeException(nameof(pipe.Direction), pipe.Direction, null)
      };

  Char this[Position pos] => lines[pos.Y][pos.X];

  Pipe Go(Pipe current, Direction dir)
  {
    if (!TryGo(current.Position, dir, out var newPos))
      throw new ArgumentException($"Move not possible: {current}, direction: {dir}");

    return new Pipe(newPos, this[newPos], dir);
  }

  bool TryGo(Position pos, Direction dir, [NotNullWhen(true)] out Position? newPos)
  {
    if (CanGo(pos, dir))
    {
      newPos = Go(pos, dir);
      return true;
    }

    newPos = null;
    return false;
  }

  bool CanGo(Position pos, Direction dir) => dir switch {
      Direction.Up => pos.Y > 0,
      Direction.Down => pos.Y < maxY,
      Direction.Left => pos.X > 0,
      Direction.Right => pos.X < maxX,
      _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null),
    };

  static Position Go(Position pos, Direction dir) => dir switch
    {
      Direction.Up => pos with { Y = pos.Y - 1 },
      Direction.Down => pos with { Y = pos.Y + 1 },
      Direction.Left => pos with { X = pos.X - 1 },
      Direction.Right => pos with { X = pos.X + 1 },
      _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
    };

  Position FindStart()
  {
    for (var y = 0; y < lines.Length; y++)
    {
      var line = lines[y];
      var x = line.IndexOf('S');
      if (x != -1) return new Position(x, y);
    }

    throw new ArgumentException("No start position", nameof(lines));
  }

  (Pipe, Pipe) FindConnections(Position pos)
  {
    var connections = new List<Pipe>();

    if (TryGo(pos, Direction.Up, out var up))
    {
      var upForm = this[up];
      if (upForm is '|' or '7' or 'F' or 'S')
      {
        connections.Add(new Pipe(up, upForm, Direction.Up));
      }
    }

    if (TryGo(pos, Direction.Down, out var down))
    {
      var downForm = this[down];
      if (downForm is '|' or 'L' or 'J' or 'S')
      {
        connections.Add(new Pipe(down, downForm, Direction.Down));
      }
    }

    if (TryGo(pos, Direction.Left, out var left))
    {
      var leftForm = this[left];
      if (leftForm is '-' or 'L' or 'F' or 'S')
      {
        connections.Add(new Pipe(left, leftForm, Direction.Left));
      }
    }

    if (TryGo(pos, Direction.Right, out var right))
    {
      var rightForm = this[right];
      if (rightForm is '-' or 'J' or '7' or 'S')
      {
        connections.Add(new Pipe(right, rightForm, Direction.Right));
      }
    }

    if (connections.Count != 2)
      throw new ArgumentException($"No 2 connections (instead {connections.Count}) at {pos}.", nameof(pos));

    return (connections[0], connections[1]);
  }

  record Pipe(Position Position, Char Form, Direction Direction);
  record Position(int X, int Y);
  enum Direction
  {
    Up,
    Down,
    Left,
    Right
  }
}
