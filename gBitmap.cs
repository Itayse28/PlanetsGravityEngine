using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsTest
{
    internal class gBitmap
    {
        public Bitmap Bitmap { get; set; }
        public gBitmap()
        {
            Bitmap=new Bitmap(100,100);
        }
    }
}
