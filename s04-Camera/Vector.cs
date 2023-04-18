using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class Vector3
    {
        public double x;
        public double y;
        public double z;

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        // Vector operations
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector3 operator *(Vector3 a, double b) => new Vector3(a.x * b, a.y * b, a.z * b);
        public static Vector3 operator *(double b, Vector3 a) => new Vector3(a.x * b, a.y * b, a.z * b);
        public static Vector3 operator /(Vector3 a, double b) => new Vector3(a.x / b, a.y / b, a.z / b);

        // Dot product
        public static double operator *(Vector3 a, Vector3 b) => (a.x * b.x) + (a.y * b.y) + (a.z * b.z);

        // Cross product
        public static Vector3 CrossProduct(Vector3 a, Vector3 b)
        {
            double cx = (a.y * b.z) - (b.y * a.z);
            double cy = ((a.x * b.z) - (b.x * a.z)) * -1;
            double cz = (a.x * b.y) - (b.x * a.y);

            return new Vector3(cx, cy, cz);
        }

        // Norm
        public double getNorm()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        // Normalize
        public Vector3 Normalize()
        {
            double norm = getNorm();
            double X = x / norm;
            double Y = y / norm;
            double Z = z / norm;

            return new Vector3(X, Y, Z);
        }

        // "other" is the vector we are projecting onto
        public Vector3 ProjectOntoVector(Vector3 other)
        {
            // Using formula for vector projection
            return other * ((this * other) / (other.getNorm() * other.getNorm()));
        }

        // Rotates vector using the vector k as an axis.
        public void RotateAlongVector(double deg, Vector3 k)
        {
            //Convert to radians
            double angle = deg * (Math.PI / 180);

            // Create a copy of our vector
            Vector3 v = new Vector3(x, y, z);
            Vector3 scaled_v = v * Math.Cos(angle);
            Vector3 skew = CrossProduct(k, v) * Math.Sin(angle);
            v = scaled_v + skew + (k * (k * v) * (1 - Math.Cos(angle)));

            // Normalize the vector to prevent it from changing size (very important!).
            v = v / v.getNorm();

            x = v.x;
            y = v.y;
            z = v.z;
        }

        // Outputs vector components
        public void Print()
        {
            Console.WriteLine(x.ToString() + ", " + y.ToString() + ", " + z.ToString());
        }
    }
}
