namespace ch.ph.FileManager
{
    partial class CreateDirectory
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._location = new System.Windows.Forms.TextBox();
            this._name = new System.Windows.Forms.TextBox();
            this._btCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name:";
            // 
            // _location
            // 
            this._location.Location = new System.Drawing.Point(71, 13);
            this._location.Name = "_location";
            this._location.ReadOnly = true;
            this._location.Size = new System.Drawing.Size(440, 20);
            this._location.TabIndex = 2;
            // 
            // _name
            // 
            this._name.Location = new System.Drawing.Point(71, 40);
            this._name.Name = "_name";
            this._name.Size = new System.Drawing.Size(440, 20);
            this._name.TabIndex = 3;
            // 
            // _btCreate
            // 
            this._btCreate.Location = new System.Drawing.Point(436, 79);
            this._btCreate.Name = "_btCreate";
            this._btCreate.Size = new System.Drawing.Size(75, 23);
            this._btCreate.TabIndex = 4;
            this._btCreate.Text = "Create";
            this._btCreate.UseVisualStyleBackColor = true;
            this._btCreate.Click += new System.EventHandler(this._btCreate_Click);
            // 
            // CreateDirectory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 114);
            this.Controls.Add(this._btCreate);
            this.Controls.Add(this._name);
            this.Controls.Add(this._location);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateDirectory";
            this.ShowInTaskbar = false;
            this.Text = "Create Directory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _location;
        private System.Windows.Forms.TextBox _name;
        private System.Windows.Forms.Button _btCreate;
    }
}