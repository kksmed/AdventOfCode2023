using System.Diagnostics.CodeAnalysis;

namespace Day10;

class Map
{
  readonly string[] lines;
  readonly HashSet<Position> loop = new();
  readonly int maxY;
  readonly int maxX;

  public Map(string[] lines)
  {
    this.lines = lines;
    maxY = lines.Length - 1;
    maxX = lines[0].Length - 1;
  }

  public int GetEnclosedTiles()
  {
    var start = FindStart();
    SetLoop(start);
    var (end1, end2) = FindConnections(start);
    var s = DetermineStart(end1.Direction, end2.Direction);
    lines[start.Y] = lines[start.Y].Replace('S', s);
    SetLoop(end1.Position);
    SetLoop(end2.Position);
    while (true)
    {
      end1 = GoNext(end1);
      if (end1.Position == end2.Position) break;
      SetLoop(end1.Position);

      end2 = GoNext(end2);
      if (end1.Position == end2.Position) break;
      SetLoop(end2.Position);
    }

    return FindEnclosed();
  }

  int FindEnclosed()
  {
    var enclosed = 0;
    for (var x = 0; x <= maxX; x++)
    {
      var isEnclosed = false;
      bool? fromLeft = null;
      for (var y = 0; y <= maxY; y++)
      {
        if (loop.Contains(new Position(x, y)))
        {
          switch (this[x, y])
          {
            case '-':
              isEnclosed = !isEnclosed;
              continue;
            case '7':
              fromLeft = true;
              continue;
            case 'F':
              fromLeft = false;
              continue;
            case 'L':
              if (!fromLeft.HasValue) throw new InvalidOperationException($"{nameof(fromLeft)} is not set!");
              if (fromLeft.Value)
                isEnclosed = !isEnclosed;
              fromLeft = null;
              continue;
            case 'J':
              if (!fromLeft.HasValue) throw new InvalidOperationException($"{nameof(fromLeft)} is not set!");
              if (!fromLeft.Value)
                isEnclosed = !isEnclosed;
              fromLeft = null;
              continue;
            case '|':
              continue;
            default: throw new InvalidOperationException($"Unexpected form '{this[x, y]}' at ({x}, {y})");
          }
        }

        if (isEnclosed) enclosed++;
      }
    }

    return enclosed;
  }

  void SetLoop(Position pos) => loop.Add(pos);

  public int GetFarthestDistance()
  {
    var start = FindStart();
    var (end1, end2) = FindConnections(start);
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

  char this[int x, int y] => lines[y][x];
  char this[Position pos] => this[pos.X, pos.Y];

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

  static char DetermineStart(Direction d1, Direction d2) => d1 switch
    {
      Direction.Up => d2 switch
        {
          Direction.Down => '|',
          Direction.Left => 'J',
          Direction.Right => 'L',
          _ => throw new InvalidOperationException($"Invalid start pipe: {d1} & {d2}")
        },
      Direction.Down => d2 switch
        {
          Direction.Up => '|',
          Direction.Left => '7',
          Direction.Right => 'F',
          _ => throw new InvalidOperationException($"Invalid start pipe: {d1} & {d2}")
        },
      Direction.Left => d2 switch
        {
          Direction.Down => '7',
          Direction.Up => 'J',
          Direction.Right => '-',
          _ => throw new InvalidOperationException($"Invalid start pipe: {d1} & {d2}")
        },
      Direction.Right => d2 switch
        {
          Direction.Down => 'F',
          Direction.Up => 'L',
          Direction.Left => '-',
          _ => throw new InvalidOperationException($"Invalid start pipe: {d1} & {d2}")
        },
      _ => throw new ArgumentOutOfRangeException(nameof(d1), d1, null)
    };

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