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
        public Quaternion rotation = Quaternion.Identity;
        //TODO 把欧拉角换成四元数;怎么把四元数转回欧拉角
        public Vector3 eulerAngles = Vector3.Zero;

        public Matrix4x4 ModelToWorld
        {
            get
            {
                float radX = Utils.Degree2Radian(eulerAngles.X);
                float radY = Utils.Degree2Radian(eulerAngles.Y);
                float radZ = Utils.Degree2Radian(eulerAngles.Z);
                //row major的时候按p' = p*SRT的顺序
                return
                    Matrix4x4.CreateScale(scale) *
                    Matrix4x4.CreateFromYawPitchRoll(radY, radX, radZ) *
                    Matrix4x4.CreateTranslation(position);
            }
        }

        public Vector3 EulerAngles
        {
            set {
                float radX = Utils.Degree2Radian(eulerAngles.X);
                float radY = Utils.Degree2Radian(eulerAngles.Y);
                float radZ = Utils.Degree2Radian(eulerAngles.Z);
                rotation = Quaternion.CreateFromYawPitchRoll(radY, radX, radZ);
            }
            get
            {
                return Utils.QuaternionToEulerAngles(rotation);
            }
        }
    }
}
