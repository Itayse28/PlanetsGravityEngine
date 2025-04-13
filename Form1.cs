using Microsoft.VisualBasic.Devices;
using System.CodeDom;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GraphicsTest
{
    public partial class Form1 : Form
    {

        Scene scene;
        public Form1()
        {
            InitializeComponent();
            scene = new Scene(this);
            
            this.MouseWheel += Form1_MouseWheel;
        }
        public void preset()
        {
            Random random = new Random();
            int num = random.Next(3);
            switch (num)
            {
                case (0):
                    scene.addPhysicsObject("sphere", 0, 0, 0, doRotate.Checked, showFaces.Checked,
                            0, 0, 2, 40000000, false);
                    scene.addPhysicsObject("sphere", 700, 1400, 0, doRotate.Checked, showFaces.Checked,
                            -4, 0, 0, 20, false);
                    break;
                case (1):
                    scene.addPhysicsObject("sphere", 0, -500, 0, doRotate.Checked, showFaces.Checked,
                            0, 0, 0, 4000000, true);
                    scene.addPhysicsObject("sphere", 0, 500, 0, doRotate.Checked, showFaces.Checked,
                            0, 0, 0, 4000000, true);
                    scene.addPhysicsObject("sphere", 0, 0, 0, doRotate.Checked, showFaces.Checked,
                            2.7, 1.9, 0, 20, false);
                    break;
                case (2):
                    scene.addPhysicsObject("sphere", 600, -600, -100, doRotate.Checked, showFaces.Checked,
                            -6, 0, 0, 20000000, false);
                    scene.addPhysicsObject("sphere", -400, 200, 0, doRotate.Checked, showFaces.Checked,
                            0, 0, 0, 20000000, false);
                    scene.addPhysicsObject("sphere", -400, -400, 300, doRotate.Checked, showFaces.Checked,
                            0, 0, 3, 20000000, false);
                    break;
            }
        }
        private void RenderLoop_Tick(object sender, EventArgs e)
        {
            
            scene.calcT = 0;
            scene.drawT = 0;
            scene.renderT = 0;
            int start = DateTime.Now.Millisecond;
            
            string shortx= scene.cam.x.ToString(), shorty= scene.cam.y.ToString(), shortz= scene.cam.z.ToString();
            if (scene.cam.x != (int)scene.cam.x)
                shortx = shortx.Substring(0, shortx.IndexOf('.') + 3);
            if (scene.cam.y != (int)scene.cam.y)
                shorty = shorty.Substring(0, shorty.IndexOf('.') + 3);
            if (scene.cam.z != Math.Round(scene.cam.z))
                shortz = shortz.Substring(0, shortz.IndexOf('.') + 3);
            label1.Text = shortx + ", " + shorty + ", " + shortz;
            
            scene.update();
            label2.Text = "Calc: " + scene.calcT + " Draw: " + scene.drawT + " Render: " + scene.renderT;

            int end = DateTime.Now.Millisecond;
            if (end == start)
                return;

            FPS.Text = "FPS: "+(1000/(end-start));
            

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            scene.formResize(this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            scene.keyDown(e);
            if (e.KeyCode == Keys.Enter)
                if (Item.SelectedItem != null)
                    scene.addObject(Item.SelectedItem.ToString(), scene.cam.x, scene.cam.y, scene.cam.z,doRotate.Checked,showFaces.Checked);
        }

        private void addModel_Click(object sender, EventArgs e)
        {
            if (Item.SelectedItem != null)
            {
                if (Physics.Checked)
                {
                    scene.addPhysicsObject(Item.SelectedItem.ToString(), scene.cam.x, scene.cam.y, scene.cam.z, doRotate.Checked, showFaces.Checked,
                    (double)physicsSpeedX.Value, (double)physicsSpeedY.Value, (double)physicsSpeedZ.Value, (double)(MassBar.Value+1)*200000, PassiveCheckBox.Checked);
                    scene.drawFuture(9000);
                }
                else
                    scene.addObject(Item.SelectedItem.ToString(), scene.cam.x, scene.cam.y, scene.cam.z, doRotate.Checked, showFaces.Checked);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            scene.mouseMove(e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            scene.renderDistance = nud.Value;
        }
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            scene.step += e.Delta/120;
            if(scene.step < 1)
                scene.step = 1;
        }

        private void Physics_CheckedChanged(object sender, EventArgs e)
        {
            if (Physics.Checked)
            {
                physicsSpeedX.Visible = true;
                physicsSpeedY.Visible = true;
                physicsSpeedZ.Visible = true;
                speedLabel.Visible = true;
                PassiveCheckBox.Visible = true;
                MassBar.Visible = true;
                MassBarLabel.Visible = true;
            }
            else
            {
                physicsSpeedX.Visible = false;
                physicsSpeedY.Visible = false;
                physicsSpeedZ.Visible = false;
                speedLabel.Visible = false;
                PassiveCheckBox.Visible = false;
                MassBar.Visible = false;
                MassBarLabel.Visible = false;
            }
        }

        private void PresetButton_Click(object sender, EventArgs e)
        {
            this.preset();
            PresetButton.Visible = false;
            PresetButton.Enabled = false;
        }

        private void FutureButton_Click(object sender, EventArgs e)
        {
            scene.drawFuture(9000);
        }

        private void ClearFutureButton_Click(object sender, EventArgs e)
        {
            scene.futurePaths.Clear();
        }
    }
}

/*  5.10.2024
 * 
 * 
 * Y+ is down
 * X+ is right
 * Z+ is forward
 * -------->x
 * |⟍
 * |  ⟍
 * |    ⟍
 * V      🡮
 * Y        Z
 * 
 * 
 * 
 * 
 *  this is an extended version to a graphics engine that i wrote in 2021
 *  the physics here are very basic because i coded it in 3 hours at a tuesday night
 * 
 * 
 * 
 */












