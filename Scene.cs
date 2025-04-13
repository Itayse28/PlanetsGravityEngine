using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ManagedCuda;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Text.Json;
using System.Net;

namespace GraphicsTest
{
    internal class Scene
    {
        int TimeLimit = 500;
        public decimal renderDistance = 100;
        public double lightX=0,lightY=0,lightZ=0;//basicly the camera position, the further the face that being drawn from these coords, the more transparent it is
        Bitmap gPrevBitmap;//used to fix fliccering effect, save the previous frame and redraw it when fliccer
        int start;
        bool undraw = false;
        public Pen pen;
        public BufferedGraphics bufferedGraphics;
        public List<MeshObject> objects;
        public Camera cam;
        public int prevx, prevy;
        public Form form;
        public int calcT = 0, drawT = 0, renderT = 0;
        public double step = 20;
        public int physicsCollisionCount=0;
        public int[] addPointToPath=new int[0];
        public LinkedList<LinkedList<double[]>> futurePaths=new LinkedList<LinkedList<double[]>>();
        public Scene(Form form)
        {
            this.form = form;
            bufferedGraphics = BufferedGraphicsManager.Current.Allocate(
                form.CreateGraphics(), form.ClientRectangle);
            pen = new Pen(Color.White, 2);
            objects = new List<MeshObject>();
            cam = new Camera(0,-500,-1200);
            cam.DSx = form.Size.Width / 2;
            cam.DSy = form.Size.Height / 2;
            gPrevBitmap=new Bitmap(form.Size.Width, form.Size.Height);
        }
        public void drawFuture(int next)
        {
            futurePaths.Clear();
            int physicsObjectsCount = 0;
            foreach(MeshObject obje in objects)
                if(obje is PhysicsObject)
                    physicsObjectsCount++;
            PhysicsObject[] physicsObjects = new PhysicsObject[physicsObjectsCount];
            int physicsCollisionCountFuture = 0;
            

            int counter = 0;
            for(int i=0;i<objects.Count;i++)
                if(objects[i] is PhysicsObject)
                    physicsObjects[counter++] = new PhysicsObject((PhysicsObject)objects[i]);

            int[] addPointToPathFuture = new int[physicsObjects.Length];
            for (int n = 0; n < next; n++)
            {
                
                for (int i = 0; i < physicsObjects.Length; i++)
                {
                    PhysicsObject obj=physicsObjects[i];
                    obj.xspeed += obj.xforce / obj.weight;
                    obj.yspeed += obj.yforce / obj.weight;
                    obj.zspeed += obj.zforce / obj.weight;
                    obj.move('x', obj.xspeed);
                    obj.move('y', obj.yspeed);
                    obj.move('z', obj.zspeed);
                    obj.xforce = 0;
                    obj.yforce = 0;
                    obj.zforce = 0;
                    if (addPointToPathFuture[i] == 20)
                    {
                        double[] coords = { obj.x, obj.y, obj.z };
                        physicsObjects[i].path.AddLast(coords);
                        addPointToPathFuture[i] = 0;
                    }
                    else
                        addPointToPathFuture[i]++;
                    for (int j = 0; j < physicsObjects.Length; j++)
                    {
                        if (i != j)
                        {
                            PhysicsObject obj2 = new PhysicsObject(physicsObjects[j]);
                            double v1x = obj.xspeed;
                            double v1y = obj.yspeed;
                            double v1z = obj.zspeed;
                            double v2x = obj2.xspeed;
                            double v2y = obj2.yspeed;
                            double v2z = obj2.zspeed;
                            double m1 = obj.weight;
                            double m2 = obj2.weight;
                            double r = Math.Sqrt((obj2.x - obj.x) * (obj2.x - obj.x) + (obj2.y - obj.y) * (obj2.y - obj.y) + (obj2.z - obj.z) * (obj2.z - obj.z));
                            if (r < 150 && (!obj.collided.Contains(obj2) || !obj2.collided.Contains(obj)))
                            {
                                obj.move('x', -v1x);
                                obj.move('y', -v1y);
                                obj.move('z', -v1z);
                                if (!obj.passive)
                                {
                                    obj.xspeed = ((m1 - m2) * v1x + 2 * m2 * v2x) / (m1 + m2);
                                    obj.yspeed = ((m1 - m2) * v1y + 2 * m2 * v2y) / (m1 + m2);
                                    obj.zspeed = ((m1 - m2) * v1z + 2 * m2 * v2z) / (m1 + m2);
                                }
                                if (!obj2.passive)
                                {
                                    obj2.xspeed = ((m2 - m1) * v2x + 2 * m1 * v1x) / (m1 + m2);
                                    obj2.yspeed = ((m2 - m1) * v2y + 2 * m1 * v1y) / (m1 + m2);
                                    obj2.zspeed = ((m2 - m1) * v2z + 2 * m1 * v1z) / (m1 + m2);
                                }
                                obj.collided.AddLast(obj2);
                                obj2.collided.AddLast(obj);
                            }

                            double G = 6.67430e-11 * 10000000; //add a factor to G
                            double F = (G * m1 * m2) / (r * r);
                            if (!obj.passive)
                            {
                                obj.xforce += F * (obj2.x - obj.x) / r;
                                obj.yforce += F * (obj2.y - obj.y) / r;
                                obj.zforce += F * (obj2.z - obj.z) / r;
                            }
                        }
                    }

                }
                if (physicsCollisionCountFuture == 5)
                {
                    for (int i = 0; i < physicsObjects.Length; i++)
                        if (physicsObjects[i]!=null)
                            (physicsObjects[i]).collided.Clear();
                }
                else
                    physicsCollisionCountFuture++;
            }
            
            for(int i = 0; i < physicsObjects.GetLength(0); i++)
            {
                if(physicsObjects[i] != null)
                {
                    futurePaths.AddLast(physicsObjects[i].path);
                }
            }

        }
        public void addPhysicsObject(String name, double x, double y, double z, bool rotate, bool face, double xspeed, double yspeed, double zspeed, double weight, bool passive)
        {
            int[] addPointToPathTemp=new int[addPointToPath.Length+1];
            for (int i = 0; i < addPointToPath.Length; i++)
                addPointToPathTemp[i] = addPointToPath[i];
            addPointToPath = addPointToPathTemp;
            switch (name.ToLower())
            {
                case ("cube"):
                    objects.Add(new PhysicsObject(Models.cubeVertex, Models.cubeEdges, Models.cubeFaces, x, y, z, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    break;
                case ("plane"):
                    objects.Add(new PhysicsObject(Models.planeVertex, Models.planeEdges, Models.planeFaces, x, y, z, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    break;
                case ("pyramid"):
                    objects.Add(new PhysicsObject(Models.pyramidVertex, Models.pyramidEdges, Models.pyramidFaces, x, y, z, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    break;
                case ("gear"):
                    objects.Add(new PhysicsObject(Models.gearVertex, Models.gearEdges, Models.gearFaces, 0, 0, 0, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    objects[objects.Count - 1].rescale(60);
                    objects[objects.Count - 1].move('x', cam.x);
                    objects[objects.Count - 1].move('y', cam.y);
                    objects[objects.Count - 1].move('z', cam.z);
                    break;
                case ("sphere"):
                    objects.Add(new PhysicsObject(Models.sphereVertex, Models.sphereEdges, Models.sphereFaces, 0, 0, 0, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(80);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("torus"):
                    objects.Add(new PhysicsObject(Models.torusVertex, Models.torusEdges, Models.torusFaces, 0, 0, 0, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(30);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("candle"):
                    objects.Add(new PhysicsObject(Models.candleVertex, Models.candleEdges, Models.candleFaces, 0, 0, 0, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    objects[objects.Count - 1].rotate('x', 180);
                    objects[objects.Count - 1].rescale(30);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("pistol"):
                    objects.Add(new PhysicsObject(Models.pistolVertex, Models.pistolEdges, Models.pistolFaces, 0, 0, 0, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    objects[objects.Count - 1].rotate('x', 180);
                    objects[objects.Count - 1].rescale(60);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("hand"):
                    objects.Add(new PhysicsObject(Models.handVertex, Models.handEdges, Models.handFaces, 0, 0, 0, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(60);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("damca"):
                    objects.Add(new PhysicsObject(Models.damcaVertex, Models.damcaEdges, Models.damcaFaces, 0, 0, 0, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(80);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("itay"):
                    objects.Add(new PhysicsObject(Models.ITAYVertex, Models.ITAYEdges, Models.ITAYFaces, 0, 0, 0, rotate, face, xspeed, yspeed, zspeed, weight, passive));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(80);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
            }
            
        }
        public void addObject(String name, double x, double y, double z,bool rotate,bool face)
        {
            switch (name.ToLower())
            {
                case ("cube"):
                    objects.Add(new MeshObject(Models.cubeVertex, Models.cubeEdges,Models.cubeFaces, x, y, z, rotate,face));
                    break;
                case ("plane"):
                    objects.Add(new MeshObject(Models.planeVertex, Models.planeEdges, Models.planeFaces, x, y, z, rotate, face));
                    break;
                case ("pyramid"):
                    objects.Add(new MeshObject(Models.pyramidVertex, Models.pyramidEdges, Models.pyramidFaces, x, y, z, rotate, face));
                    break;
                case ("gear"):
                    objects.Add(new MeshObject(Models.gearVertex, Models.gearEdges, Models.gearFaces, 0, 0, 0, rotate, face));
                    objects[objects.Count - 1].rescale(60);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("sphere"):
                    objects.Add(new MeshObject(Models.sphereVertex, Models.sphereEdges, Models.sphereFaces, 0, 0, 0, rotate, face));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(80);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("torus"):
                    objects.Add(new MeshObject(Models.torusVertex, Models.torusEdges, Models.torusFaces, 0, 0, 0, rotate, face));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(30);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("candle"):
                    objects.Add(new MeshObject(Models.candleVertex, Models.candleEdges, Models.candleFaces, 0, 0, 0, rotate, face));
                    objects[objects.Count - 1].rotate('x', 180);
                    objects[objects.Count - 1].rescale(30);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("pistol"):
                    objects.Add(new MeshObject(Models.pistolVertex, Models.pistolEdges, Models.pistolFaces, 0, 0, 0, rotate, face));
                    objects[objects.Count - 1].rotate('x', 180);
                    objects[objects.Count - 1].rescale(60);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("hand"):
                    objects.Add(new MeshObject(Models.handVertex, Models.handEdges, Models.handFaces, 0, 0, 0, rotate, face));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(60);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("damca"):
                    objects.Add(new MeshObject(Models.damcaVertex, Models.damcaEdges, Models.damcaFaces, 0, 0, 0, rotate, face));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(80);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
                case ("itay"):
                    objects.Add(new MeshObject(Models.ITAYVertex, Models.ITAYEdges, Models.ITAYFaces, 0, 0, 0, rotate, face));
                    objects[objects.Count - 1].rotate('x', 90);
                    objects[objects.Count - 1].rescale(80);
                    objects[objects.Count - 1].move('x', x);
                    objects[objects.Count - 1].move('y', y);
                    objects[objects.Count - 1].move('z', z);
                    break;
            }
        }
        public void addObject(String name)
        {
            addObject(name, 0, 0, 0,false,false);
        }
        public async void update()
        {
            start = DateTime.Now.Millisecond;

            lightX = cam.x;
            lightY = cam.y;
            lightZ = cam.z;

            form.DrawToBitmap(gPrevBitmap,new Rectangle(0,0,form.Width,form.Height));
            bufferedGraphics.Graphics.Clear(Color.Black);
            bufferedGraphics.Graphics.DrawLine(Pens.Red, form.Size.Width / 2 + 10, form.Size.Height / 2 + 10, form.Size.Width / 2 - 10, form.Size.Height / 2 - 10);
            bufferedGraphics.Graphics.DrawLine(Pens.Red, form.Size.Width / 2 - 10, form.Size.Height / 2 + 10, form.Size.Width / 2 + 10, form.Size.Height / 2 - 10);

            int s=DateTime.Now.Millisecond;
            
            for (int i=0;i<objects.Count;i++)
            {
                if (objects[i] is PhysicsObject)
                {
                    PhysicsObject obj = (PhysicsObject)objects[i];
                    obj.xspeed += obj.xforce / obj.weight;
                    obj.yspeed += obj.yforce / obj.weight;
                    obj.zspeed += obj.zforce / obj.weight;
                    obj.move('x', obj.xspeed);
                    obj.move('y', obj.yspeed);
                    obj.move('z', obj.zspeed);
                    obj.xforce = 0;
                    obj.yforce = 0;
                    obj.zforce = 0;
                    if (addPointToPath[i] == 20)
                    {
                        double[] coords = { obj.x, obj.y, obj.z };
                        obj.path.AddLast(coords);
                        addPointToPath[i] = 0;
                    }
                    else
                        addPointToPath[i]++;
                    for (int j = 0; j < objects.Count; j++)
                    {
                        if (i != j && objects[j] is PhysicsObject)
                        {
                            PhysicsObject obj2 = (PhysicsObject)objects[j];
                            double v1x = obj.xspeed;
                            double v1y = obj.yspeed;
                            double v1z = obj.zspeed;
                            double v2x = obj2.xspeed;
                            double v2y = obj2.yspeed;
                            double v2z = obj2.zspeed;
                            double m1 = obj.weight;
                            double m2 = obj2.weight;
                            double r = Math.Sqrt((obj2.x - obj.x) * (obj2.x - obj.x) + (obj2.y - obj.y) * (obj2.y - obj.y) + (obj2.z - obj.z) * (obj2.z - obj.z));
                            if (r < 150 && (!obj.collided.Contains(obj2) || !obj2.collided.Contains(obj)))
                            {
                                obj.move('x', -v1x);
                                obj.move('y', -v1y);
                                obj.move('z', -v1z);
                                if (!obj.passive)
                                {
                                    obj.xspeed = ((m1 - m2) * v1x + 2 * m2 * v2x) / (m1 + m2);
                                    obj.yspeed = ((m1 - m2) * v1y + 2 * m2 * v2y) / (m1 + m2);
                                    obj.zspeed = ((m1 - m2) * v1z + 2 * m2 * v2z) / (m1 + m2);
                                }
                                if (!obj2.passive)
                                {
                                    obj2.xspeed = ((m2 - m1) * v2x + 2 * m1 * v1x) / (m1 + m2);
                                    obj2.yspeed = ((m2 - m1) * v2y + 2 * m1 * v1y) / (m1 + m2);
                                    obj2.zspeed = ((m2 - m1) * v2z + 2 * m1 * v1z) / (m1 + m2);
                                }
                                obj.collided.AddLast(obj2);
                                obj2.collided.AddLast(obj);
                            }
                            
                            double G =6.67430e-11* 10000000; //add a factor to G
                            double F = (G*m1*m2) / (r * r);
                            if (!obj.passive)
                            {
                                obj.xforce += F * (obj2.x - obj.x) / r;
                                obj.yforce += F * (obj2.y - obj.y) / r;
                                obj.zforce += F * (obj2.z - obj.z) / r;
                            }
                            
                        }
                    }

                }
                
            }
            
            if (physicsCollisionCount == 5)
            {
                for (int i = 0; i < objects.Count; i++)
                    if (objects[i] is PhysicsObject)
                        ((PhysicsObject)objects[i]).collided.Clear();
            }
            else
                physicsCollisionCount++;
            calcT = DateTime.Now.Millisecond - s;
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < objects.Count; i++)
            {
                int idx = i;
                tasks.Add(drawObject(objects[idx]));
                if (objects[idx] is PhysicsObject) {
                    Pen p = new Pen(Color.FromArgb((new Random(idx + 5)).Next(256),
                    (new Random(idx * idx + 2)).Next(256), (new Random(idx * idx * idx + 4)).Next(256),
                    (new Random(idx * idx * idx * idx + 7)).Next(256)),2);
                    tasks.Add(drawPath(((PhysicsObject)objects[idx]).path,p));
                }
            }
            await Task.WhenAll(tasks.ToArray());
            tasks.Clear();
            for (int i= 0;i < futurePaths.Count;i++)
            {
                int idx = i;
                Pen p = new Pen(Color.FromArgb(255,
                    (new Random(idx)).Next(256), (new Random(idx*idx)).Next(256),
                    (new Random(idx*idx*idx)).Next(256)), 2);
                tasks.Add(drawPath(futurePaths.ElementAt(idx), p));
            }
            await Task.WhenAll(tasks.ToArray());
            if (undraw)
            {
                using (Graphics bitmapG = Graphics.FromImage(gPrevBitmap))
                    bitmapG.DrawImage(gPrevBitmap, new Point(0,0));
                undraw=false;
            }
            else
                bufferedGraphics.Render();

            renderT = DateTime.Now.Millisecond - s;
            
        }
        
        private Task drawObject(MeshObject obj)
        {
            if (obj == null) return Task.CompletedTask;
            if (obj.doRotate)
                obj.rotate('y', 1);
            int s = DateTime.Now.Millisecond;
            double[,] points = cam.project(obj.vertex);
            calcT+=DateTime.Now.Millisecond-s;
            s = DateTime.Now.Millisecond;
            Random rnd = new Random(obj.GetHashCode());
            int colorHueR = rnd.Next(256);
            int colorHueG = rnd.Next(256);
            int colorHueB = rnd.Next(256);
            int addR = 1;
            int addG = 1;
            int addB = 1;
            try
            {
                if (obj.face)
                    for (int i = 0; i < obj.faces.GetLongLength(0); i++)
                    {
                        int now = DateTime.Now.Millisecond;
                        if (now - start > TimeLimit || now - start < -TimeLimit)
                        {
                            undraw=true;
                            break;
                        }
                        int x1 = (int)Math.Round(points[obj.faces[i, 0], 0]);
                        int y1 = (int)Math.Round(points[obj.faces[i, 0], 1]);
                        int x2 = (int)Math.Round(points[obj.faces[i, 1], 0]);
                        int y2 = (int)Math.Round(points[obj.faces[i, 1], 1]);
                        int x3 = (int)Math.Round(points[obj.faces[i, 2], 0]);
                        int y3 = (int)Math.Round(points[obj.faces[i, 2], 1]);

                        //if(Area Of Triangle < TriangleLimit)
                        if (Math.Abs(x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2 < 300/renderDistance)
                            continue;
                        List<Point> p = new List<Point>();
                        if (x1 > 0 && y1 > 0)
                            p.Add(new Point(x1, y1));
                        if (x2 > 0 && y2 > 0)
                            p.Add(new Point(x2, y2));
                        if (x3 > 0 && y3 > 0)
                            p.Add(new Point(x3, y3));

                        
                        double alpha =Math.Sqrt(Math.Pow(obj.vertex[i, 0] - lightX, 2)+ Math.Pow(obj.vertex[i, 1] - lightY, 2)+Math.Pow(obj.vertex[i, 2] - lightZ, 2));
                        alpha = alpha / (double)renderDistance;
                        alpha = 255 - alpha;
                        if (alpha < 0)
                            alpha = 0;
                        
                        if (colorHueR >= 255)
                            addR = -1;
                        if (colorHueG >= 255)
                            addG = -1;
                        if (colorHueB >= 255)
                            addB = -1;
                        if (colorHueR <= 0)
                            addR = 1;
                        if (colorHueG <= 0)
                            addG = 1;
                        if (colorHueB <= 0)
                            addB = 1;
                        Brush b = new SolidBrush(Color.FromArgb((int)alpha, colorHueR, colorHueG, colorHueB));
                        colorHueR += addR;
                        colorHueG += addG;
                        colorHueB += addB;
                        bufferedGraphics.Graphics.FillPolygon(b, p.ToArray());
                    }
                else
                {
                    for (int i = 0; i < obj.edges.GetLongLength(0); i++)
                    {
                        int now = DateTime.Now.Millisecond;
                        if (now - start > TimeLimit || now - start < -TimeLimit)
                        {
                            undraw = true;
                            break;
                        }
                        int x1 = (int)Math.Round(points[obj.edges[i, 0], 0]);
                        int y1 = (int)Math.Round(points[obj.edges[i, 0], 1]);
                        int x2 = (int)Math.Round(points[obj.edges[i, 1], 0]);
                        int y2 = (int)Math.Round(points[obj.edges[i, 1], 1]);
                        if ((x1 < 0 || x2 < 0 || y1 < 0 || y2 < 0) || 
                            (Math.Sqrt(Math.Abs(x1 - x2)*Math.Abs(x1-x2)+ Math.Abs(y1 - y2)* Math.Abs(y1 - y2))<100/(double)renderDistance))
                            continue;
                        bufferedGraphics.Graphics.DrawLine(pen, x1, y1, x2, y2);
                    }

                }
            }
            catch (Exception e)
            {
                return Task.CompletedTask;
            }
            drawT += DateTime.Now.Millisecond - s;
            return Task.CompletedTask;
        }
        public Task drawPath(LinkedList<double[]> path,Pen pen)
        {
            int start = DateTime.Now.Millisecond;
            try
            {
                double[,] projectedPath=new double[path.Count,2];
                for (int i = 0; i < path.Count; i++)
                {
                    double[] point = cam.project(path.ElementAt(i));
                    projectedPath[i, 0] = point[0];
                    projectedPath[i, 1] = point[1];
                }
            
                for (int i = 0; i < projectedPath.GetLength(0)-2; i++)
                {
                    int now = DateTime.Now.Millisecond;
                    if (now - start > TimeLimit || now - start < -TimeLimit)
                    {
                        undraw = true;
                        break;
                    }
                    int x1 = (int)Math.Round(projectedPath[i,0]);
                    int y1 = (int)Math.Round(projectedPath[i, 1]);
                    int x2 = (int)Math.Round(projectedPath[i+1, 0]);
                    int y2 = (int)Math.Round(projectedPath[i+1, 1]);
                    if ((x1 < 0 || x2 < 0 || y1 < 0 || y2 < 0) ||
                        (Math.Sqrt(Math.Abs(x1 - x2) * Math.Abs(x1 - x2) + Math.Abs(y1 - y2) * Math.Abs(y1 - y2)) < 100 / (double)renderDistance))
                        continue;
                    bufferedGraphics.Graphics.DrawLine(pen, x1, y1, x2, y2);
                }
            }
            catch(Exception e)
            {
                return Task.CompletedTask;
            }
            drawT += DateTime.Now.Millisecond - start;
            return Task.CompletedTask;
        }
        public void formResize(Form form)
        {
            this.form = form;
            cam.DSx = form.Size.Width / 2;
            cam.DSy = form.Size.Height / 2;
            bufferedGraphics = BufferedGraphicsManager.Current.Allocate(
                form.CreateGraphics(), form.ClientRectangle);
        }
        public void mouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cam.rotate('y', (prevx - e.X) / 3);
                cam.rotate('x', (prevy - e.Y) / 3);
                if (cam.xrot > 90)
                    cam.rotate('x', -2);
                else if (cam.xrot < -90)
                    cam.rotate('x', 2);
            }
            
            prevx = e.X;
            prevy = e.Y;
        }
        public void keyDown(KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                cam.forward(-step);
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                cam.forward(step);
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                cam.sideways(-step);
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                cam.sideways(step);
            if (e.KeyCode == Keys.E)
                cam.y -= step;
            if (e.KeyCode == Keys.Q)
                cam.y += step;
            
        }
    }
}
