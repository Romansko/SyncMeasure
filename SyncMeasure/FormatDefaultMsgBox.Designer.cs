namespace SyncMeasure
{
    partial class FormatDefaultMsgBox
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.oldFormat = new System.Windows.Forms.Button();
            this.newFormat = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(248, 66);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // oldFormat
            // 
            this.oldFormat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.oldFormat.Location = new System.Drawing.Point(125, 66);
            this.oldFormat.Name = "oldFormat";
            this.oldFormat.Size = new System.Drawing.Size(75, 23);
            this.oldFormat.TabIndex = 1;
            this.oldFormat.Text = "Old Format";
            this.oldFormat.UseVisualStyleBackColor = true;
            this.oldFormat.Click += new System.EventHandler(this.oldFormat_Click);
            // 
            // newFormat
            // 
            this.newFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newFormat.Location = new System.Drawing.Point(12, 66);
            this.newFormat.Name = "newFormat";
            this.newFormat.Size = new System.Drawing.Size(75, 23);
            this.newFormat.TabIndex = 2;
            this.newFormat.Text = "New Format";
            this.newFormat.UseVisualStyleBackColor = true;
            this.newFormat.Click += new System.EventHandler(this.newFormat_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(13, 13);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(205, 13);
            this.label.TabIndex = 3;
            this.label.Text = "Set csv file column names format defaults:";
            // 
            // FormatDefaultMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(335, 101);
            this.Controls.Add(this.label);
            this.Controls.Add(this.newFormat);
            this.Controls.Add(this.oldFormat);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormatDefaultMsgBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Format Defaults";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button oldFormat;
        private System.Windows.Forms.Button newFormat;
        private System.Windows.Forms.Label label;
    }
}