using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        bool enableItemCheck = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            try {
            //Set max values
            minutes.Maximum = 59;
            minutes.Minimum = 1;

            //Load up the visable data for the UI.
            int interval = settings.LoadInterval();
            timerMain.Interval = interval;
            minutes.Value = utility.miliSecondsToMinutes(interval);

            //Load up the saved Active Days.
            var savedActiveDays = settings.LoadActiveDays();
            foreach(string day in savedActiveDays)
            {
                int index = ActiveDays.Items.IndexOf(day);

                if (index > 0)
                {
                    ActiveDays.SetItemChecked(index, true);
                }
            }

            enableItemCheck = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            bool showMessage = false;

            foreach (string day in settings.LoadActiveDays())
            {
                if (day == DateTime.Now.ToString("dddd"))
                {
                    showMessage = true;
                }
            }

            if (showMessage == true)
            {
                MessageBox.Show(settings.LoadMainMessage());
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            //Save the days of the week.
            //var items = ActiveDays.Items;
            //foreach(var item in items)
            //{
            //    if()
            //}

            //Save Minute Value in miliseconds.
            timerMain.Interval = utility.minutesToMiliseconds(Convert.ToInt32(minutes.Value.ToString()));
            settings.SaveInterval(utility.minutesToMiliseconds(Convert.ToInt32(minutes.Value)));

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

        private void ActiveDays_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (enableItemCheck == true)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    //settings.SaveActiveDays
                    //listBox1.Items.Add(checkedListBox1.Text);
                    settings.SaveActiveDay(e.CurrentValue.ToString());
                }

                else if (e.NewValue == CheckState.Unchecked)
                {
                    //listBox1.Items.Remove(checkedListBox1.Text);
                    settings.RemoveActiveDay(e.CurrentValue.ToString());
                }
            }
        }

        private void btnCancelSave_Click(object sender, EventArgs e)
        {
            timerMain.Dispose();
            sysTrayContextMenu.Dispose();
            sysTrayIcon.Dispose();
            Application.Exit();
        } 
    }
}
