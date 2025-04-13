using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicsTest
{
    internal class Camera
    {
        public double x, y, z;
        public double xrot, yrot, zrot;
        public double DSx, DSy, DSz;
        public double lx, ly, lz, length;
        public Camera(double x, double y, double z, double xrot, double yrot, double zrot, double DSx, double DSy, double DSz)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.xrot = xrot;
            this.yrot = yrot;
            this.zrot = zrot;
            this.DSx = DSx;
            this.DSy = DSy;
            this.DSz = DSz;
            ly = Cos(yrot) * Sin(zrot);
            lx = Sin(yrot);
            lz = Cos(yrot) * Cos(zrot);
            length = Math.Sqrt(lx * lx + ly * ly + lz * lz);
        }
        public Camera(double x, double y, double z) : this(x, y, z, 0, 0, 0, 360, 360, 360)
        {
        }
        public Camera() : this(0,0,0)
        { 
        }
        private double Sin(double angle)
        {
            return Math.Sin(angle * 0.01745329252);
        }
        private double Cos(double angle)
        {
            return Math.Cos(angle * 0.01745329252);
        }
        private double Tan(double angle)
        {
            return Math.Tan(angle * 0.01745329252);
        }
        public void forward(double step)
        {
            x += lx * step;
            z += lz * step;
        }
        public void sideways(double step)
        {
            x += lz * step;
            z -= lx * step;
        }
        private void calcVec()
        {
            ly = Cos(yrot) * Sin(zrot);
            lx = Sin(yrot);
            lz = Cos(yrot) * Cos(zrot);
            length = Math.Sqrt(lx * lx + ly * ly + lz * lz);
            ly /= length;
            lx /= length;
            lz /= length;
        }
        public void rotate(char axis,double angle)
        {
            switch(axis)
            {
                case ('x'):
                    xrot += angle;
                    break;
                case ('y'):
                    yrot -= angle;
                    calcVec();
                    break;
                case ('z'):
                    yrot += angle;
                    calcVec();
                    break;
            }
        }
        public double[]? project(double[] A)
        {
            //gets a 3d point 'A' and returns a 2d point that is the projection of A
            //followed by: https://en.wikipedia.org/wiki/3D_projection#Perspective_projection
            
            double X = A[0] - this.x;
            double Y = A[1] - this.y;
            double Z = A[2] - this.z;

            //created by me, used to determened if a point is behind the camera
            if (X * lx + Y * ly + Z * lz < 0)
                return null;

            double sx = Sin(xrot);
            double sy = Sin(yrot);
            double sz = Sin(zrot);
            
            double cx=Cos(xrot);
            double cy=Cos(yrot);
            double cz=Cos(zrot);
            
            double[] d = new double[3];
            
            d[0] = cy * (sz*Y + cz*X) - sy*Z;
            d[1] = sx * (cy*Z + sy*(sz*Y + cz*X)) + cx*(cz * Y - sz * X);
            d[2] = cx * (cy*Z + sy*(sz*Y + cz*X)) - sx*(cz * Y - sz * X);

            double[] b = new double[2];
            b[0] = (DSz / d[2]) * d[0] + DSx;
            b[1] = (DSz / d[2]) * d[1] + DSy;
            
            return b;
        }
        public double[,] project(double[,] A)
        {
            double[,] res = new double[A.GetLongLength(0),2];
            for(int i = 0; i < A.GetLongLength(0); i++)
            {
                double[] a = { A[i, 0], A[i, 1], A[i, 2]};
                double[]? pro=project(a);
                if(pro == null)
                {
                    res[i,0] = -1;
                    res[i,1] = -1;
                    continue;
                }
                res[i,0] = pro[0];
                res[i,1] = pro[1];
            }
            return res;
        }
    }
}
