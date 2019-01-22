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
                transform.scale = Vector3.One;
                Matrix4x4 C = transform.ModelToWorld;
                Matrix4x4 V = Matrix4x4.Identity;
                Matrix4x4.Invert(C, out V);
                return V;
            }
        }

        public Matrix4x4 Projection
        {
            get
            {
                //return Matrix4x4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
                //上面自带的方法ndc的z范围是[0,1]，所以要自己写成[-1,1]范围的
                return GetFrustum(fov, aspect, near, far);
            }
        }

        public static Matrix4x4 RawGetFrustum(float l, float r, float b, float t, float n, float f)
        {
            return new Matrix4x4(
                2*n/(r-l), 0, 0, 0,
                0, 2*n/(t-b), 0, 0,
                (r+l)/(r-l), (t+b)/(t-b), -(f+n)/(f-n), -1,
                0, 0, -(2*f*n)/(f-n), 0
                );
           
        }

        public static Matrix4x4 GetFrustum(float fovY, float aspect, float front, float back)
        {
            float tangent = (float)Math.Tan(Utils.Degree2Radian(fovY / 2) ); // tangent of half fovY
            float height = front * tangent;         // half height of near plane
            float width = height * aspect;          // half width of near plane
            return RawGetFrustum(-width, width, -height, height, front, back);
        }



        public void LookAt(Vector3 lookAtPos)
        {
            //TODO
        }
    }
}
