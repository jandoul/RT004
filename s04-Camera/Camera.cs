using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    internal class Camera
    {
        public Vector3 position;

        // The orientation of the camera (all 3 vectors are orthonormal)
        public Vector3 view;
        public Vector3 right;
        public Vector3 up;

        public double d;        // Distance from projection plane

        public Camera(Vector3 position, Vector3 view, double angle, double d)
        {
            this.position = position;
            this.d = d;

            this.view = view.Normalize();
            this.up = new Vector3(0, 1.0, 0);
            this.right = Vector3.CrossProduct(view, up).Normalize();
            this.up = Vector3.CrossProduct(view, -1.0 * right).Normalize();
        }
    }
}
