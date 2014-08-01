using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breaker
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                sysTrayIcon.Visible = true;
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                sysTrayIcon.Visible = false;
            }
        }

        private void sysTrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void restoreToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                sysTrayIcon.Visible = false;
                this.Show();
                WindowState = FormWindowState.Normal;

            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                sysTrayIcon.Visible = true;
                this.Hide();
                WindowState = FormWindowState.Minimized;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
