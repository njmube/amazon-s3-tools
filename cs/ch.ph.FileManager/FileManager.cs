using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ch.ph.FileManager
{
    static class FileManager
    {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}