using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ch.ph.FileSystem;

namespace ch.ph.FileManager
{
    public partial class CreateDirectory : Form
    {
        private readonly IDirectory _dir;

        public CreateDirectory(IDirectory dir) 
        {
            _dir = dir;
            InitializeComponent();
            _location.Text = _dir.FullName;
            _name.Select();
        }

        private void _btCreate_Click(object sender, EventArgs e) 
        {    
            try {
                _dir.CreateDirectory(_name.Text);
                Close();

            } catch(Exception ex) {
                MessageBox.Show("Failed to create directory '" + _name.Text + "'.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}