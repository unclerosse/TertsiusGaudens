namespace ClosedEllipse.Models;

public record Point
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public double Z { get; private set; }

    public double Length { get; private set; }
    public static readonly Point ZERO = new(0, 0, 0);


    public Point(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;

        Length = Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public Point(double[] coordinates)
    {
        if (coordinates == null || coordinates.Length != 3)
            throw new ArgumentException("Coordinates cannot be non 3D-format");

        X = coordinates[0];
        Y = coordinates[1];
        Z = coordinates[2];

        Length = Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public Point Rotate(double alpha, double beta, double gamma)
    {
        var sinAlpha = Math.Sin(alpha);
        var cosAlpha = Math.Cos(alpha);

        var sinBeta = Math.Sin(beta);
        var cosBeta = Math.Cos(beta);

        var sinGamma = Math.Sin(gamma);
        var cosGamma = Math.Cos(gamma);

        double rotatedX = X * cosBeta * cosGamma + 
                        Y * (-cosBeta * sinGamma) + 
                        Z * sinBeta;

        double rotatedY = X * (cosAlpha * sinGamma  + sinAlpha * sinBeta * cosGamma) +
                        Y * (cosAlpha * cosGamma - sinAlpha * sinBeta * sinGamma) +
                        Z * (-cosBeta * sinAlpha);

        double rotatedZ = X * (sinAlpha * sinGamma - cosAlpha * sinBeta * cosGamma) +
                        Y * (cosGamma * sinAlpha + cosAlpha * sinBeta * sinGamma) +
                        Z * cosAlpha * cosBeta;

        return new Point(rotatedX, rotatedY, rotatedZ);
    }

    public static Point operator +(Point p1, Point p2) { return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z); }
    public static Point operator -(Point p1, Point p2) { return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z); }
    public static Point operator *(Point p, double number) { return new Point(p.X * number, p.Y * number, p.Z * number); }
    public static Point operator /(Point p, double number) { return new Point(p.X / number, p.Y / number, p.Z / number); }

    public static double Distance(Point p1, Point p2) { return (p2 - p1).Length; }
    public static double Distance(Point p) { return p.Length; }
}