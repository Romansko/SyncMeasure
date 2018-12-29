namespace SyncMeasure
{
    partial class SyncMeasureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncMeasureForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.circularProgressBar = new CircularProgressBar.CircularProgressBar();
            this.allGraphBox = new Cyotek.Windows.Forms.ImageBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.measureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.combineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weightsColNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVFileColumnNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.setNBasisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.setTimeLagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.averageText = new System.Windows.Forms.Label();
            this.timeLagGB = new System.Windows.Forms.GroupBox();
            this.timeLagLabel = new System.Windows.Forms.Label();
            this.absCvvButton = new System.Windows.Forms.RadioButton();
            this.squareCvvButton = new System.Windows.Forms.RadioButton();
            this.cvvButton = new System.Windows.Forms.RadioButton();
            this.graphicsGB = new System.Windows.Forms.GroupBox();
            this.bothRB = new System.Windows.Forms.RadioButton();
            this.linesRB = new System.Windows.Forms.RadioButton();
            this.pointsRB = new System.Windows.Forms.RadioButton();
            this.cvvMethodGB = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.nbasisLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pinchLabel = new System.Windows.Forms.Label();
            this.grabLabel = new System.Windows.Forms.Label();
            this.elbowsLabel = new System.Windows.Forms.Label();
            this.armsLabel = new System.Windows.Forms.Label();
            this.handsLabel = new System.Windows.Forms.Label();
            this.avgLabel = new System.Windows.Forms.Label();
            this.elbowCvvText = new System.Windows.Forms.Label();
            this.armsCvvText = new System.Windows.Forms.Label();
            this.handsCvvText = new System.Windows.Forms.Label();
            this.measureOnLoad = new System.Windows.Forms.CheckBox();
            this.menuStrip.SuspendLayout();
            this.timeLagGB.SuspendLayout();
            this.graphicsGB.SuspendLayout();
            this.cvvMethodGB.SuspendLayout();
            this.progGroupBox.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.sumGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.circularProgressBar.Location = new System.Drawing.Point(45, 16);
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
            this.allGraphBox.Size = new System.Drawing.Size(447, 414);
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
            this.combineToolStripMenuItem,
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
            // combineToolStripMenuItem
            // 
            this.combineToolStripMenuItem.Name = "combineToolStripMenuItem";
            this.combineToolStripMenuItem.Size = new System.Drawing.Size(76, 23);
            this.combineToolStripMenuItem.Text = "&Combine";
            this.combineToolStripMenuItem.ToolTipText = "Combine alone files to single file.";
            this.combineToolStripMenuItem.Click += new System.EventHandler(this.combineToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.weightsColNamesToolStripMenuItem,
            this.cSVFileColumnNamesToolStripMenuItem,
            this.toolStripSeparator2,
            this.setNBasisToolStripMenuItem,
            this.toolStripSeparator3,
            this.setTimeLagToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.ToolTipText = "Program settings. (ctrl+S).";
            // 
            // weightsColNamesToolStripMenuItem
            // 
            this.weightsColNamesToolStripMenuItem.Name = "weightsColNamesToolStripMenuItem";
            this.weightsColNamesToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.weightsColNamesToolStripMenuItem.Text = "Sync Weights";
            this.weightsColNamesToolStripMenuItem.Click += new System.EventHandler(this.weightsColNamesToolStripMenuItem_Click);
            // 
            // cSVFileColumnNamesToolStripMenuItem
            // 
            this.cSVFileColumnNamesToolStripMenuItem.Name = "cSVFileColumnNamesToolStripMenuItem";
            this.cSVFileColumnNamesToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.cSVFileColumnNamesToolStripMenuItem.Text = "CSV File column names";
            this.cSVFileColumnNamesToolStripMenuItem.Click += new System.EventHandler(this.cSVFileColumnNamesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // setNBasisToolStripMenuItem
            // 
            this.setNBasisToolStripMenuItem.Name = "setNBasisToolStripMenuItem";
            this.setNBasisToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.setNBasisToolStripMenuItem.Text = "Set nBasis";
            this.setNBasisToolStripMenuItem.ToolTipText = "Dimension of B-spline basis used to represent each person\'s data along each dimen" +
    "sion.";
            this.setNBasisToolStripMenuItem.Click += new System.EventHandler(this.SetNBasis);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(217, 6);
            // 
            // setTimeLagToolStripMenuItem
            // 
            this.setTimeLagToolStripMenuItem.Name = "setTimeLagToolStripMenuItem";
            this.setTimeLagToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.setTimeLagToolStripMenuItem.Text = "Set Time Lag";
            this.setTimeLagToolStripMenuItem.Click += new System.EventHandler(this.SetTimeLag);
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
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Avg Pinch Strength:";
            this.toolTip.SetToolTip(this.label6, "(1 - Pinch Difference)");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Avg Grab Strength:";
            this.toolTip.SetToolTip(this.label5, "(1 - Grab Difference)");
            // 
            // averageText
            // 
            this.averageText.AutoSize = true;
            this.averageText.Location = new System.Drawing.Point(9, 25);
            this.averageText.Name = "averageText";
            this.averageText.Size = new System.Drawing.Size(50, 13);
            this.averageText.TabIndex = 0;
            this.averageText.Text = "Average:";
            this.toolTip.SetToolTip(this.averageText, "Weighted calculation");
            // 
            // timeLagGB
            // 
            this.timeLagGB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLagGB.Controls.Add(this.timeLagLabel);
            this.timeLagGB.Location = new System.Drawing.Point(489, 163);
            this.timeLagGB.Name = "timeLagGB";
            this.timeLagGB.Size = new System.Drawing.Size(195, 45);
            this.timeLagGB.TabIndex = 12;
            this.timeLagGB.TabStop = false;
            this.timeLagGB.Text = "Current Time Lag [ms]";
            this.toolTip.SetToolTip(this.timeLagGB, "Current time lag [ms] for person 1 with respect to person 0. Changes will be refl" +
        "ected on the next sync measuring.");
            // 
            // timeLagLabel
            // 
            this.timeLagLabel.AutoSize = true;
            this.timeLagLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.timeLagLabel.ForeColor = System.Drawing.Color.Black;
            this.timeLagLabel.Location = new System.Drawing.Point(7, 20);
            this.timeLagLabel.Name = "timeLagLabel";
            this.timeLagLabel.Size = new System.Drawing.Size(41, 13);
            this.timeLagLabel.TabIndex = 0;
            this.timeLagLabel.Text = "0 [ms]";
            this.toolTip.SetToolTip(this.timeLagLabel, "Current time lag [ms] for person 1 with respect to person 0. Changes will be refl" +
        "ected on the next sync measuring.");
            // 
            // absCvvButton
            // 
            this.absCvvButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.absCvvButton.AutoSize = true;
            this.absCvvButton.Location = new System.Drawing.Point(75, 19);
            this.absCvvButton.Name = "absCvvButton";
            this.absCvvButton.Size = new System.Drawing.Size(50, 17);
            this.absCvvButton.TabIndex = 2;
            this.absCvvButton.Text = "|CVV|";
            this.toolTip.SetToolTip(this.absCvvButton, "Absolute value CVV. To be used with \"Mirror\" motion.");
            this.absCvvButton.UseVisualStyleBackColor = true;
            this.absCvvButton.CheckedChanged += new System.EventHandler(this.cvvMethod_CheckedChanged);
            // 
            // squareCvvButton
            // 
            this.squareCvvButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.squareCvvButton.AutoSize = true;
            this.squareCvvButton.Location = new System.Drawing.Point(131, 19);
            this.squareCvvButton.Name = "squareCvvButton";
            this.squareCvvButton.Size = new System.Drawing.Size(58, 17);
            this.squareCvvButton.TabIndex = 1;
            this.squareCvvButton.Text = "CVV^2";
            this.toolTip.SetToolTip(this.squareCvvButton, "Square CVV. To be used with \"Mirror\" motion. Differentitates more strongly betwee" +
        "n cosine values near +-1.");
            this.squareCvvButton.UseVisualStyleBackColor = true;
            this.squareCvvButton.CheckedChanged += new System.EventHandler(this.cvvMethod_CheckedChanged);
            // 
            // cvvButton
            // 
            this.cvvButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cvvButton.AutoSize = true;
            this.cvvButton.Checked = true;
            this.cvvButton.Location = new System.Drawing.Point(14, 19);
            this.cvvButton.Name = "cvvButton";
            this.cvvButton.Size = new System.Drawing.Size(46, 17);
            this.cvvButton.TabIndex = 0;
            this.cvvButton.TabStop = true;
            this.cvvButton.Text = "CVV";
            this.toolTip.SetToolTip(this.cvvButton, "Regular CVV. Might be a negative value. To be used with regular motion.");
            this.cvvButton.UseVisualStyleBackColor = true;
            this.cvvButton.CheckedChanged += new System.EventHandler(this.cvvMethod_CheckedChanged);
            // 
            // graphicsGB
            // 
            this.graphicsGB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.graphicsGB.Controls.Add(this.bothRB);
            this.graphicsGB.Controls.Add(this.linesRB);
            this.graphicsGB.Controls.Add(this.pointsRB);
            this.graphicsGB.Location = new System.Drawing.Point(489, 112);
            this.graphicsGB.Name = "graphicsGB";
            this.graphicsGB.Size = new System.Drawing.Size(195, 45);
            this.graphicsGB.TabIndex = 11;
            this.graphicsGB.TabStop = false;
            this.graphicsGB.Text = "Graph Graphics";
            this.toolTip.SetToolTip(this.graphicsGB, "Changes will be reflected on the next sync measuring.");
            // 
            // bothRB
            // 
            this.bothRB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bothRB.AutoSize = true;
            this.bothRB.Checked = true;
            this.bothRB.Location = new System.Drawing.Point(75, 19);
            this.bothRB.Name = "bothRB";
            this.bothRB.Size = new System.Drawing.Size(47, 17);
            this.bothRB.TabIndex = 2;
            this.bothRB.TabStop = true;
            this.bothRB.Text = "Both";
            this.bothRB.UseVisualStyleBackColor = true;
            this.bothRB.CheckedChanged += new System.EventHandler(this.graphics_CheckedChanged);
            // 
            // linesRB
            // 
            this.linesRB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linesRB.AutoSize = true;
            this.linesRB.Location = new System.Drawing.Point(131, 19);
            this.linesRB.Name = "linesRB";
            this.linesRB.Size = new System.Drawing.Size(50, 17);
            this.linesRB.TabIndex = 1;
            this.linesRB.Text = "Lines";
            this.linesRB.UseVisualStyleBackColor = true;
            this.linesRB.CheckedChanged += new System.EventHandler(this.graphics_CheckedChanged);
            // 
            // pointsRB
            // 
            this.pointsRB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pointsRB.AutoSize = true;
            this.pointsRB.Location = new System.Drawing.Point(14, 19);
            this.pointsRB.Name = "pointsRB";
            this.pointsRB.Size = new System.Drawing.Size(54, 17);
            this.pointsRB.TabIndex = 0;
            this.pointsRB.Text = "Points";
            this.pointsRB.UseVisualStyleBackColor = true;
            this.pointsRB.CheckedChanged += new System.EventHandler(this.graphics_CheckedChanged);
            // 
            // cvvMethodGB
            // 
            this.cvvMethodGB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cvvMethodGB.Controls.Add(this.absCvvButton);
            this.cvvMethodGB.Controls.Add(this.squareCvvButton);
            this.cvvMethodGB.Controls.Add(this.cvvButton);
            this.cvvMethodGB.Location = new System.Drawing.Point(489, 61);
            this.cvvMethodGB.Name = "cvvMethodGB";
            this.cvvMethodGB.Size = new System.Drawing.Size(195, 45);
            this.cvvMethodGB.TabIndex = 12;
            this.cvvMethodGB.TabStop = false;
            this.cvvMethodGB.Text = "CVV Calculation Method";
            this.toolTip.SetToolTip(this.cvvMethodGB, "Changes will be reflected on the next sync measuring.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "nbasis:";
            this.toolTip.SetToolTip(this.label3, "Dimension of B-spline basis used to represent each person\'s data along each dimen" +
        "sion.");
            // 
            // progGroupBox
            // 
            this.progGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progGroupBox.Controls.Add(this.cancelButton);
            this.progGroupBox.Controls.Add(this.circularProgressBar);
            this.progGroupBox.Location = new System.Drawing.Point(489, 333);
            this.progGroupBox.Name = "progGroupBox";
            this.progGroupBox.Size = new System.Drawing.Size(195, 154);
            this.progGroupBox.TabIndex = 7;
            this.progGroupBox.TabStop = false;
            this.progGroupBox.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(45, 119);
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
            this.tabControl.Size = new System.Drawing.Size(461, 446);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.allGraphBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(453, 420);
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
            this.tabPage2.Size = new System.Drawing.Size(453, 420);
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
            this.handGraphBox.Size = new System.Drawing.Size(447, 414);
            this.handGraphBox.TabIndex = 6;
            this.handGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.armGraphBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(453, 420);
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
            this.armGraphBox.Size = new System.Drawing.Size(453, 420);
            this.armGraphBox.TabIndex = 7;
            this.armGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.elbowGraphBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(453, 420);
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
            this.elbowGraphBox.Size = new System.Drawing.Size(453, 420);
            this.elbowGraphBox.TabIndex = 7;
            this.elbowGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.grabStrBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(453, 420);
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
            this.grabStrBox.Size = new System.Drawing.Size(447, 414);
            this.grabStrBox.TabIndex = 8;
            this.grabStrBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.pinchStrBox);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(453, 420);
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
            this.pinchStrBox.Size = new System.Drawing.Size(447, 414);
            this.pinchStrBox.TabIndex = 8;
            this.pinchStrBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // sumGroupBox
            // 
            this.sumGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sumGroupBox.Controls.Add(this.nbasisLabel);
            this.sumGroupBox.Controls.Add(this.label3);
            this.sumGroupBox.Controls.Add(this.groupBox1);
            this.sumGroupBox.Controls.Add(this.pinchLabel);
            this.sumGroupBox.Controls.Add(this.grabLabel);
            this.sumGroupBox.Controls.Add(this.label6);
            this.sumGroupBox.Controls.Add(this.label5);
            this.sumGroupBox.Controls.Add(this.elbowsLabel);
            this.sumGroupBox.Controls.Add(this.armsLabel);
            this.sumGroupBox.Controls.Add(this.handsLabel);
            this.sumGroupBox.Controls.Add(this.avgLabel);
            this.sumGroupBox.Controls.Add(this.elbowCvvText);
            this.sumGroupBox.Controls.Add(this.armsCvvText);
            this.sumGroupBox.Controls.Add(this.handsCvvText);
            this.sumGroupBox.Controls.Add(this.averageText);
            this.sumGroupBox.Enabled = false;
            this.sumGroupBox.Location = new System.Drawing.Point(489, 218);
            this.sumGroupBox.Name = "sumGroupBox";
            this.sumGroupBox.Size = new System.Drawing.Size(195, 269);
            this.sumGroupBox.TabIndex = 9;
            this.sumGroupBox.TabStop = false;
            this.sumGroupBox.Text = "Synchronization Summary";
            this.sumGroupBox.Visible = false;
            // 
            // nbasisLabel
            // 
            this.nbasisLabel.AutoSize = true;
            this.nbasisLabel.Location = new System.Drawing.Point(152, 157);
            this.nbasisLabel.Name = "nbasisLabel";
            this.nbasisLabel.Size = new System.Drawing.Size(29, 13);
            this.nbasisLabel.TabIndex = 14;
            this.nbasisLabel.Text = "label";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox1.Location = new System.Drawing.Point(12, 189);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 74);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Legend";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "1 - Synchronized";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "0 - Not synchronized";
            // 
            // pinchLabel
            // 
            this.pinchLabel.AutoSize = true;
            this.pinchLabel.Location = new System.Drawing.Point(152, 135);
            this.pinchLabel.Name = "pinchLabel";
            this.pinchLabel.Size = new System.Drawing.Size(29, 13);
            this.pinchLabel.TabIndex = 11;
            this.pinchLabel.Text = "label";
            // 
            // grabLabel
            // 
            this.grabLabel.AutoSize = true;
            this.grabLabel.Location = new System.Drawing.Point(152, 113);
            this.grabLabel.Name = "grabLabel";
            this.grabLabel.Size = new System.Drawing.Size(29, 13);
            this.grabLabel.TabIndex = 10;
            this.grabLabel.Text = "label";
            // 
            // elbowsLabel
            // 
            this.elbowsLabel.AutoSize = true;
            this.elbowsLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.elbowsLabel.Location = new System.Drawing.Point(152, 91);
            this.elbowsLabel.Name = "elbowsLabel";
            this.elbowsLabel.Size = new System.Drawing.Size(29, 13);
            this.elbowsLabel.TabIndex = 7;
            this.elbowsLabel.Text = "label";
            // 
            // armsLabel
            // 
            this.armsLabel.AutoSize = true;
            this.armsLabel.ForeColor = System.Drawing.Color.Green;
            this.armsLabel.Location = new System.Drawing.Point(152, 69);
            this.armsLabel.Name = "armsLabel";
            this.armsLabel.Size = new System.Drawing.Size(29, 13);
            this.armsLabel.TabIndex = 6;
            this.armsLabel.Text = "label";
            // 
            // handsLabel
            // 
            this.handsLabel.AutoSize = true;
            this.handsLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.handsLabel.Location = new System.Drawing.Point(152, 47);
            this.handsLabel.Name = "handsLabel";
            this.handsLabel.Size = new System.Drawing.Size(29, 13);
            this.handsLabel.TabIndex = 5;
            this.handsLabel.Text = "label";
            // 
            // avgLabel
            // 
            this.avgLabel.AutoSize = true;
            this.avgLabel.Location = new System.Drawing.Point(152, 25);
            this.avgLabel.Name = "avgLabel";
            this.avgLabel.Size = new System.Drawing.Size(29, 13);
            this.avgLabel.TabIndex = 4;
            this.avgLabel.Text = "label";
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
            // measureOnLoad
            // 
            this.measureOnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.measureOnLoad.AutoSize = true;
            this.measureOnLoad.Location = new System.Drawing.Point(491, 38);
            this.measureOnLoad.Name = "measureOnLoad";
            this.measureOnLoad.Size = new System.Drawing.Size(132, 17);
            this.measureOnLoad.TabIndex = 10;
            this.measureOnLoad.Text = "Measure Sync on load";
            this.measureOnLoad.UseVisualStyleBackColor = true;
            // 
            // SyncMeasureForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(696, 499);
            this.Controls.Add(this.cvvMethodGB);
            this.Controls.Add(this.timeLagGB);
            this.Controls.Add(this.graphicsGB);
            this.Controls.Add(this.measureOnLoad);
            this.Controls.Add(this.sumGroupBox);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.progGroupBox);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(712, 538);
            this.Name = "SyncMeasureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SyncMeasure";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.timeLagGB.ResumeLayout(false);
            this.timeLagGB.PerformLayout();
            this.graphicsGB.ResumeLayout(false);
            this.graphicsGB.PerformLayout();
            this.cvvMethodGB.ResumeLayout(false);
            this.cvvMethodGB.PerformLayout();
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label avgLabel;
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
        private System.Windows.Forms.GroupBox graphicsGB;
        private System.Windows.Forms.RadioButton linesRB;
        private System.Windows.Forms.RadioButton pointsRB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem combineToolStripMenuItem;
        private System.Windows.Forms.RadioButton bothRB;
        private System.Windows.Forms.ToolStripMenuItem cSVFileColumnNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem setTimeLagToolStripMenuItem;
        private System.Windows.Forms.GroupBox timeLagGB;
        private System.Windows.Forms.Label timeLagLabel;
        private System.Windows.Forms.GroupBox cvvMethodGB;
        private System.Windows.Forms.RadioButton absCvvButton;
        private System.Windows.Forms.RadioButton squareCvvButton;
        private System.Windows.Forms.RadioButton cvvButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem setNBasisToolStripMenuItem;
        private System.Windows.Forms.Label nbasisLabel;
        private System.Windows.Forms.Label label3;
    }
}

