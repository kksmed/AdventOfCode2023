
namespace Day5;

public record PlantField
{
  public List<long> Seeds { get; } = new();

  public Map[] Maps { get; } = { new(), new(), new(), new(), new(), new(), new() };

  public IEnumerable<(long Seed, long Soil, long Fertilizer, long Water, long Light, long Temperature, long Humidity,
    long Location)> Convert() => Seeds.Select(x => (Seed: x, Soil: Maps[0].Convert(x)))
    .Select(x => (x.Seed, x.Soil, Fertilizer: Maps[1].Convert(x.Soil)))
    .Select(x => (x.Seed, x.Soil, x.Fertilizer, Water: Maps[2].Convert(x.Fertilizer)))
    .Select(x => (x.Seed, x.Soil, x.Fertilizer, x.Water, Light: Maps[3].Convert(x.Water)))
    .Select(x => (x.Seed, x.Soil, x.Fertilizer, x.Water, x.Light, Temperature: Maps[4].Convert(x.Light)))
    .Select(x => (x.Seed, x.Soil, x.Fertilizer, x.Water, x.Light, x.Temperature, Humidity: Maps[5].Convert(x.Temperature)))
    .Select(x => (x.Seed, x.Soil, x.Fertilizer, x.Water, x.Light, x.Temperature, x.Humidity, Location: Maps[6].Convert(x.Humidity)));
}
