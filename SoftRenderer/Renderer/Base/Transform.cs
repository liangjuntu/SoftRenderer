using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftRenderer
{
    public class Transform
    {
        public Vector3 position = Vector3.Zero;
        public Vector3 scale = Vector3.One;
        public Vector3 eulerAngles = Vector3.Zero;

        public Matrix4x4 ModelToWorld
        {
            get
            {
                //TODO
                return Matrix4x4.Identity;
            }
        }
    }
}
