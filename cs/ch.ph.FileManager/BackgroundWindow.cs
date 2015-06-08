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
    public partial class BackgroundWindow : Form
    {
        public BackgroundWindow(EventHandler cancelHandler) {
            InitializeComponent();
            _btCancel.Click += cancelHandler;
        }

        public string Line1 { 
            set { _line1.Text = value; } 
        }

        public string Line2 { 
            set { _line2.Text = value; } 
        }

        public string Line3 { 
            set { _line3.Text = value; } 
        }
    }
}