using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Vector3D
    {
        public decimal X, Y, Z;
        public Vector3D()
        {
        }

        public Vector3D(decimal aX, decimal aY, decimal aZ)
        {
            this.X = aX;
            this.Y = aY;
            this.Z = aZ;
        }

        public Vector3D Add(Vector3D aVector)
        {
            var varX = this.X + aVector.X;
            var varY = this.Y + aVector.Y;
            var varZ = this.Z + aVector.Z;
            Vector3D localVar = new Vector3D(varX, varY, varZ);
            return localVar;
        }
    }

    public class AnacleTest
    {
        static void Main(string[] args)
        {
            Vector3D a = new Vector3D(2, 3, 4);
            Vector3D b = new Vector3D(1, 1, 1);
            Vector3D c = new Vector3D();
            c = b.Add(a);
            Console.WriteLine(c.X + ", " + c.Y + ", " + c.Z);
        }
    }
}
