public class Line
{
    private Point _pointA, _pointB;

    public Line(Point pointA, Point pointB)
    {
        _pointA = pointA;
        _pointB = pointB;
    }

    public Point PointA => _pointA;
    public Point PointB => _pointB;
}
