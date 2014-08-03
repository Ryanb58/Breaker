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

        Settings settings = new Settings();
        Utility utility = new Utility();

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            minutes.Maximum = 59;
            minutes.Minimum = 1;

            timerMain.Interval = settings.LoadInterval();
            minutes.Value = utility.miliSecondsToMinutes(settings.LoadInterval());
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
                timerMain.Enabled = false;
                startStopToolStripMenuItem.Text = "Start";
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
            timerMain.Dispose();
            sysTrayContextMenu.Dispose();
            sysTrayIcon.Dispose();
            Application.Exit();
        }

        private void startStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timerMain.Enabled == true)
            {
                timerMain.Stop();
                startStopToolStripMenuItem.Text = "Start";
            }
            else
            { 
                timerMain.Start();
                startStopToolStripMenuItem.Text = "Stop";
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            MessageBox.Show(settings.LoadMainMessage());
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            
            timerMain.Interval = utility.minutesToMiliseconds(Convert.ToInt32(minutes.Value.ToString()));
            settings.SaveInterval(Convert.ToInt32(minutes.Value));

            //Start the timer.
            timerMain.Start();
            startStopToolStripMenuItem.Text = "Stop";

            //Minimize the app.
            sysTrayIcon.Visible = true;
            this.Hide();
            WindowState = FormWindowState.Minimized;
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerMain.Dispose();
            sysTrayContextMenu.Dispose();
            sysTrayIcon.Dispose();
            Application.Exit();
        } 
    }
}
