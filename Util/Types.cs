namespace Advent.Util {
    /// <summary>
    /// Helper type to represent integer X and Y coordinates
    /// </summary>
    public record struct Coords(int X, int Y) {

        public static Coords operator +(Coords c) 
            => c;

        public static Coords operator -(Coords c) 
            => new Coords(-c.X, -c.Y);

        public static Coords operator +(Coords a, Coords b) 
            => new Coords(a.X + b.X, a.Y + b.Y);

        public static Coords operator -(Coords a, Coords b)
            => a + -b;
    }
}
