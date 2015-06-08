namespace ch.ph.FileManager
{
    partial class BackgroundWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this._btCancel = new System.Windows.Forms.Button();
            this._line1 = new System.Windows.Forms.TextBox();
            this._line2 = new System.Windows.Forms.TextBox();
            this._line3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _btCancel
            // 
            this._btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btCancel.Location = new System.Drawing.Point(401, 91);
            this._btCancel.Name = "_btCancel";
            this._btCancel.Size = new System.Drawing.Size(75, 23);
            this._btCancel.TabIndex = 0;
            this._btCancel.Text = "Cancel";
            this._btCancel.UseVisualStyleBackColor = true;
            // 
            // _line1
            // 
            this._line1.BackColor = System.Drawing.SystemColors.Control;
            this._line1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._line1.Location = new System.Drawing.Point(13, 13);
            this._line1.Name = "_line1";
            this._line1.ReadOnly = true;
            this._line1.Size = new System.Drawing.Size(463, 13);
            this._line1.TabIndex = 1;
            // 
            // _line2
            // 
            this._line2.BackColor = System.Drawing.SystemColors.Control;
            this._line2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._line2.Location = new System.Drawing.Point(13, 39);
            this._line2.Name = "_line2";
            this._line2.ReadOnly = true;
            this._line2.Size = new System.Drawing.Size(463, 13);
            this._line2.TabIndex = 2;
            // 
            // _line3
            // 
            this._line3.BackColor = System.Drawing.SystemColors.Control;
            this._line3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._line3.Location = new System.Drawing.Point(13, 65);
            this._line3.Name = "_line3";
            this._line3.ReadOnly = true;
            this._line3.Size = new System.Drawing.Size(463, 13);
            this._line3.TabIndex = 3;
            // 
            // BackgroundWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 123);
            this.ControlBox = false;
            this.Controls.Add(this._line3);
            this.Controls.Add(this._line2);
            this.Controls.Add(this._line1);
            this.Controls.Add(this._btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BackgroundWindow";
            this.ShowInTaskbar = false;
            this.Text = "Background Job...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btCancel;
        private System.Windows.Forms.TextBox _line1;
        private System.Windows.Forms.TextBox _line2;
        private System.Windows.Forms.TextBox _line3;
    }
}