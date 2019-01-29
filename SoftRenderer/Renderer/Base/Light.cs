using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftRenderer
{
    public class Light
    {
        //方向光
        public Vector3 lightDir = new Vector3(0, 0, 1);
        public Vector4 lightColor = Vector4.One;
        //public Vector4 lightColor = new Vector4(1, 0, 0, 1);
        public Vector3 lightDirForShader = new Vector3(0, 0, -1);
        //TODO其他类型的光源
        public void InitForShader()
        {
            if (lightDir != Vector3.Zero)
            {
                //shader里实际用的是反过来的方向：物体到光源的方向
                lightDirForShader = -Vector3.Normalize(lightDir);
            }
            else
            {
                lightDirForShader = new Vector3(0, 0, 1);
            }
        }
    }
}
