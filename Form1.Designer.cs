namespace GraphicsTest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RenderLoop = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FPS = new System.Windows.Forms.Label();
            this.Item = new System.Windows.Forms.ComboBox();
            this.addModel = new System.Windows.Forms.Button();
            this.doRotate = new System.Windows.Forms.CheckBox();
            this.showFaces = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Physics = new System.Windows.Forms.CheckBox();
            this.physicsSpeedZ = new System.Windows.Forms.NumericUpDown();
            this.physicsSpeedY = new System.Windows.Forms.NumericUpDown();
            this.physicsSpeedX = new System.Windows.Forms.NumericUpDown();
            this.RenderDistanceLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.ShapeLabel = new System.Windows.Forms.Label();
            this.PassiveCheckBox = new System.Windows.Forms.CheckBox();
            this.PresetButton = new System.Windows.Forms.Button();
            this.MassBarLabel = new System.Windows.Forms.Label();
            this.MassBar = new System.Windows.Forms.NumericUpDown();
            this.FutureButton = new System.Windows.Forms.Button();
            this.ClearFutureButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.physicsSpeedZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.physicsSpeedY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.physicsSpeedX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MassBar)).BeginInit();
            this.SuspendLayout();
            // 
            // RenderLoop
            // 
            this.RenderLoop.Enabled = true;
            this.RenderLoop.Interval = 1;
            this.RenderLoop.Tick += new System.EventHandler(this.RenderLoop_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1196, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1196, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 1;
            // 
            // FPS
            // 
            this.FPS.AutoSize = true;
            this.FPS.ForeColor = System.Drawing.Color.White;
            this.FPS.Location = new System.Drawing.Point(33, 14);
            this.FPS.Name = "FPS";
            this.FPS.Size = new System.Drawing.Size(38, 15);
            this.FPS.TabIndex = 2;
            this.FPS.Text = "FPS: 0";
            // 
            // Item
            // 
            this.Item.CausesValidation = false;
            this.Item.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Item.FormattingEnabled = true;
            this.Item.Items.AddRange(new object[] {
            "Cube",
            "Plane",
            "Pyramid",
            "Sphere",
            "Gear",
            "Torus",
            "Candle",
            "Pistol",
            "Hand",
            "Damca",
            "ITAY"});
            this.Item.Location = new System.Drawing.Point(1289, 85);
            this.Item.Name = "Item";
            this.Item.Size = new System.Drawing.Size(121, 23);
            this.Item.TabIndex = 3;
            // 
            // addModel
            // 
            this.addModel.Location = new System.Drawing.Point(1310, 198);
            this.addModel.Name = "addModel";
            this.addModel.Size = new System.Drawing.Size(75, 23);
            this.addModel.TabIndex = 4;
            this.addModel.Text = "Add Model";
            this.addModel.UseVisualStyleBackColor = true;
            this.addModel.Click += new System.EventHandler(this.addModel_Click);
            // 
            // doRotate
            // 
            this.doRotate.AutoSize = true;
            this.doRotate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.doRotate.ForeColor = System.Drawing.Color.White;
            this.doRotate.Location = new System.Drawing.Point(1310, 118);
            this.doRotate.Name = "doRotate";
            this.doRotate.Size = new System.Drawing.Size(66, 19);
            this.doRotate.TabIndex = 5;
            this.doRotate.Text = "Spining";
            this.doRotate.UseVisualStyleBackColor = true;
            // 
            // showFaces
            // 
            this.showFaces.AutoSize = true;
            this.showFaces.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showFaces.ForeColor = System.Drawing.Color.White;
            this.showFaces.Location = new System.Drawing.Point(1310, 143);
            this.showFaces.Name = "showFaces";
            this.showFaces.Size = new System.Drawing.Size(87, 19);
            this.showFaces.TabIndex = 6;
            this.showFaces.Text = "Show Faces";
            this.showFaces.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(109, 37);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(91, 23);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1196, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 8;
            // 
            // Physics
            // 
            this.Physics.AutoSize = true;
            this.Physics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Physics.ForeColor = System.Drawing.Color.White;
            this.Physics.Location = new System.Drawing.Point(1310, 168);
            this.Physics.Name = "Physics";
            this.Physics.Size = new System.Drawing.Size(65, 19);
            this.Physics.TabIndex = 9;
            this.Physics.Text = "Physics";
            this.Physics.UseVisualStyleBackColor = true;
            this.Physics.CheckedChanged += new System.EventHandler(this.Physics_CheckedChanged);
            // 
            // physicsSpeedZ
            // 
            this.physicsSpeedZ.Location = new System.Drawing.Point(1338, 244);
            this.physicsSpeedZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.physicsSpeedZ.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.physicsSpeedZ.Name = "physicsSpeedZ";
            this.physicsSpeedZ.Size = new System.Drawing.Size(47, 23);
            this.physicsSpeedZ.TabIndex = 10;
            this.physicsSpeedZ.Visible = false;
            // 
            // physicsSpeedY
            // 
            this.physicsSpeedY.Location = new System.Drawing.Point(1285, 244);
            this.physicsSpeedY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.physicsSpeedY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.physicsSpeedY.Name = "physicsSpeedY";
            this.physicsSpeedY.Size = new System.Drawing.Size(47, 23);
            this.physicsSpeedY.TabIndex = 11;
            this.physicsSpeedY.Visible = false;
            // 
            // physicsSpeedX
            // 
            this.physicsSpeedX.Location = new System.Drawing.Point(1232, 244);
            this.physicsSpeedX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.physicsSpeedX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.physicsSpeedX.Name = "physicsSpeedX";
            this.physicsSpeedX.Size = new System.Drawing.Size(47, 23);
            this.physicsSpeedX.TabIndex = 12;
            this.physicsSpeedX.Visible = false;
            // 
            // RenderDistanceLabel
            // 
            this.RenderDistanceLabel.AutoSize = true;
            this.RenderDistanceLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.RenderDistanceLabel.Location = new System.Drawing.Point(8, 39);
            this.RenderDistanceLabel.Name = "RenderDistanceLabel";
            this.RenderDistanceLabel.Size = new System.Drawing.Size(95, 15);
            this.RenderDistanceLabel.TabIndex = 13;
            this.RenderDistanceLabel.Text = "Render Distance:";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.speedLabel.Location = new System.Drawing.Point(1196, 226);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(170, 15);
            this.speedLabel.TabIndex = 14;
            this.speedLabel.Text = "speed:     X                Y               Z";
            this.speedLabel.Visible = false;
            // 
            // ShapeLabel
            // 
            this.ShapeLabel.AutoSize = true;
            this.ShapeLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ShapeLabel.Location = new System.Drawing.Point(1241, 85);
            this.ShapeLabel.Name = "ShapeLabel";
            this.ShapeLabel.Size = new System.Drawing.Size(42, 15);
            this.ShapeLabel.TabIndex = 15;
            this.ShapeLabel.Text = "Shape:";
            // 
            // PassiveCheckBox
            // 
            this.PassiveCheckBox.AutoSize = true;
            this.PassiveCheckBox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PassiveCheckBox.Location = new System.Drawing.Point(1232, 273);
            this.PassiveCheckBox.Name = "PassiveCheckBox";
            this.PassiveCheckBox.Size = new System.Drawing.Size(64, 19);
            this.PassiveCheckBox.TabIndex = 16;
            this.PassiveCheckBox.Text = "Passive";
            this.PassiveCheckBox.UseVisualStyleBackColor = true;
            this.PassiveCheckBox.Visible = false;
            // 
            // PresetButton
            // 
            this.PresetButton.Location = new System.Drawing.Point(33, 102);
            this.PresetButton.Name = "PresetButton";
            this.PresetButton.Size = new System.Drawing.Size(75, 23);
            this.PresetButton.TabIndex = 17;
            this.PresetButton.Text = "Preset";
            this.PresetButton.UseVisualStyleBackColor = true;
            this.PresetButton.Click += new System.EventHandler(this.PresetButton_Click);
            // 
            // MassBarLabel
            // 
            this.MassBarLabel.AutoSize = true;
            this.MassBarLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MassBarLabel.Location = new System.Drawing.Point(1232, 302);
            this.MassBarLabel.Name = "MassBarLabel";
            this.MassBarLabel.Size = new System.Drawing.Size(34, 15);
            this.MassBarLabel.TabIndex = 19;
            this.MassBarLabel.Text = "Mass";
            this.MassBarLabel.Visible = false;
            // 
            // MassBar
            // 
            this.MassBar.Location = new System.Drawing.Point(1272, 300);
            this.MassBar.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.MassBar.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MassBar.Name = "MassBar";
            this.MassBar.Size = new System.Drawing.Size(120, 23);
            this.MassBar.TabIndex = 20;
            this.MassBar.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MassBar.Visible = false;
            // 
            // FutureButton
            // 
            this.FutureButton.Location = new System.Drawing.Point(33, 66);
            this.FutureButton.Name = "FutureButton";
            this.FutureButton.Size = new System.Drawing.Size(90, 23);
            this.FutureButton.TabIndex = 21;
            this.FutureButton.Text = "Watch Future";
            this.FutureButton.UseVisualStyleBackColor = true;
            this.FutureButton.Click += new System.EventHandler(this.FutureButton_Click);
            // 
            // ClearFutureButton
            // 
            this.ClearFutureButton.Location = new System.Drawing.Point(129, 66);
            this.ClearFutureButton.Name = "ClearFutureButton";
            this.ClearFutureButton.Size = new System.Drawing.Size(80, 23);
            this.ClearFutureButton.TabIndex = 22;
            this.ClearFutureButton.Text = "Clear Future";
            this.ClearFutureButton.UseVisualStyleBackColor = true;
            this.ClearFutureButton.Click += new System.EventHandler(this.ClearFutureButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1454, 871);
            this.Controls.Add(this.ClearFutureButton);
            this.Controls.Add(this.FutureButton);
            this.Controls.Add(this.MassBar);
            this.Controls.Add(this.MassBarLabel);
            this.Controls.Add(this.PresetButton);
            this.Controls.Add(this.PassiveCheckBox);
            this.Controls.Add(this.ShapeLabel);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.RenderDistanceLabel);
            this.Controls.Add(this.physicsSpeedX);
            this.Controls.Add(this.physicsSpeedY);
            this.Controls.Add(this.physicsSpeedZ);
            this.Controls.Add(this.Physics);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.showFaces);
            this.Controls.Add(this.doRotate);
            this.Controls.Add(this.addModel);
            this.Controls.Add(this.Item);
            this.Controls.Add(this.FPS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Graphics is cool (;";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.physicsSpeedZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.physicsSpeedY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.physicsSpeedX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MassBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer RenderLoop;
        private Label label1;
        private Label label2;
        private Label FPS;
        private ComboBox Item;
        private Button addModel;
        private CheckBox doRotate;
        private CheckBox showFaces;
        private NumericUpDown numericUpDown1;
        private Label label3;
        private CheckBox Physics;
        private NumericUpDown physicsSpeedZ;
        private NumericUpDown physicsSpeedY;
        private NumericUpDown physicsSpeedX;
        private Label RenderDistanceLabel;
        private Label speedLabel;
        private Label ShapeLabel;
        private CheckBox PassiveCheckBox;
        private Button PresetButton;
        private Label MassBarLabel;
        private NumericUpDown MassBar;
        private Button FutureButton;
        private Button ClearFutureButton;
    }
}