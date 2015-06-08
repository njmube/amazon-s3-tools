using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using ch.ph.AmazonWS;

namespace ch.ph.AmazonUI
{
    public partial class Form1 : Form
    {
        RegistryKey             _registry;
        private IStorageService _service;
        private AWSGrant        _rights;

        public Form1() 
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

            _registry = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("ch.ph.AmazonUI");

            _service = new SoapStorageService();
            uiMenuSoap.Checked = true;
            uiMenuRest.Checked = false;

            _rights = AWSGrant.PrivateOnly;
            uiMenuRightPrivate.    Checked = true;
            uiMenuRightPublicRead. Checked = false;
            uiMenuRightPublicWrite.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e) {       
            uiAccessKeyID.Text     = _registry.GetValue("AccessKeyID")     as string;
            uiSecretAccessKey.Text = _registry.GetValue("SecretAccessKey") as string;
            uiBucket.Text          = _registry.GetValue("Bucket")          as string;
            uiDelimiter.Text       = "/";
        }

        private void Form1_FormClosing(object sender, EventArgs e) {
            _registry.SetValue("AccessKeyID",     uiAccessKeyID.Text);
            _registry.SetValue("SecretAccessKey", uiSecretAccessKey.Text);
            _registry.SetValue("Bucket",          uiBucket.Text);
        }

        private void btEnumBuckets_Click(object sender, EventArgs e) 
        {
            try {
                Cursor = Cursors.WaitCursor;

                string[] buckets = _service.EnumBuckets();

                uiBucketList.Items.Clear();
                foreach(string bucket in buckets) {
                    uiBucketList.Items.Add(bucket);
                }
                
            } catch(Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                Cursor = Cursors.Default;
            }
        }

        private void btCreateBucket_Click(object sender, EventArgs e) 
        {
            try {
                Cursor = Cursors.WaitCursor;

                _service.CreateBucket(uiBucket.Text, _rights);

            } catch(Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                Cursor = Cursors.Default;
            }
        }

        private void btDeleteBucket_Click(object sender, EventArgs e)
        {
            try {
                if(MessageBox.Show("Are you sure you want to delete the bucket '"+uiBucket.Text+"' with all its objects?", "Confirm Deletion", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes) {
                    return;
                }

                Cursor = Cursors.WaitCursor;

                _service.DeleteBucket(uiBucket.Text);

            } catch(Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                Cursor = Cursors.Default;
            }
        }

        private void btListBucket_Click(object sender, EventArgs e) 
        {
            try {
                Cursor = Cursors.WaitCursor;

                AWSBucket bucket = _service.ListBucket(uiBucket.Text, uiPrefix.Text, uiMarker.Text, 100, uiDelimiter.Text);

                uiObjectList.Items.Clear();
                foreach(AWSObject obj in bucket.Objects) {
                    uiObjectList.Items.Add(obj.Key + " (FILE "+((obj.Size+1023)>>10)+" KB)");
                }
                foreach(string pre in bucket.ObjectPrefixes) {
                    uiObjectList.Items.Add(pre + " (DIR)");
                }
                if(bucket.IsTruncated) {
                    uiObjectList.Items.Add("...");
                }
                
            } catch(Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                Cursor = Cursors.Default;
            }
        }

        private void btCreateObject_Click(object sender, EventArgs e) 
        {
            try {
                OpenFileDialog fd = new OpenFileDialog();
                if(fd.ShowDialog() != DialogResult.OK) {
                    return;
                }

                Cursor = Cursors.WaitCursor;

                int    pos  = fd.FileName.LastIndexOf(".");
                string ext  = (pos == -1) ? "" : fd.FileName.Substring(pos).ToLower();
                string mime = "application/binary";;

                if(ext == ".htm" || ext == ".html") {
                    mime = "text/html";
                } else if(ext == ".xml" || ext == ".xsd" || ext == ".xsl" || ext == ".xslt") {
                    mime = "text/xml";
                } else if(ext == ".txt") {
                    mime = "text/plain";
                } else if(ext == ".jpg" || ext == ".jpeg") {
                    mime = "image/jpeg";
                } else if(ext == ".pdf") {
                    mime = "application/pdf";
                }

                AWSMetadataEntry[] meta = new AWSMetadataEntry[2];
                meta[0] = new AWSMetadataEntry();
                meta[0].MetaKey   = "CoNtEnT-TyPe";
                meta[0].MetaValue = mime;
                meta[1] = new AWSMetadataEntry();
                meta[1].MetaKey   = "Z-Application";
                meta[1].MetaValue = "ch.ph.AmazonUI";

                byte[] data;
                using(FileStream stm = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read)) {
                    data = new byte[stm.Length];
                    stm.Read(data, 0, data.Length);
                }

                _service.CreateObject(uiBucket.Text, uiObject.Text, meta, data, _rights);

            } catch(Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                Cursor = Cursors.Default;
            }
        }

