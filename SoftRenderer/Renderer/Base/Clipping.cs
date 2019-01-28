using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftRenderer
{

    public enum ClippingMode
    {
        Off,
        SixPlane,
        OnlyNF,
    }

    public class Clipping
    {
        static List<Vector4> g_clipping_planes = new List<Vector4>
        {
            new Vector4(1,0,0,1),
            new Vector4(-1,0,0,1),
            new Vector4(0,-1,0,1),
            new Vector4(0,1,0,1),
            new Vector4(0,0,1,1),
            new Vector4(0,0,-1,1),
        };

        static List<Vector4> g_nf_clipping_planes = new List<Vector4>
        {
            new Vector4(0,0,1,1),
            new Vector4(0,0,-1,1),
        };

        static List<Vector4> GetClippingPlanes(Context context)
        {
            if(context.clippingMode == ClippingMode.OnlyNF)
            {
                return g_nf_clipping_planes;
            }
            return g_clipping_planes;
        }

        public static List<VSOutput> Clip(List<VSOutput> inputs, Context context)
        {
            List <VSOutput> outputs = inputs;
            if(context.clippingMode == ClippingMode.Off)
            {
                return outputs;
            }

            //Sutherland-Hodgman 算法
            List<Vector4> planes = GetClippingPlanes(context);
            for (int i = 0; i < planes.Count; ++i)
            {
                inputs = outputs;
                Vector4 plane = planes[i];
                outputs = ClipToPlane(inputs, plane);
                if(outputs.Count <= 0)
                {
                    break;
                }
            }

            return outputs;
        }

        static VSOutput ComputeIntersection(VSOutput S, VSOutput E, float ds, float de)
        {
            float amount = ds / (ds - de);
            VSOutput v = VSOutput.Lerp(S, E, amount);
            return v;
        }

        static List<VSOutput> ClipToPlane(List<VSOutput> inputs, Vector4 plane)
        {
            List<VSOutput> outputs = new List<VSOutput>();
            VSOutput S = inputs.Last();
            float ds = Vector4.Dot(plane, S.position);
            bool sInside = ds >= 0;
            foreach(var E in inputs)
            {
                float de = Vector4.Dot(plane, E.position);
                bool eInside = de >= 0;
                if( eInside )
                {
                    if (!sInside)
                    {
                        VSOutput C = ComputeIntersection(S, E, ds, de);
                        outputs.Add(C);
                    }
                    outputs.Add(E);
                }
                else if(sInside)
                {
                    VSOutput C = ComputeIntersection(S, E, ds, de);
                    outputs.Add(C);
                }

                //记录为上一个
                S = E;
                ds = de;
                sInside = eInside;
            }
            return outputs;
        }
    }
}
