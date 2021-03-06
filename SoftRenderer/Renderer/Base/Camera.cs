﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftRenderer
{
    public class Camera
    {
        public float Near = 0f;
        public float Far = 1f;
        public float Fov = 15f;
        public float Aspect = 1f;

        public Transform transform = new Transform();
        public Matrix4x4 ViewMatrix
        {
            get
            {
                transform.scale = Vector3.One;
                Matrix4x4 C = transform.ModelToWorld;
                Matrix4x4 V = Matrix4x4.Identity;
                Matrix4x4.Invert(C, out V);
                Matrix4x4 invertZ = Matrix4x4.Identity;
                invertZ.M33 = -1;
                V = V * invertZ;
                return V;
            }
        }

        public Matrix4x4 ProjectionMatrix
        {
            get
            {
                //return Matrix4x4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
                //上面自带的方法ndc的z范围是[0,1]，所以要自己写成[-1,1]范围的
                return GetFrustum(Fov, Aspect, Near, Far);
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
