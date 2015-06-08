using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ch.ph.FileManager
{
    public partial class MainWindow : Form
    {
        private readonly RegistryKey _reg;
        private readonly Clipboard   _clip;

        public MainWindow() 
        {
            _reg  = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("ch.ph.FileManager");
            _clip = new Clipboard();

            InitializeComponent();
            FormClosing += new FormClosingEventHandler(this_FormClosing);

            _localRootDir.Text       = _reg.GetValue("LocalRootDir")       as string;
            _awsAccessKeyId.Text     = _reg.GetValue("AwsAccessKeyId")     as string;
            _awsSecretAccessKey.Text = _reg.GetValue("AwsSecretAccessKey") as string;
            _awsBucket.Text          = _reg.GetValue("AwsBucket")          as string;
        }

        private void this_FormClosing(object sender, EventArgs e) {
            _reg.SetValue("LocalRootDir",       _localRootDir.Text);
            _reg.SetValue("AwsAccessKeyId",     _awsAccessKeyId.Text);
            _reg.SetValue("AwsSecretAccessKey", _awsSecretAccessKey.Text);
            _reg.SetValue("AwsBucket",          _awsBucket.Text);
        }

        private void localOpen_Click(object sender, EventArgs e) 
        {
            Form form = new TreeWindow(_clip, new ch.ph.FileSystem.LocalFileSystem.FileSystem(_localRootDir.Text));
            form.Show();
        }

        private void awsOpen_Click(object sender, EventArgs e) 
        {
            string svc = null;
            if(_radioREST.Checked) {
                svc = "REST";
            } else if(_radioSOAP.Checked) {
                svc = "SOAP";
            }

            string acl = null;
            if(_radioPrivateOnly.Checked) {
                acl = "PrivateOnly";
            } else if(_radioPublicRead.Checked) {
                acl = "PublicRead";
            } else if(_radioPublicWrite.Checked) {
                acl = "PublicWrite";
            }

            Form form = new TreeWindow(_clip, new ch.ph.FileSystem.AmazonS3.FileSystem(svc, _awsAccessKeyId.Text, _awsSecretAccessKey.Text, _awsBucket.Text, acl));
            form.Show();
        }
    }
}