        private void btDeleteObject_Click(object sender, EventArgs e) 
        {
            try {
                if(MessageBox.Show("Are you sure you want to delete the object '"+uiObject.Text+"'?", "Confirm Deletion", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes) {
                    return;
                }

                Cursor = Cursors.WaitCursor;

                _service.DeleteObject(uiBucket.Text, uiObject.Text);

            } catch(Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                Cursor = Cursors.Default;
            }
        }

        private void btGetObject_Click(object sender, EventArgs e) 
        {
            try {
                SaveFileDialog fd = new SaveFileDialog();
                if(fd.ShowDialog() != DialogResult.OK) {
                    return;
                }

                Cursor = Cursors.WaitCursor;

                AWSObject obj = _service.GetObject(uiBucket.Text, uiObject.Text, true, true);

                using(FileStream stm = new FileStream(fd.FileName, FileMode.Create, FileAccess.Write)) {
                    stm.Write(obj.Data, 0, (int) obj.Size);
                }

            } catch(Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                Cursor = Cursors.Default;
            }

        }

        private void uiAccessKeyID_TextChanged(object sender, EventArgs e) {
            _service.AccessKeyId = uiAccessKeyID.Text;
        }

        private void uiSecretAccessKey_TextChanged(object sender, EventArgs e) {
            _service.SecretAccessKey = uiSecretAccessKey.Text;
        }

        private void uiBucketList_SelectedIndexChanged(object sender, EventArgs e) {
            if(uiBucketList.SelectedIndex != -1) {
                uiBucket.Text = (string) uiBucketList.Items[uiBucketList.SelectedIndex];
            }
        }

        private void uiObjectList_SelectedIndexChanged(object sender, EventArgs e) {
            if(uiObjectList.SelectedIndex != -1) {
                string text = (string) uiObjectList.Items[uiObjectList.SelectedIndex];
                if(text == "...") {
                    uiObject.Text = null;
                } else {
                    uiObject.Text = text.Substring(0, text.LastIndexOf('(') - 1);
                }
            }
        }

        private void uiMenuSoap_Click(object sender, EventArgs e) {
            uiMenuSoap.Checked = true;
            uiMenuRest.Checked = false;
            _service = new SoapStorageService();
            _service.AccessKeyId     = uiAccessKeyID.Text;
            _service.SecretAccessKey = uiSecretAccessKey.Text;
        }

        private void uiMenuRest_Click(object sender, EventArgs e) {
            uiMenuSoap.Checked = false;
            uiMenuRest.Checked = true;
            _service = new RestStorageService();
            _service.AccessKeyId     = uiAccessKeyID.Text;
            _service.SecretAccessKey = uiSecretAccessKey.Text;
        }

        private void uiMenuRightPrivate_Click(object sender, EventArgs e) {
            _rights = AWSGrant.PrivateOnly;
            uiMenuRightPrivate.    Checked = true;
            uiMenuRightPublicRead. Checked = false;
            uiMenuRightPublicWrite.Checked = false;
        }

        private void uiMenuRightPublicRead_Click(object sender, EventArgs e) {
            _rights = AWSGrant.PublicRead;
            uiMenuRightPrivate.    Checked = false;
            uiMenuRightPublicRead. Checked = true;
            uiMenuRightPublicWrite.Checked = false;
        }

        private void uiMenuRightPublicWrite_Click(object sender, EventArgs e) {
            _rights = AWSGrant.PublicWrite;
            uiMenuRightPrivate.    Checked = false;
            uiMenuRightPublicRead. Checked = false;
            uiMenuRightPublicWrite.Checked = true;
        }
    }
}