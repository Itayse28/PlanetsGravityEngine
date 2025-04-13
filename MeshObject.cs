using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsTest
{
    internal class MeshObject
    {
        public double[,] vertex;
        public int[,] edges;
        public int[,] faces;
        public double x, y,z;
        public bool doRotate, face;
        
        public MeshObject(double[,] vertex, int[,] edges, int[,] faces, double x, double y, double z,bool rot,bool face)
        {
            this.vertex = new double[vertex.GetLongLength(0), 3];
            for (int i = 0; i < vertex.GetLongLength(0); i++)
            {
                this.vertex[i, 0] = x + vertex[i, 0];
                this.vertex[i, 1] = y + vertex[i, 1];
                this.vertex[i, 2] = z + vertex[i, 2];
            }
            this.edges = edges;
            this.faces = faces;
            this.x = x;
            this.y = y;
            this.z = z;
            doRotate = rot;
            this.face = face;
            
        }
        public MeshObject(double[,] vertex, int[,] edges, int[,] faces) : this(vertex, edges,faces ,0, 0, 0,false,false)
        {
        }
        public void rescale(double scale)
        {
            for (int i = 0; i < vertex.GetLongLength(0); i++)
            {
                vertex[i, 0] *= scale;
                vertex[i, 1] *= scale;
                vertex[i, 2] *= scale;
            }
        }
        private double Sin(double angle)
        {
            return Math.Sin(angle * 0.01745329252);
        }
        private double Cos(double angle)
        {
            return Math.Cos(angle * 0.01745329252);
        }
        
        public void rotate(char axis, double angle)
        {
            double reverseX = 0, reverseY = 0;
            double[,] vert = new double[vertex.GetLongLength(0), 2];
            for (int i = 0; i < vertex.GetLongLength(0); i++)
            {
                if (axis == 'x')
                {
                    vert[i, 0] = vertex[i, 1];
                    vert[i, 1] = vertex[i, 2];
                    reverseX = y;
                    reverseY = z;
                }
                else if (axis == 'y')
                {
                    vert[i, 0] = vertex[i, 0];
                    vert[i, 1] = vertex[i, 2];
                    reverseX = x;
                    reverseY = z;
                }
                else if (axis == 'z')
                {
                    vert[i, 0] = vertex[i, 0];
                    vert[i, 1] = vertex[i, 1];
                    reverseX = x;
                    reverseY = y;
                }
            }
            for (int i = 0; i < vert.GetLongLength(0); i++)
            {
                double[] rot = { vert[i, 0] * Cos(angle) - vert[i, 1] * Sin(angle),
                                vert[i,1] * Cos(angle) + vert[i,0]*Sin(angle) };
                vert[i,0] = rot[0];
                vert[i,1] = rot[1];
            }

            for (int i = 0; i < vert.GetLongLength(0); i++)
            {
                if (axis == 'x')
                {
                    vertex[i,1] = vert[i,0];
                    vertex[i,2] = vert[i,1];
                }
                else if (axis == 'y')
                {
                    vertex[i,0] = vert[i,0];
                    vertex[i,2] = vert[i,1];
                }
                else if (axis == 'z')
                {
                    vertex[i,0] = vert[i,0];
                    vertex[i,1] = vert[i,1];
                }
            }
            
            double R = Math.Sqrt(reverseX*reverseX+reverseY*reverseY);
            double newAngle = Math.Atan2(reverseY, reverseX)*180/Math.PI;
            double newY = Sin(newAngle+angle)*R;
            double newX = Cos(newAngle+angle)*R;

            if (axis == 'x')
            {
                secretMove('y', reverseX - newX);
                secretMove('z', reverseY - newY);
            }
            else if (axis == 'y')
            {
                secretMove('x', reverseX-newX);
                secretMove('z', reverseY-newY);
            }
            else if (axis == 'z')
            {
                secretMove('x', reverseX - newX);
                secretMove('y', reverseY - newY);
            }

        }
        private void secretMove(char axis, double length)
        {
            int idx = 0;
            if (axis == 'y')
                idx = 1;
            else if (axis == 'z')
                idx = 2;
            for (int i = 0; i < vertex.GetLongLength(0); i++)
                vertex[i, idx] += length;
            
        }
        public void move(char axis, double length)
        {
            switch (axis)
            {
                case 'x':
                    for (int i = 0; i < vertex.GetLongLength(0); i++)
                        vertex[i, 0] += length;
                    x += length;
                    break;
                case 'y':
                    for (int i = 0; i < vertex.GetLongLength(0); i++)
                        vertex[i, 1] += length;
                    y+=length;
                    break;
                case 'z':
                    for (int i = 0; i < vertex.GetLongLength(0); i++)
                        vertex[i, 2] += length;
                    z+=length;
                    break;
            }
        
        }
    }
}

