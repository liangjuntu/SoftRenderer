using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer
{
    public class Statics
    {
        public int meshCount = 0;
        public int submeshCount = 0;
        public int vertexCount = 0;
        public int triangleCount = 0;
        public int fragmentCount = 0;
        public void Clear()
        {
            meshCount = 0;
            submeshCount = 0;
            vertexCount = 0;
            triangleCount = 0;
            fragmentCount = 0;
        }
    }
}
