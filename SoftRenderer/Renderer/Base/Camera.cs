using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftRenderer
{
    public class Camera
    {
        float near = 0f;
        float far = 1f;
        float fov = 15f;
        float aspect = 1f;
       
        public Transform transform;
        public Matrix4x4 World2Camera
        {
            get
            {
                //TODO
                return Matrix4x4.Identity;
            }
        }
        public Matrix4x4 Projection
        {
            get
            {
                //TODO
                return Matrix4x4.Identity;
            }
        }
    }
}
