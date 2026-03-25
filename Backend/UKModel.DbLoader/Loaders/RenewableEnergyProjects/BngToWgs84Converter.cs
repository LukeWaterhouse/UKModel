namespace UKModel.DbLoader.Loaders.RenewableEnergyProjects;

public static class BngToWgs84Converter
{
    // Airy 1830 ellipsoid (used by OSGB36)
    private const double AiryA = 6377563.396;
    private const double AiryB = 6356256.909;

    // GRS80 ellipsoid (used by WGS84)
    private const double Grs80A = 6378137.0;
    private const double Grs80B = 6356752.314140;

    // National Grid true origin
    private const double N0 = -100000.0;
    private const double E0 = 400000.0;
    private const double Lat0 = 49.0 * Math.PI / 180.0;
    private const double Lon0 = -2.0 * Math.PI / 180.0;
    private const double F0 = 0.9996012717;

    // Helmert transform parameters (OSGB36 → WGS84)
    private const double Tx = 446.448;
    private const double Ty = -125.157;
    private const double Tz = 542.060;
    private const double Rx = 0.1502 / 3600.0 * Math.PI / 180.0;
    private const double Ry = 0.2470 / 3600.0 * Math.PI / 180.0;
    private const double Rz = 0.8421 / 3600.0 * Math.PI / 180.0;
    private const double S = -20.4894e-6;

    public static (double Latitude, double Longitude)? Convert(decimal? easting, decimal? northing)
    {
        if (easting is null || northing is null)
            return null;

        var e = (double)easting.Value;
        var n = (double)northing.Value;

        if (e == 0 && n == 0)
            return null;

        // Step 1: BNG → OSGB36 lat/lon (reverse transverse Mercator)
        var (osgbLat, osgbLon) = BngToOsgb36(e, n);

        // Step 2: OSGB36 lat/lon → OSGB36 cartesian
        var (x1, y1, z1) = LatLonToCartesian(osgbLat, osgbLon, 0, AiryA, AiryB);

        // Step 3: Helmert transform → WGS84 cartesian
        var x2 = Tx + (1 + S) * (x1 - Rz * y1 + Ry * z1);
        var y2 = Ty + (1 + S) * (Rz * x1 + y1 - Rx * z1);
        var z2 = Tz + (1 + S) * (-Ry * x1 + Rx * y1 + z1);

        // Step 4: WGS84 cartesian → WGS84 lat/lon
        var (wgsLat, wgsLon) = CartesianToLatLon(x2, y2, z2, Grs80A, Grs80B);

        return (wgsLat * 180.0 / Math.PI, wgsLon * 180.0 / Math.PI);
    }

    private static (double Lat, double Lon) BngToOsgb36(double e, double n)
    {
        var a = AiryA;
        var b = AiryB;
        var e2 = (a * a - b * b) / (a * a);

        var lat = Lat0;
        var m = 0.0;

        // Iteratively solve for latitude
        do
        {
            lat = (n - N0 - m) / (a * F0) + lat;
            m = MeridionalArc(lat, a, b);
        } while (Math.Abs(n - N0 - m) >= 1e-5);

        var sinLat = Math.Sin(lat);
        var cosLat = Math.Cos(lat);
        var tanLat = Math.Tan(lat);
        var nu = a * F0 / Math.Sqrt(1 - e2 * sinLat * sinLat);
        var rho = a * F0 * (1 - e2) / Math.Pow(1 - e2 * sinLat * sinLat, 1.5);
        var eta2 = nu / rho - 1;

        var dE = e - E0;
        var tan2 = tanLat * tanLat;
        var tan4 = tan2 * tan2;
        var nu3 = nu * nu * nu;
        var nu5 = nu3 * nu * nu;
        var nu7 = nu5 * nu * nu;

        var VII = tanLat / (2 * rho * nu);
        var VIII = tanLat / (24 * rho * nu3) * (5 + 3 * tan2 + eta2 - 9 * tan2 * eta2);
        var IX = tanLat / (720 * rho * nu5) * (61 + 90 * tan2 + 45 * tan4);
        var X = 1.0 / (cosLat * nu);
        var XI = 1.0 / (6 * cosLat * nu3) * (nu / rho + 2 * tan2);
        var XII = 1.0 / (120 * cosLat * nu5) * (5 + 28 * tan2 + 24 * tan4);
        var XIIA = 1.0 / (5040 * cosLat * nu7) * (61 + 662 * tan2 + 1320 * tan4 + 720 * tan4 * tan2);

        var dE2 = dE * dE;
        lat = lat - VII * dE2 + VIII * dE2 * dE2 - IX * dE2 * dE2 * dE2;
        var lon = Lon0 + X * dE - XI * dE2 * dE + XII * dE2 * dE2 * dE - XIIA * dE2 * dE2 * dE2 * dE;

        return (lat, lon);
    }

    private static double MeridionalArc(double lat, double a, double b)
    {
        var n = (a - b) / (a + b);
        var n2 = n * n;
        var n3 = n2 * n;

        var latDiff = lat - Lat0;
        var latSum = lat + Lat0;

        var ma = (1 + n + 5.0 / 4 * n2 + 5.0 / 4 * n3) * latDiff;
        var mb = (3 * n + 3 * n2 + 21.0 / 8 * n3) * Math.Sin(latDiff) * Math.Cos(latSum);
        var mc = (15.0 / 8 * n2 + 15.0 / 8 * n3) * Math.Sin(2 * latDiff) * Math.Cos(2 * latSum);
        var md = 35.0 / 24 * n3 * Math.Sin(3 * latDiff) * Math.Cos(3 * latSum);

        return b * F0 * (ma - mb + mc - md);
    }

    private static (double X, double Y, double Z) LatLonToCartesian(double lat, double lon, double h, double a, double b)
    {
        var e2 = (a * a - b * b) / (a * a);
        var sinLat = Math.Sin(lat);
        var cosLat = Math.Cos(lat);
        var nu = a / Math.Sqrt(1 - e2 * sinLat * sinLat);

        var x = (nu + h) * cosLat * Math.Cos(lon);
        var y = (nu + h) * cosLat * Math.Sin(lon);
        var z = ((1 - e2) * nu + h) * sinLat;

        return (x, y, z);
    }

    private static (double Lat, double Lon) CartesianToLatLon(double x, double y, double z, double a, double b)
    {
        var e2 = (a * a - b * b) / (a * a);
        var lon = Math.Atan2(y, x);
        var p = Math.Sqrt(x * x + y * y);
        var lat = Math.Atan2(z, p * (1 - e2));

        for (var i = 0; i < 10; i++)
        {
            var sinLat = Math.Sin(lat);
            var nu = a / Math.Sqrt(1 - e2 * sinLat * sinLat);
            lat = Math.Atan2(z + e2 * nu * sinLat, p);
        }

        return (lat, lon);
    }
}
