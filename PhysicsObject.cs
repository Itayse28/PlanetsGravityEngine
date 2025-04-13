using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsTest
{
    internal class PhysicsObject : MeshObject
    {
        public double xforce, yforce, zforce, xspeed, yspeed, zspeed, weight;
        public bool passive;
        public LinkedList<PhysicsObject> collided;
        public LinkedList<double[]> path;
        public PhysicsObject(double[,] vertex, int[,] edges, int[,] faces, double x, double y, double z, bool rot, bool face,double xspeed, double yspeed, double zspeed, double weight, bool passive) : base(vertex, edges, faces, x, y, z, rot, face)
        {
            this.xforce = 0; //[m/s^2]
            this.yforce = 0; //[m/s^2]
            this.zforce = 0; //[m/s^2]
            this.xspeed = xspeed; //[m/s]
            this.yspeed = yspeed; //[m/s]
            this.zspeed = zspeed; //[m/s]
            this.weight = weight; //[kg]
            this.collided = new LinkedList<PhysicsObject>();
            this.passive = passive;
            path = new LinkedList<double[]>();
        }
        public PhysicsObject(PhysicsObject po) : this(po.vertex, po.edges, po.faces, po.x, po.y, po.z, false, po.face, po.xspeed, po.yspeed, po.zspeed, po.weight, po.passive)
        {
        }

    }
}
