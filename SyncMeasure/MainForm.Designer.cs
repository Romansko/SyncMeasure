namespace SyncMeasure
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
            this.sumGroupBox = new System.Windows.Forms.GroupBox();
            this.elbowsLabel = new System.Windows.Forms.Label();
            this.armsLabel = new System.Windows.Forms.Label();
            this.handsLabel = new System.Windows.Forms.Label();
            this.allLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.measureOnLoad = new System.Windows.Forms.CheckBox();
            this.menuStrip.SuspendLayout();
            this.progGroupBox.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.sumGroupBox.SuspendLayout();
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
            this.circularProgressBar.Location = new System.Drawing.Point(21, 16);
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
            this.allGraphBox.Size = new System.Drawing.Size(440, 262);
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
            this.menuStrip.Size = new System.Drawing.Size(641, 27);
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
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.ToolTipText = "Program settings. (ctrl+S).";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
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
            this.progGroupBox.Location = new System.Drawing.Point(481, 181);
            this.progGroupBox.Name = "progGroupBox";
            this.progGroupBox.Size = new System.Drawing.Size(148, 154);
            this.progGroupBox.TabIndex = 7;
            this.progGroupBox.TabStop = false;
            this.progGroupBox.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(21, 119);
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
            this.tabControl.Location = new System.Drawing.Point(12, 41);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(454, 294);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.allGraphBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(446, 268);
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
            this.tabPage2.Size = new System.Drawing.Size(446, 268);
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
            this.handGraphBox.Size = new System.Drawing.Size(440, 262);
            this.handGraphBox.TabIndex = 6;
            this.handGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.armGraphBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(446, 268);
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
            this.armGraphBox.Size = new System.Drawing.Size(446, 268);
            this.armGraphBox.TabIndex = 7;
            this.armGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.elbowGraphBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(446, 268);
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
            this.elbowGraphBox.Size = new System.Drawing.Size(446, 268);
            this.elbowGraphBox.TabIndex = 7;
            this.elbowGraphBox.DoubleClick += new System.EventHandler(this.graphBox_DoubleClick);
            // 
            // sumGroupBox
            // 
            this.sumGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sumGroupBox.Controls.Add(this.elbowsLabel);
            this.sumGroupBox.Controls.Add(this.armsLabel);
            this.sumGroupBox.Controls.Add(this.handsLabel);
            this.sumGroupBox.Controls.Add(this.allLabel);
            this.sumGroupBox.Controls.Add(this.label4);
            this.sumGroupBox.Controls.Add(this.label3);
            this.sumGroupBox.Controls.Add(this.label2);
            this.sumGroupBox.Controls.Add(this.label1);
            this.sumGroupBox.Location = new System.Drawing.Point(481, 61);
            this.sumGroupBox.Name = "sumGroupBox";
            this.sumGroupBox.Size = new System.Drawing.Size(148, 114);
            this.sumGroupBox.TabIndex = 9;
            this.sumGroupBox.TabStop = false;
            this.sumGroupBox.Text = "Summary";
            this.sumGroupBox.Visible = false;
            // 
            // elbowsLabel
            // 
            this.elbowsLabel.AutoSize = true;
            this.elbowsLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.elbowsLabel.Location = new System.Drawing.Point(107, 91);
            this.elbowsLabel.Name = "elbowsLabel";
            this.elbowsLabel.Size = new System.Drawing.Size(35, 13);
            this.elbowsLabel.TabIndex = 7;
            this.elbowsLabel.Text = "label8";
            // 
            // armsLabel
            // 
            this.armsLabel.AutoSize = true;
            this.armsLabel.ForeColor = System.Drawing.Color.Green;
            this.armsLabel.Location = new System.Drawing.Point(107, 69);
            this.armsLabel.Name = "armsLabel";
            this.armsLabel.Size = new System.Drawing.Size(35, 13);
            this.armsLabel.TabIndex = 6;
            this.armsLabel.Text = "label7";
            // 
            // handsLabel
            // 
            this.handsLabel.AutoSize = true;
            this.handsLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.handsLabel.Location = new System.Drawing.Point(107, 47);
            this.handsLabel.Name = "handsLabel";
            this.handsLabel.Size = new System.Drawing.Size(35, 13);
            this.handsLabel.TabIndex = 5;
            this.handsLabel.Text = "label6";
            // 
            // allLabel
            // 
            this.allLabel.AutoSize = true;
            this.allLabel.Location = new System.Drawing.Point(107, 25);
            this.allLabel.Name = "allLabel";
            this.allLabel.Size = new System.Drawing.Size(35, 13);
            this.allLabel.TabIndex = 4;
            this.allLabel.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(9, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Avg Elbows CVV:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(9, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Avg Arms CVV:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Avg Hands CVV:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Avg CVV:";
            // 
            // measureOnLoad
            // 
            this.measureOnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.measureOnLoad.AutoSize = true;
            this.measureOnLoad.Location = new System.Drawing.Point(481, 38);
            this.measureOnLoad.Name = "measureOnLoad";
            this.measureOnLoad.Size = new System.Drawing.Size(132, 17);
            this.measureOnLoad.TabIndex = 10;
            this.measureOnLoad.Text = "Measure Sync on load";
            this.measureOnLoad.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(641, 347);
            this.Controls.Add(this.measureOnLoad);
            this.Controls.Add(this.sumGroupBox);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.progGroupBox);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(657, 386);
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
            this.sumGroupBox.ResumeLayout(false);
            this.sumGroupBox.PerformLayout();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label elbowsLabel;
        private System.Windows.Forms.Label armsLabel;
        private System.Windows.Forms.Label handsLabel;
        private System.Windows.Forms.Label allLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.CheckBox measureOnLoad;
    }
}

