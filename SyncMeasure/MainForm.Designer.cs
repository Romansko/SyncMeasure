﻿namespace SyncMeasure
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.circularProgressBar = new CircularProgressBar.CircularProgressBar();
            this.allGraphBox = new Cyotek.Windows.Forms.ImageBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cVVMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.weightsColNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.loadingBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progGroupBox = new System.Windows.Forms.GroupBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.measureBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.handGraphBox = new Cyotek.Windows.Forms.ImageBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.armGraphBox = new Cyotek.Windows.Forms.ImageBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.elbowGraphBox = new Cyotek.Windows.Forms.ImageBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.grabStrBox = new Cyotek.Windows.Forms.ImageBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.pinchStrBox = new Cyotek.Windows.Forms.ImageBox();
            this.sumGroupBox = new System.Windows.Forms.GroupBox();
            this.pinchLabel = new System.Windows.Forms.Label();
            this.grabLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.elbowsLabel = new System.Windows.Forms.Label();
            this.armsLabel = new System.Windows.Forms.Label();
            this.handsLabel = new System.Windows.Forms.Label();
            this.allLabel = new System.Windows.Forms.Label();
            this.elbowCvvText = new System.Windows.Forms.Label();
            this.armsCvvText = new System.Windows.Forms.Label();
            this.handsCvvText = new System.Windows.Forms.Label();
            this.averageText = new System.Windows.Forms.Label();
            this.measureOnLoad = new System.Windows.Forms.CheckBox();
            this.graphicsGB = new System.Windows.Forms.GroupBox();
            this.linesRB = new System.Windows.Forms.RadioButton();
            this.pointsRB = new System.Windows.Forms.RadioButton();
            this.menuStrip.SuspendLayout();
            this.progGroupBox.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.sumGroupBox.SuspendLayout();
            this.graphicsGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "csv";
            this.openFileDialog.Filter = "csv|*.csv";
            this.openFileDialog.InitialDirectory = ".";
            this.openFileDialog.Title = "Leap Motion output file";
            // 
            // circularProgressBar
            // 
            this.circularProgressBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.circularProgressBar.AnimationSpeed = 500;
            this.circularProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.circularProgressBar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularProgressBar.ForeColor = System.Drawing.Color.DarkBlue;
            this.circularProgressBar.InnerColor = System.Drawing.Color.PowderBlue;
            this.circularProgressBar.InnerMargin = 1;
            this.circularProgressBar.InnerWidth = 1;
            this.circularProgressBar.Location = new System.Drawing.Point(32, 16);
            this.circularProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.circularProgressBar.MarqueeAnimationSpeed = 1000;
            this.circularProgressBar.Name = "circularProgressBar";
            this.circularProgressBar.OuterColor = System.Drawing.Color.DarkCyan;
            this.circularProgressBar.OuterMargin = -8;
            this.circularProgressBar.OuterWidth = 8;
            this.circularProgressBar.ProgressColor = System.Drawing.Color.Cyan;
            this.circularProgressBar.ProgressWidth = 8;
            this.circularProgressBar.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 4.125F);
            this.circularProgressBar.Size = new System.Drawing.Size(100, 100);
            this.circularProgressBar.StartAngle = 0;
            this.circularProgressBar.Step = 0;
            this.circularProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.circularProgressBar.SubscriptColor = System.Drawing.Color.Gray;
            this.circularProgressBar.SubscriptMargin = new System.Windows.Forms.Padding(0);
            this.circularProgressBar.SubscriptText = "";
            this.circularProgressBar.SuperscriptColor = System.Drawing.Color.Gray;
            this.circularProgressBar.SuperscriptMargin = new System.Windows.Forms.Padding(0);
            this.circularProgressBar.SuperscriptText = "";
            this.circularProgressBar.TabIndex = 3;
            this.circularProgressBar.Text = "Loading Text";
            this.circularProgressBar.TextMargin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.circularProgressBar.Value = 10;
            // 
            // allGraphBox
            // 
            this.allGraphBox.AllowDoubleClick = true;
            this.allGraphBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allGraphBox.Location = new System.Drawing.Point(3, 3);
            this.allGraphBox.Name = "allGraphBox";
            this.allGraphBox.Size = new System.Drawing.Size(481, 374);
            this.allGraphBox.TabIndex = 5;
            this.allGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.measureToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(696, 27);
            this.menuStrip.TabIndex = 6;
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(51, 23);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.ToolTipText = "Load LeapMotion output csv file. (ctrl+L).";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // measureToolStripMenuItem
            // 
            this.measureToolStripMenuItem.Enabled = false;
            this.measureToolStripMenuItem.Name = "measureToolStripMenuItem";
            this.measureToolStripMenuItem.Size = new System.Drawing.Size(106, 23);
            this.measureToolStripMenuItem.Text = "&Measure Sync";
            this.measureToolStripMenuItem.ToolTipText = "Measure the synchrony. (Enter).";
            this.measureToolStripMenuItem.Click += new System.EventHandler(this.measureToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cVVMethodToolStripMenuItem,
            this.toolStripSeparator2,
            this.weightsColNamesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.ToolTipText = "Program settings. (ctrl+S).";
            // 
            // cVVMethodToolStripMenuItem
            // 
            this.cVVMethodToolStripMenuItem.Name = "cVVMethodToolStripMenuItem";
            this.cVVMethodToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.cVVMethodToolStripMenuItem.Text = "CVV Method";
            this.cVVMethodToolStripMenuItem.Click += new System.EventHandler(this.cVVMethodToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
            // 
            // weightsColNamesToolStripMenuItem
            // 
            this.weightsColNamesToolStripMenuItem.Name = "weightsColNamesToolStripMenuItem";
            this.weightsColNamesToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.weightsColNamesToolStripMenuItem.Text = "Weights and Names";
            this.weightsColNamesToolStripMenuItem.Click += new System.EventHandler(this.weightsColNamesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlsToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.aboutToolStripMenuItem.Text = "&Help";
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.controlsToolStripMenuItem.Text = "&Controls";
            this.controlsToolStripMenuItem.Click += new System.EventHandler(this.controlsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(130, 24);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // progGroupBox
            // 
            this.progGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progGroupBox.Controls.Add(this.cancelButton);
            this.progGroupBox.Controls.Add(this.circularProgressBar);
            this.progGroupBox.Location = new System.Drawing.Point(519, 293);
            this.progGroupBox.Name = "progGroupBox";
            this.progGroupBox.Size = new System.Drawing.Size(171, 154);
            this.progGroupBox.TabIndex = 7;
            this.progGroupBox.TabStop = false;
            this.progGroupBox.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(32, 119);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Location = new System.Drawing.Point(12, 41);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(495, 406);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.allGraphBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(487, 380);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "All";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.handGraphBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(487, 380);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hands CVV";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // handGraphBox
            // 
            this.handGraphBox.AllowDoubleClick = true;
            this.handGraphBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.handGraphBox.Location = new System.Drawing.Point(3, 3);
            this.handGraphBox.Name = "handGraphBox";
            this.handGraphBox.Size = new System.Drawing.Size(481, 374);
            this.handGraphBox.TabIndex = 6;
            this.handGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.armGraphBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(487, 380);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Arm CVV";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // armGraphBox
            // 
            this.armGraphBox.AllowDoubleClick = true;
            this.armGraphBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.armGraphBox.Location = new System.Drawing.Point(0, 0);
            this.armGraphBox.Name = "armGraphBox";
            this.armGraphBox.Size = new System.Drawing.Size(487, 380);
            this.armGraphBox.TabIndex = 7;
            this.armGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.elbowGraphBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(487, 380);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Elbow CVV";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // elbowGraphBox
            // 
            this.elbowGraphBox.AllowDoubleClick = true;
            this.elbowGraphBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elbowGraphBox.Location = new System.Drawing.Point(0, 0);
            this.elbowGraphBox.Name = "elbowGraphBox";
            this.elbowGraphBox.Size = new System.Drawing.Size(487, 380);
            this.elbowGraphBox.TabIndex = 7;
            this.elbowGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.grabStrBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(487, 380);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Grab Strength";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // grabStrBox
            // 
            this.grabStrBox.AllowDoubleClick = true;
            this.grabStrBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grabStrBox.Location = new System.Drawing.Point(3, 3);
            this.grabStrBox.Name = "grabStrBox";
            this.grabStrBox.Size = new System.Drawing.Size(481, 374);
            this.grabStrBox.TabIndex = 8;
            this.grabStrBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.pinchStrBox);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(487, 380);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Pinch Strength";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // pinchStrBox
            // 
            this.pinchStrBox.AllowDoubleClick = true;
            this.pinchStrBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pinchStrBox.Location = new System.Drawing.Point(3, 3);
            this.pinchStrBox.Name = "pinchStrBox";
            this.pinchStrBox.Size = new System.Drawing.Size(481, 374);
            this.pinchStrBox.TabIndex = 8;
            this.pinchStrBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // sumGroupBox
            // 
            this.sumGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sumGroupBox.Controls.Add(this.pinchLabel);
            this.sumGroupBox.Controls.Add(this.grabLabel);
            this.sumGroupBox.Controls.Add(this.label6);
            this.sumGroupBox.Controls.Add(this.label5);
            this.sumGroupBox.Controls.Add(this.elbowsLabel);
            this.sumGroupBox.Controls.Add(this.armsLabel);
            this.sumGroupBox.Controls.Add(this.handsLabel);
            this.sumGroupBox.Controls.Add(this.allLabel);
            this.sumGroupBox.Controls.Add(this.elbowCvvText);
            this.sumGroupBox.Controls.Add(this.armsCvvText);
            this.sumGroupBox.Controls.Add(this.handsCvvText);
            this.sumGroupBox.Controls.Add(this.averageText);
            this.sumGroupBox.Location = new System.Drawing.Point(519, 127);
            this.sumGroupBox.Name = "sumGroupBox";
            this.sumGroupBox.Size = new System.Drawing.Size(171, 161);
            this.sumGroupBox.TabIndex = 9;
            this.sumGroupBox.TabStop = false;
            this.sumGroupBox.Text = "Summary";
            this.sumGroupBox.Visible = false;
            // 
            // pinchLabel
            // 
            this.pinchLabel.AutoSize = true;
            this.pinchLabel.Location = new System.Drawing.Point(130, 135);
            this.pinchLabel.Name = "pinchLabel";
            this.pinchLabel.Size = new System.Drawing.Size(29, 13);
            this.pinchLabel.TabIndex = 11;
            this.pinchLabel.Text = "label";
            // 
            // grabLabel
            // 
            this.grabLabel.AutoSize = true;
            this.grabLabel.Location = new System.Drawing.Point(130, 113);
            this.grabLabel.Name = "grabLabel";
            this.grabLabel.Size = new System.Drawing.Size(29, 13);
            this.grabLabel.TabIndex = 10;
            this.grabLabel.Text = "label";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Avg Pinch Strength:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Avg Grab Strength:";
            // 
            // elbowsLabel
            // 
            this.elbowsLabel.AutoSize = true;
            this.elbowsLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.elbowsLabel.Location = new System.Drawing.Point(130, 91);
            this.elbowsLabel.Name = "elbowsLabel";
            this.elbowsLabel.Size = new System.Drawing.Size(29, 13);
            this.elbowsLabel.TabIndex = 7;
            this.elbowsLabel.Text = "label";
            // 
            // armsLabel
            // 
            this.armsLabel.AutoSize = true;
            this.armsLabel.ForeColor = System.Drawing.Color.Green;
            this.armsLabel.Location = new System.Drawing.Point(130, 69);
            this.armsLabel.Name = "armsLabel";
            this.armsLabel.Size = new System.Drawing.Size(29, 13);
            this.armsLabel.TabIndex = 6;
            this.armsLabel.Text = "label";
            // 
            // handsLabel
            // 
            this.handsLabel.AutoSize = true;
            this.handsLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.handsLabel.Location = new System.Drawing.Point(130, 47);
            this.handsLabel.Name = "handsLabel";
            this.handsLabel.Size = new System.Drawing.Size(29, 13);
            this.handsLabel.TabIndex = 5;
            this.handsLabel.Text = "label";
            // 
            // allLabel
            // 
            this.allLabel.AutoSize = true;
            this.allLabel.Location = new System.Drawing.Point(130, 25);
            this.allLabel.Name = "allLabel";
            this.allLabel.Size = new System.Drawing.Size(29, 13);
            this.allLabel.TabIndex = 4;
            this.allLabel.Text = "label";
            // 
            // elbowCvvText
            // 
            this.elbowCvvText.AutoSize = true;
            this.elbowCvvText.ForeColor = System.Drawing.Color.DarkBlue;
            this.elbowCvvText.Location = new System.Drawing.Point(9, 91);
            this.elbowCvvText.Name = "elbowCvvText";
            this.elbowCvvText.Size = new System.Drawing.Size(90, 13);
            this.elbowCvvText.TabIndex = 3;
            this.elbowCvvText.Text = "Avg Elbows CVV:";
            // 
            // armsCvvText
            // 
            this.armsCvvText.AutoSize = true;
            this.armsCvvText.ForeColor = System.Drawing.Color.Green;
            this.armsCvvText.Location = new System.Drawing.Point(9, 69);
            this.armsCvvText.Name = "armsCvvText";
            this.armsCvvText.Size = new System.Drawing.Size(79, 13);
            this.armsCvvText.TabIndex = 2;
            this.armsCvvText.Text = "Avg Arms CVV:";
            // 
            // handsCvvText
            // 
            this.handsCvvText.AutoSize = true;
            this.handsCvvText.ForeColor = System.Drawing.Color.DarkRed;
            this.handsCvvText.Location = new System.Drawing.Point(9, 47);
            this.handsCvvText.Name = "handsCvvText";
            this.handsCvvText.Size = new System.Drawing.Size(87, 13);
            this.handsCvvText.TabIndex = 1;
            this.handsCvvText.Text = "Avg Hands CVV:";
            // 
            // averageText
            // 
            this.averageText.AutoSize = true;
            this.averageText.Location = new System.Drawing.Point(9, 25);
            this.averageText.Name = "averageText";
            this.averageText.Size = new System.Drawing.Size(50, 13);
            this.averageText.TabIndex = 0;
            this.averageText.Text = "Average:";
            // 
            // measureOnLoad
            // 
            this.measureOnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.measureOnLoad.AutoSize = true;
            this.measureOnLoad.Location = new System.Drawing.Point(519, 38);
            this.measureOnLoad.Name = "measureOnLoad";
            this.measureOnLoad.Size = new System.Drawing.Size(132, 17);
            this.measureOnLoad.TabIndex = 10;
            this.measureOnLoad.Text = "Measure Sync on load";
            this.measureOnLoad.UseVisualStyleBackColor = true;
            // 
            // graphicsGB
            // 
            this.graphicsGB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.graphicsGB.Controls.Add(this.linesRB);
            this.graphicsGB.Controls.Add(this.pointsRB);
            this.graphicsGB.Location = new System.Drawing.Point(519, 61);
            this.graphicsGB.Name = "graphicsGB";
            this.graphicsGB.Size = new System.Drawing.Size(171, 60);
            this.graphicsGB.TabIndex = 11;
            this.graphicsGB.TabStop = false;
            this.graphicsGB.Text = "Graph Graphics";
            // 
            // linesRB
            // 
            this.linesRB.AutoSize = true;
            this.linesRB.Location = new System.Drawing.Point(115, 28);
            this.linesRB.Name = "linesRB";
            this.linesRB.Size = new System.Drawing.Size(50, 17);
            this.linesRB.TabIndex = 1;
            this.linesRB.Text = "Lines";
            this.linesRB.UseVisualStyleBackColor = true;
            this.linesRB.CheckedChanged += new System.EventHandler(this.graphics_CheckedChanged);
            // 
            // pointsRB
            // 
            this.pointsRB.AutoSize = true;
            this.pointsRB.Checked = true;
            this.pointsRB.Location = new System.Drawing.Point(6, 28);
            this.pointsRB.Name = "pointsRB";
            this.pointsRB.Size = new System.Drawing.Size(54, 17);
            this.pointsRB.TabIndex = 0;
            this.pointsRB.TabStop = true;
            this.pointsRB.Text = "Points";
            this.pointsRB.UseVisualStyleBackColor = true;
            this.pointsRB.CheckedChanged += new System.EventHandler(this.graphics_CheckedChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(696, 459);
            this.Controls.Add(this.graphicsGB);
            this.Controls.Add(this.measureOnLoad);
            this.Controls.Add(this.sumGroupBox);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.progGroupBox);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(712, 498);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SyncMeasure";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.progGroupBox.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.sumGroupBox.ResumeLayout(false);
            this.sumGroupBox.PerformLayout();
            this.graphicsGB.ResumeLayout(false);
            this.graphicsGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private CircularProgressBar.CircularProgressBar circularProgressBar;
        private Cyotek.Windows.Forms.ImageBox allGraphBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measureToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.ComponentModel.BackgroundWorker loadingBackgroundWorker;
        private System.Windows.Forms.GroupBox progGroupBox;
        private System.Windows.Forms.Button cancelButton;
        private System.ComponentModel.BackgroundWorker measureBackgroundWorker;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox sumGroupBox;
        private Cyotek.Windows.Forms.ImageBox handGraphBox;
        private Cyotek.Windows.Forms.ImageBox armGraphBox;
        private Cyotek.Windows.Forms.ImageBox elbowGraphBox;
        private System.Windows.Forms.Label elbowCvvText;
        private System.Windows.Forms.Label armsCvvText;
        private System.Windows.Forms.Label handsCvvText;
        private System.Windows.Forms.Label averageText;
        private System.Windows.Forms.Label elbowsLabel;
        private System.Windows.Forms.Label armsLabel;
        private System.Windows.Forms.Label handsLabel;
        private System.Windows.Forms.Label allLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.CheckBox measureOnLoad;
        private System.Windows.Forms.TabPage tabPage5;
        private Cyotek.Windows.Forms.ImageBox grabStrBox;
        private System.Windows.Forms.TabPage tabPage6;
        private Cyotek.Windows.Forms.ImageBox pinchStrBox;
        private System.Windows.Forms.Label pinchLabel;
        private System.Windows.Forms.Label grabLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem weightsColNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cVVMethodToolStripMenuItem;
        private System.Windows.Forms.GroupBox graphicsGB;
        private System.Windows.Forms.RadioButton linesRB;
        private System.Windows.Forms.RadioButton pointsRB;
    }
}

