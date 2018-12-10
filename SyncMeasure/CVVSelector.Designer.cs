namespace SyncMeasure
{
    partial class CvvSelector
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.squareSvv = new System.Windows.Forms.RadioButton();
            this.absCvv = new System.Windows.Forms.RadioButton();
            this.regularCvv = new System.Windows.Forms.RadioButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.squareSvv);
            this.groupBox.Controls.Add(this.absCvv);
            this.groupBox.Controls.Add(this.regularCvv);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(157, 113);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "CVV Calculation Method";
            // 
            // squareSvv
            // 
            this.squareSvv.AutoSize = true;
            this.squareSvv.Location = new System.Drawing.Point(6, 78);
            this.squareSvv.Name = "squareSvv";
            this.squareSvv.Size = new System.Drawing.Size(83, 17);
            this.squareSvv.TabIndex = 2;
            this.squareSvv.Text = "Square CVV";
            this.toolTip.SetToolTip(this.squareSvv, "CVV^2. Might be good for considering opposite direction as synchronization.");
            this.squareSvv.UseVisualStyleBackColor = true;
            // 
            // absCvv
            // 
            this.absCvv.AutoSize = true;
            this.absCvv.Location = new System.Drawing.Point(6, 55);
            this.absCvv.Name = "absCvv";
            this.absCvv.Size = new System.Drawing.Size(119, 17);
            this.absCvv.TabIndex = 1;
            this.absCvv.Text = "Absolute value CVV";
            this.toolTip.SetToolTip(this.absCvv, "|CVV|. Might be good for considering opposite direction as synchronization.");
            this.absCvv.UseVisualStyleBackColor = true;
            // 
            // regularCvv
            // 
            this.regularCvv.AutoSize = true;
            this.regularCvv.Checked = true;
            this.regularCvv.Location = new System.Drawing.Point(6, 32);
            this.regularCvv.Name = "regularCvv";
            this.regularCvv.Size = new System.Drawing.Size(86, 17);
            this.regularCvv.TabIndex = 0;
            this.regularCvv.TabStop = true;
            this.regularCvv.Text = "Regular CVV";
            this.toolTip.SetToolTip(this.regularCvv, "Might be negative as well.");
            this.regularCvv.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 131);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(68, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(101, 131);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(68, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // CvvSelector
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(180, 162);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CvvSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CVVSelector";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton squareSvv;
        private System.Windows.Forms.RadioButton absCvv;
        private System.Windows.Forms.RadioButton regularCvv;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}