namespace ch.ph.AmazonUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uiAccessKeyID = new System.Windows.Forms.TextBox();
            this.uiSecretAccessKey = new System.Windows.Forms.TextBox();
            this.btEnumBuckets = new System.Windows.Forms.Button();
            this.uiBucket = new System.Windows.Forms.TextBox();
            this.uiObject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btCreateBucket = new System.Windows.Forms.Button();
            this.btDeleteBucket = new System.Windows.Forms.Button();
            this.btCreateObject = new System.Windows.Forms.Button();
            this.btDeleteObject = new System.Windows.Forms.Button();
            this.uiBucketList = new System.Windows.Forms.ListBox();
            this.uiObjectList = new System.Windows.Forms.ListBox();
            this.btListBucket = new System.Windows.Forms.Button();
            this.btGetObject = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.uiPrefix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.uiMarker = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.uiDelimiter = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uiMenuConn = new System.Windows.Forms.ToolStripMenuItem();
            this.uiMenuSoap = new System.Windows.Forms.ToolStripMenuItem();
            this.uiMenuRest = new System.Windows.Forms.ToolStripMenuItem();
            this.uiMenuRight = new System.Windows.Forms.ToolStripMenuItem();
            this.uiMenuRightPrivate = new System.Windows.Forms.ToolStripMenuItem();
            this.uiMenuRightPublicRead = new System.Windows.Forms.ToolStripMenuItem();
            this.uiMenuRightPublicWrite = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Access Key ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Secret Access Key";
            // 
            // uiAccessKeyID
            // 
            this.uiAccessKeyID.Location = new System.Drawing.Point(119, 27);
            this.uiAccessKeyID.Name = "uiAccessKeyID";
            this.uiAccessKeyID.Size = new System.Drawing.Size(340, 20);
            this.uiAccessKeyID.TabIndex = 2;
            this.uiAccessKeyID.TextChanged += new System.EventHandler(this.uiAccessKeyID_TextChanged);
            // 
            // uiSecretAccessKey
            // 
            this.uiSecretAccessKey.Location = new System.Drawing.Point(119, 54);
            this.uiSecretAccessKey.Name = "uiSecretAccessKey";
            this.uiSecretAccessKey.Size = new System.Drawing.Size(340, 20);
            this.uiSecretAccessKey.TabIndex = 3;
            this.uiSecretAccessKey.UseSystemPasswordChar = true;
            this.uiSecretAccessKey.TextChanged += new System.EventHandler(this.uiSecretAccessKey_TextChanged);
            // 
            // btEnumBuckets
            // 
            this.btEnumBuckets.Location = new System.Drawing.Point(465, 52);
            this.btEnumBuckets.Name = "btEnumBuckets";
            this.btEnumBuckets.Size = new System.Drawing.Size(99, 23);
            this.btEnumBuckets.TabIndex = 4;
            this.btEnumBuckets.Text = "Enum Buckets";
            this.btEnumBuckets.UseVisualStyleBackColor = true;
            this.btEnumBuckets.Click += new System.EventHandler(this.btEnumBuckets_Click);
            // 
            // uiBucket
            // 
            this.uiBucket.Location = new System.Drawing.Point(119, 81);
            this.uiBucket.Name = "uiBucket";
            this.uiBucket.Size = new System.Drawing.Size(340, 20);
            this.uiBucket.TabIndex = 5;
            // 
            // uiObject
            // 
            this.uiObject.Location = new System.Drawing.Point(119, 175);
            this.uiObject.Name = "uiObject";
            this.uiObject.Size = new System.Drawing.Size(340, 20);
            this.uiObject.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Bucket";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Object";
            // 
            // btCreateBucket
            // 
            this.btCreateBucket.Location = new System.Drawing.Point(465, 79);
            this.btCreateBucket.Name = "btCreateBucket";
            this.btCreateBucket.Size = new System.Drawing.Size(99, 23);
            this.btCreateBucket.TabIndex = 9;
            this.btCreateBucket.Text = "Create Bucket";
            this.btCreateBucket.UseVisualStyleBackColor = true;
            this.btCreateBucket.Click += new System.EventHandler(this.btCreateBucket_Click);
            // 
            // btDeleteBucket
            // 
            this.btDeleteBucket.Location = new System.Drawing.Point(570, 79);
            this.btDeleteBucket.Name = "btDeleteBucket";
            this.btDeleteBucket.Size = new System.Drawing.Size(99, 23);
            this.btDeleteBucket.TabIndex = 10;
            this.btDeleteBucket.Text = "Delete Bucket";
            this.btDeleteBucket.UseVisualStyleBackColor = true;
            this.btDeleteBucket.Click += new System.EventHandler(this.btDeleteBucket_Click);
            // 
            // btCreateObject
            // 
            this.btCreateObject.Location = new System.Drawing.Point(465, 173);
            this.btCreateObject.Name = "btCreateObject";
            this.btCreateObject.Size = new System.Drawing.Size(98, 23);
            this.btCreateObject.TabIndex = 11;
            this.btCreateObject.Text = "Create Object";
            this.btCreateObject.UseVisualStyleBackColor = true;
            this.btCreateObject.Click += new System.EventHandler(this.btCreateObject_Click);
            // 
            // btDeleteObject
            // 
            this.btDeleteObject.Location = new System.Drawing.Point(570, 173);
            this.btDeleteObject.Name = "btDeleteObject";
            this.btDeleteObject.Size = new System.Drawing.Size(98, 23);
            this.btDeleteObject.TabIndex = 12;
            this.btDeleteObject.Text = "Delete Object";
            this.btDeleteObject.UseVisualStyleBackColor = true;
            this.btDeleteObject.Click += new System.EventHandler(this.btDeleteObject_Click);
            // 
            // uiBucketList
            // 
            this.uiBucketList.FormattingEnabled = true;
            this.uiBucketList.Location = new System.Drawing.Point(119, 111);
            this.uiBucketList.Name = "uiBucketList";
            this.uiBucketList.Size = new System.Drawing.Size(340, 56);
            this.uiBucketList.TabIndex = 13;
            this.uiBucketList.SelectedIndexChanged += new System.EventHandler(this.uiBucketList_SelectedIndexChanged);
            // 
            // uiObjectList
            // 
            this.uiObjectList.FormattingEnabled = true;
            this.uiObjectList.Location = new System.Drawing.Point(119, 233);
            this.uiObjectList.Name = "uiObjectList";
            this.uiObjectList.Size = new System.Drawing.Size(340, 160);
            this.uiObjectList.TabIndex = 14;
            this.uiObjectList.SelectedIndexChanged += new System.EventHandler(this.uiObjectList_SelectedIndexChanged);
            // 
            // btListBucket
            // 
            this.btListBucket.Location = new System.Drawing.Point(465, 202);
            this.btListBucket.Name = "btListBucket";
            this.btListBucket.Size = new System.Drawing.Size(98, 23);
            this.btListBucket.TabIndex = 15;
            this.btListBucket.Text = "Find Objects";
            this.btListBucket.UseVisualStyleBackColor = true;
            this.btListBucket.Click += new System.EventHandler(this.btListBucket_Click);
            // 
            // btGetObject
            // 
            this.btGetObject.Location = new System.Drawing.Point(570, 201);
            this.btGetObject.Name = "btGetObject";
            this.btGetObject.Size = new System.Drawing.Size(98, 23);
            this.btGetObject.TabIndex = 16;
            this.btGetObject.Text = "Get Object";
            this.btGetObject.UseVisualStyleBackColor = true;
            this.btGetObject.Click += new System.EventHandler(this.btGetObject_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(116, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Prefix";
            // 
            // uiPrefix
            // 
            this.uiPrefix.Location = new System.Drawing.Point(155, 204);
            this.uiPrefix.Name = "uiPrefix";
            this.uiPrefix.Size = new System.Drawing.Size(94, 20);
            this.uiPrefix.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "After";
            // 
            // uiMarker
            // 
            this.uiMarker.Location = new System.Drawing.Point(290, 203);
            this.uiMarker.Name = "uiMarker";
            this.uiMarker.Size = new System.Drawing.Size(89, 20);
            this.uiMarker.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(385, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Delimiter";
            // 
            // uiDelimiter
            // 
            this.uiDelimiter.Location = new System.Drawing.Point(438, 203);
            this.uiDelimiter.Name = "uiDelimiter";
            this.uiDelimiter.Size = new System.Drawing.Size(20, 20);
            this.uiDelimiter.TabIndex = 22;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiMenuConn,
            this.uiMenuRight});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(681, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // uiMenuConn
            // 
            this.uiMenuConn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiMenuSoap,
            this.uiMenuRest});
            this.uiMenuConn.Name = "uiMenuConn";
            this.uiMenuConn.Size = new System.Drawing.Size(73, 20);
            this.uiMenuConn.Text = "Connection";
            // 
            // uiMenuSoap
            // 
            this.uiMenuSoap.Name = "uiMenuSoap";
            this.uiMenuSoap.Size = new System.Drawing.Size(152, 22);
            this.uiMenuSoap.Text = "SOAP";
            this.uiMenuSoap.Click += new System.EventHandler(this.uiMenuSoap_Click);
            // 
            // uiMenuRest
            // 
            this.uiMenuRest.Name = "uiMenuRest";
            this.uiMenuRest.Size = new System.Drawing.Size(152, 22);
            this.uiMenuRest.Text = "REST";
            this.uiMenuRest.Click += new System.EventHandler(this.uiMenuRest_Click);
            // 
            // uiMenuRight
            // 
            this.uiMenuRight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiMenuRightPrivate,
            this.uiMenuRightPublicRead,
            this.uiMenuRightPublicWrite});
            this.uiMenuRight.Name = "uiMenuRight";
            this.uiMenuRight.Size = new System.Drawing.Size(49, 20);
            this.uiMenuRight.Text = "Rights";
            // 
            // uiMenuRightPrivate
            // 
            this.uiMenuRightPrivate.Name = "uiMenuRightPrivate";
            this.uiMenuRightPrivate.Size = new System.Drawing.Size(133, 22);
            this.uiMenuRightPrivate.Text = "Private Only";
            this.uiMenuRightPrivate.Click += new System.EventHandler(this.uiMenuRightPrivate_Click);
            // 
            // uiMenuRightPublicRead
            // 
            this.uiMenuRightPublicRead.Name = "uiMenuRightPublicRead";
            this.uiMenuRightPublicRead.Size = new System.Drawing.Size(133, 22);
            this.uiMenuRightPublicRead.Text = "Public Read";
            this.uiMenuRightPublicRead.Click += new System.EventHandler(this.uiMenuRightPublicRead_Click);
            // 
            // uiMenuRightPublicWrite
            // 
            this.uiMenuRightPublicWrite.Name = "uiMenuRightPublicWrite";
            this.uiMenuRightPublicWrite.Size = new System.Drawing.Size(133, 22);
            this.uiMenuRightPublicWrite.Text = "Public Write";
            this.uiMenuRightPublicWrite.Click += new System.EventHandler(this.uiMenuRightPublicWrite_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 421);
            this.Controls.Add(this.uiDelimiter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.uiMarker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.uiPrefix);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btGetObject);
            this.Controls.Add(this.btListBucket);
            this.Controls.Add(this.uiObjectList);
            this.Controls.Add(this.uiBucketList);
            this.Controls.Add(this.btDeleteObject);
            this.Controls.Add(this.btCreateObject);
            this.Controls.Add(this.btDeleteBucket);
            this.Controls.Add(this.btCreateBucket);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uiObject);
            this.Controls.Add(this.uiBucket);
            this.Controls.Add(this.btEnumBuckets);
            this.Controls.Add(this.uiSecretAccessKey);
            this.Controls.Add(this.uiAccessKeyID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Amazon S3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uiAccessKeyID;
        private System.Windows.Forms.TextBox uiSecretAccessKey;
        private System.Windows.Forms.Button btEnumBuckets;
        private System.Windows.Forms.TextBox uiBucket;
        private System.Windows.Forms.TextBox uiObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btCreateBucket;
        private System.Windows.Forms.Button btDeleteBucket;
        private System.Windows.Forms.Button btCreateObject;
        private System.Windows.Forms.Button btDeleteObject;
        private System.Windows.Forms.ListBox uiBucketList;
        private System.Windows.Forms.ListBox uiObjectList;
        private System.Windows.Forms.Button btListBucket;
        private System.Windows.Forms.Button btGetObject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox uiPrefix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox uiMarker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox uiDelimiter;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uiMenuConn;
        private System.Windows.Forms.ToolStripMenuItem uiMenuRest;
        private System.Windows.Forms.ToolStripMenuItem uiMenuSoap;
        private System.Windows.Forms.ToolStripMenuItem uiMenuRight;
        private System.Windows.Forms.ToolStripMenuItem uiMenuRightPrivate;
        private System.Windows.Forms.ToolStripMenuItem uiMenuRightPublicRead;
        private System.Windows.Forms.ToolStripMenuItem uiMenuRightPublicWrite;
    }
}

