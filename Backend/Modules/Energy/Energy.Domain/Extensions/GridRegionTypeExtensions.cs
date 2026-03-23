namespace Energy.Domain.Models;

public static class GridRegionTypeExtensions
{
    private static readonly Dictionary<GridRegionType, Coordinate> RegionCoordinates = new()
    {
        [GridRegionType.NorthScotland] = new(-4.5, 57.5),
        [GridRegionType.SouthScotland] = new(-3.5, 55.9),
        [GridRegionType.NorthWestEngland] = new(-2.7, 53.8),
        [GridRegionType.NorthEastEngland] = new(-1.5, 54.9),
        [GridRegionType.Yorkshire] = new(-1.3, 53.8),
        [GridRegionType.NorthWales] = new(-3.5, 53.2),
        [GridRegionType.SouthWales] = new(-3.4, 51.7),
        [GridRegionType.WestMidlands] = new(-2.0, 52.5),
        [GridRegionType.EastMidlands] = new(-1.0, 52.8),
        [GridRegionType.EastEngland] = new(0.9, 52.2),
        [GridRegionType.SouthWestEngland] = new(-3.5, 50.7),
        [GridRegionType.SouthEngland] = new(-1.3, 51.0),
        [GridRegionType.London] = new(-0.1, 51.5),
        [GridRegionType.SouthEastEngland] = new(0.5, 51.2),
        [GridRegionType.England] = new(-1.5, 52.5),
        [GridRegionType.Scotland] = new(-4.0, 56.5),
        [GridRegionType.Wales] = new(-3.5, 52.0)
    };

    public static Coordinate GetCoordinate(this GridRegionType regionType)
    {
        return RegionCoordinates.GetValueOrDefault(regionType, new Coordinate(0.0, 0.0));
    }
}
