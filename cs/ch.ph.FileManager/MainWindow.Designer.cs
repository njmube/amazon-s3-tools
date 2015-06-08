namespace ch.ph.FileManager
{
    partial class MainWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.localOpen = new System.Windows.Forms.Button();
            this._localRootDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._radioPrivateOnly = new System.Windows.Forms.RadioButton();
            this._radioPublicRead = new System.Windows.Forms.RadioButton();
            this._radioPublicWrite = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._radioREST = new System.Windows.Forms.RadioButton();
            this._radioSOAP = new System.Windows.Forms.RadioButton();
            this._awsBucket = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._awsSecretAccessKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._awsAccessKeyId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.awsOpen = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.localOpen);
            this.groupBox1.Controls.Add(this._localRootDir);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Local File System";
            // 
            // localOpen
            // 
            this.localOpen.Location = new System.Drawing.Point(457, 50);
            this.localOpen.Name = "localOpen";
            this.localOpen.Size = new System.Drawing.Size(75, 23);
            this.localOpen.TabIndex = 2;
            this.localOpen.Text = "Open...";
            this.localOpen.UseVisualStyleBackColor = true;
            this.localOpen.Click += new System.EventHandler(this.localOpen_Click);
            // 
            // _localRootDir
            // 
            this._localRootDir.Location = new System.Drawing.Point(118, 20);
            this._localRootDir.Name = "_localRootDir";
            this._localRootDir.Size = new System.Drawing.Size(414, 20);
            this._localRootDir.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Root Directory:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this._awsBucket);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this._awsSecretAccessKey);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this._awsAccessKeyId);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.awsOpen);
            this.groupBox2.Location = new System.Drawing.Point(13, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 256);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Amazon S3";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this._radioPrivateOnly);
            this.groupBox4.Controls.Add(this._radioPublicRead);
            this.groupBox4.Controls.Add(this._radioPublicWrite);
            this.groupBox4.Location = new System.Drawing.Point(317, 104);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(214, 100);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Access Rights";
            // 
            // _radioPrivateOnly
            // 
            this._radioPrivateOnly.AutoSize = true;
            this._radioPrivateOnly.Checked = true;
            this._radioPrivateOnly.Location = new System.Drawing.Point(17, 23);
            this._radioPrivateOnly.Name = "_radioPrivateOnly";
            this._radioPrivateOnly.Size = new System.Drawing.Size(82, 17);
            this._radioPrivateOnly.TabIndex = 12;
            this._radioPrivateOnly.TabStop = true;
            this._radioPrivateOnly.Text = "Private Only";
            this._radioPrivateOnly.UseVisualStyleBackColor = true;
            // 
            // _radioPublicRead
            // 
            this._radioPublicRead.AutoSize = true;
            this._radioPublicRead.Location = new System.Drawing.Point(17, 46);
            this._radioPublicRead.Name = "_radioPublicRead";
            this._radioPublicRead.Size = new System.Drawing.Size(83, 17);
            this._radioPublicRead.TabIndex = 13;
            this._radioPublicRead.Text = "Public Read";
            this._radioPublicRead.UseVisualStyleBackColor = true;
            // 
            // _radioPublicWrite
            // 
            this._radioPublicWrite.AutoSize = true;
            this._radioPublicWrite.Location = new System.Drawing.Point(17, 69);
            this._radioPublicWrite.Name = "_radioPublicWrite";
            this._radioPublicWrite.Size = new System.Drawing.Size(82, 17);
            this._radioPublicWrite.TabIndex = 14;
            this._radioPublicWrite.Text = "Public Write";
            this._radioPublicWrite.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._radioREST);
            this.groupBox3.Controls.Add(this._radioSOAP);
            this.groupBox3.Location = new System.Drawing.Point(117, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 100);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Interface";
            // 
            // _radioREST
            // 
            this._radioREST.AutoSize = true;
            this._radioREST.Checked = true;
            this._radioREST.Location = new System.Drawing.Point(18, 23);
            this._radioREST.Name = "_radioREST";
            this._radioREST.Size = new System.Drawing.Size(54, 17);
            this._radioREST.TabIndex = 9;
            this._radioREST.TabStop = true;
            this._radioREST.Text = "REST";
            this._radioREST.UseVisualStyleBackColor = true;
            // 
            // _radioSOAP
            // 
            this._radioSOAP.AutoSize = true;
            this._radioSOAP.Location = new System.Drawing.Point(18, 46);
            this._radioSOAP.Name = "_radioSOAP";
            this._radioSOAP.Size = new System.Drawing.Size(54, 17);
            this._radioSOAP.TabIndex = 10;
            this._radioSOAP.Text = "SOAP";
            this._radioSOAP.UseVisualStyleBackColor = true;
            // 
            // _awsBucket
            // 
            this._awsBucket.Location = new System.Drawing.Point(117, 71);
            this._awsBucket.Name = "_awsBucket";
            this._awsBucket.Size = new System.Drawing.Size(414, 20);
            this._awsBucket.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Bucket:";
            // 
            // _awsSecretAccessKey
            // 
            this._awsSecretAccessKey.Location = new System.Drawing.Point(117, 45);
            this._awsSecretAccessKey.Name = "_awsSecretAccessKey";
            this._awsSecretAccessKey.Size = new System.Drawing.Size(414, 20);
            this._awsSecretAccessKey.TabIndex = 6;
            this._awsSecretAccessKey.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Secret Access Key:";
            // 
            // _awsAccessKeyId
            // 
            this._awsAccessKeyId.Location = new System.Drawing.Point(117, 19);
            this._awsAccessKeyId.Name = "_awsAccessKeyId";
            this._awsAccessKeyId.Size = new System.Drawing.Size(414, 20);
            this._awsAccessKeyId.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Access Key ID:";
            // 
            // awsOpen
            // 
            this.awsOpen.Location = new System.Drawing.Point(456, 219);
            this.awsOpen.Name = "awsOpen";
            this.awsOpen.Size = new System.Drawing.Size(75, 23);
            this.awsOpen.TabIndex = 0;
            this.awsOpen.Text = "Open...";
            this.awsOpen.UseVisualStyleBackColor = true;
            this.awsOpen.Click += new System.EventHandler(this.awsOpen_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 364);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainWindow";
            this.Text = "File Manager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button localOpen;
        private System.Windows.Forms.TextBox _localRootDir;
        private System.Windows.Forms.Button awsOpen;
        private System.Windows.Forms.TextBox _awsBucket;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _awsSecretAccessKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _awsAccessKeyId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton _radioPublicWrite;
        private System.Windows.Forms.RadioButton _radioPublicRead;
        private System.Windows.Forms.RadioButton _radioPrivateOnly;
        private System.Windows.Forms.RadioButton _radioREST;
        private System.Windows.Forms.RadioButton _radioSOAP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

