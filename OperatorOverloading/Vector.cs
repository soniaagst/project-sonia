public readonly struct Vector{
    public double X {get;}
    public double Y {get;}
    public double Z {get;}
    public Vector(double x, double y, double z){
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector operator +(Vector v1, Vector v2) {
        return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static bool operator ==(Vector v1, Vector v2) {
        return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
    }

    public static bool operator !=(Vector v1, Vector v2) {
        return !(v1==v2);
    }

    public override bool Equals(object? v1) { // why need override Equals?
        if (v1 is not null) {
            return (Vector)v1 == this;
        }
        else {
            return false;
        }
    }

    public override int GetHashCode() { // wtf is this
        return base.GetHashCode();
    }

    public static Vector operator *(Vector v1, Vector v2) { // dot product
        return new Vector(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
    }

    public static Vector operator &(Vector v1, Vector v2) { // cross product
        double x = v1.Y * v2.Z - v1.Z * v2.Y;
        double y = v1.X * v2.Z - v1.Z * v2.X;
        double z = v1.X * v2.Y - v1.Y * v2.X;
        return new Vector(x, y, z);
    }

    public static Vector operator ^(int scalar, Vector v) { // scalar mult
        return new Vector(scalar * v.X, scalar * v.Y, scalar * v.Z);
    }

    public static Vector operator ~(Vector v) {
        return new Vector(-v.X, -v.Y, -v.Z);
    }

    public static Vector operator ++(Vector v) {
        return new Vector(v.X + 1, v.Y + 1, v.Z + 1);
    }

    public static Vector operator --(Vector v) {
        return new Vector(v.X - 1, v.Y - 1, v.Z - 1);
    }

    public void PrintValue() {
        Console.WriteLine($"({X}, {Y}, {Z})");
    }
}
