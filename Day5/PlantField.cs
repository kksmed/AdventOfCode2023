
namespace Day5;

public record PlantField
{
  public List<Range> Seeds { get; } = new();

  public Map[] Maps { get; } = { new(), new(), new(), new(), new(), new(), new() };

  public IEnumerable<Range> ConvertSeedsToLocations()
  {
    var soils = Seeds.SelectMany(x => Maps[0].Convert(x)).ToList();
    var fertilizers = soils.SelectMany(x => Maps[1].Convert(x)).ToList();
    var waters = fertilizers.SelectMany(x => Maps[2].Convert(x)).ToList();
    var lights = waters.SelectMany(x => Maps[3].Convert(x)).ToList();
    var temperatures = lights.SelectMany(x => Maps[4].Convert(x)).ToList();
    var humidities = temperatures.SelectMany(x => Maps[5].Convert(x)).ToList();
    return humidities.SelectMany(x => Maps[6].Convert(x)).ToList();
  }
}