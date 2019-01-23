using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Games;

namespace SoftRenderer.Test
{
    /// <summary>
    /// 正方形测试数据
    /// </summary>
    public class CubeTestData
    {
        //顶点坐标
        public static Vector3[] pointList = {
                                            new Vector3(-1,  1, -1),
                                            new Vector3(-1, -1, -1),
                                            new Vector3(1, -1, -1),
                                            new Vector3(1, 1, -1),

                                            new Vector3( -1,  1, 1),
                                            new Vector3(-1, -1, 1),
                                            new Vector3(1, -1, 1),
                                            new Vector3(1, 1, 1)
                                        };
        //三角形顶点索引 12个面
        public static int[] indexs = {   0,1,2,
                                   0,2,3,
                                   //
                                   7,6,5,
                                   7,5,4,
                                   //
                                   0,4,5,
                                   0,5,1,
                                   //
                                   1,5,6,
                                   1,6,2,
                                   //
                                   2,6,7,
                                   2,7,3,
                                   //
                                   3,7,4,
                                   3,4,0
                               };

        //uv坐标
        public static Vector2[] uvs ={
                                  new Vector2(0, 0),new Vector2( 0, 1),new Vector2(1, 1),
                                   new Vector2(0, 0),new Vector2(1, 1),new Vector2(1, 0),
                                   //
                                    new Vector2(0, 0),new Vector2( 0, 1),new Vector2(1, 1),
                                   new Vector2(0, 0),new Vector2(1, 1),new Vector2(1, 0),
                                   //
                                    new Vector2(0, 0),new Vector2( 0, 1),new Vector2(1, 1),
                                   new Vector2(0, 0),new Vector2(1, 1),new Vector2(1, 0),
                                   //
                                    new Vector2(0, 0),new Vector2( 0, 1),new Vector2(1, 1),
                                   new Vector2(0, 0),new Vector2(1, 1),new Vector2(1, 0),
                                   //
                                     new Vector2(0, 0),new Vector2( 0, 1),new Vector2(1, 1),
                                   new Vector2(0, 0),new Vector2(1, 1),new Vector2(1, 0),
                                   ///
                                     new Vector2(0, 0),new Vector2( 0, 1),new Vector2(1, 1),
                                   new Vector2(0, 0),new Vector2(1, 1),new Vector2(1, 0)
                              };
        //顶点色
        public static Vector3[] vertColors = {
                                             new Vector3( 0, 1, 0), new Vector3( 0, 0, 1), new Vector3( 1, 0, 0),
                                               new Vector3( 0, 1, 0), new Vector3( 1, 0, 0), new Vector3( 0, 0, 1),
                                               //
                                                new Vector3( 0, 1, 0), new Vector3( 0, 0, 1), new Vector3( 1, 0, 0),
                                               new Vector3( 0, 1, 0), new Vector3( 1, 0, 0), new Vector3( 0, 0, 1),
                                               //
                                                new Vector3( 0, 1, 0), new Vector3( 0, 0, 1), new Vector3( 1, 0, 0),
                                               new Vector3( 0, 1, 0), new Vector3( 1, 0, 0), new Vector3( 0, 0, 1),
                                               //
                                                new Vector3( 0, 1, 0), new Vector3( 0, 0, 1), new Vector3( 1, 0, 0),
                                               new Vector3( 0, 1, 0), new Vector3( 1, 0, 0), new Vector3( 0, 0, 1),
                                               //
                                                new Vector3( 0, 1, 0), new Vector3( 0, 0, 1), new Vector3( 1, 0, 0),
                                               new Vector3( 0, 1, 0), new Vector3( 1, 0, 0), new Vector3( 0, 0, 1),
                                               //
                                                new Vector3( 0, 1, 0), new Vector3( 0, 0, 1), new Vector3( 1, 0, 0),
                                               new Vector3( 0, 1, 0), new Vector3( 1, 0, 0), new Vector3( 0, 0, 1)
                                         };
        //法线
        public static Vector3[] normals = {
                                                new Vector3( 0, 0, -1), new Vector3(0, 0, -1), new Vector3( 0, 0, -1),
                                               new Vector3(0, 0, -1), new Vector3( 0, 0, -1), new Vector3( 0, 0, -1),
                                               //
                                                new Vector3( 0, 0, 1), new Vector3( 0, 0, 1), new Vector3( 0, 0, 1),
                                               new Vector3( 0, 0, 1), new Vector3( 0, 0, 1), new Vector3( 0, 0, 1),
                                               //
                                                new Vector3( -1, 0, 0), new Vector3( -1, 0, 0), new Vector3( -1, 0, 0),
                                               new Vector3( -1, 0, 0), new Vector3(-1, 0, 0), new Vector3( -1, 0, 0),
                                               //
                                                new Vector3( 0, -1, 0), new Vector3(  0, -1, 0), new Vector3(  0, -1, 0),
                                               new Vector3(  0, -1, 0), new Vector3( 0, -1, 0), new Vector3( 0, -1, 0),
                                               //
                                                new Vector3( 1, 0, 0), new Vector3( 1, 0, 0), new Vector3( 1, 0, 0),
                                               new Vector3( 1, 0, 0), new Vector3( 1, 0, 0), new Vector3( 1, 0, 0),
                                               //
                                                new Vector3( 0, 1, 0), new Vector3( 0, 1, 0), new Vector3( 0, 1, 0),
                                               new Vector3( 0, 1, 0 ), new Vector3(0, 1, 0), new Vector3( 0, 1, 0)
                                            };
        //材质
        //public static Material mat = new Material(new Color(0, 0, 0.1f), 0.1f, new Color(0.3f, 0.3f, 0.3f), new Color(1, 1, 1), 99);


        public static WavefrontObject ToWavefrontObject()
        {
            WavefrontObject obj = new WavefrontObject();
            for( int i = 0; i < CubeTestData.pointList.Length; ++i)
            {
                Vector3 point = CubeTestData.pointList[i];
                obj.Positions.Add(point);
            }
            for( int i = 0; i < CubeTestData.normals.Length; ++i)
            {
                Vector3 normal = CubeTestData.normals[i];
                obj.Normals.Add(normal);
            }
            for( int i = 0; i < CubeTestData.uvs.Length; ++i)
            {
                Vector2 uv = CubeTestData.uvs[i];
                obj.Texcoords.Add(uv);

            }
            for (int i = 0; i < CubeTestData.vertColors.Length; ++i)
            {
                Vector4 color = new Vector4(CubeTestData.vertColors[i],1);
                obj.Colors.Add(color);
            }

            //处理三角形
            WavefrontFaceGroup faceGroup = new WavefrontFaceGroup();
            obj.Groups.Add(faceGroup);
            for( int i = 0; i + 2 < CubeTestData.indexs.Length; i += 3)
            {
                WavefrontFace face = new WavefrontFace();
                faceGroup.Faces.Add(face);
                int index0 = CubeTestData.indexs[i];
                int index1 = CubeTestData.indexs[i+1];
                int index2 = CubeTestData.indexs[i+2];
                WavefrontVertex v0 = new WavefrontVertex(index0, index0, index0);
                WavefrontVertex v1 = new WavefrontVertex(index1, index1, index1);
                WavefrontVertex v2 = new WavefrontVertex(index2, index2, index2);
                v0.Color = index0;
                v1.Color = index1;
                v2.Color = index2;
                face.Vertices.Add(v0);
                face.Vertices.Add(v1);
                face.Vertices.Add(v2);
            }
            return obj;
        }
    }
}
