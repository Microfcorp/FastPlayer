using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FastPlayer
{
    public partial class Controller : Form
    {
        SortedList<string, Form1> Players = new SortedList<string, Form1>();
        
        string Selected = 0.ToString();

        WMPLib.IWMPPlaylist dd = null;

        public Controller()
        {
            InitializeComponent();

            Players.Add(0.ToString(), new Form1());

            dd = Players[Selected].axWindowsMediaPlayer1.playlistCollection.newPlaylist("PlayList");
        }

        public Controller(string[] path) : this()
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            var dd = d.playlistCollection.newPlaylist("Selected");
            foreach (var item in path)
            {
                dd.appendItem(d.newMedia(item));
            }

            d.currentPlaylist = dd;

            Players[Selected].Show();
            d.Ctlcontrols.play();

            this.WindowState = FormWindowState.Minimized;
        }

        private void Controller_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Players.Add((Players.Count).ToString(), new Form1());
            comboBox1.Items.Add(Players.Count.ToString());
        }

        public void UpdateDisp()
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            if (d.playState == WMPLib.WMPPlayState.wmppsPlaying || d.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                trackBar1.Value = d.settings.volume;
                hScrollBar1.Maximum = (int)d.Ctlcontrols.currentItem.duration;
                hScrollBar1.Value = (int)d.Ctlcontrols.currentPosition;
                checkBox1.Checked = Players[Selected].Visible;
            }
            else
            {
                trackBar1.Value = d.settings.volume;
                hScrollBar1.Value = 0;
                checkBox1.Checked = Players[Selected].Visible;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Selected = comboBox1.SelectedIndex.ToString();
            UpdateDisp();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.Ctlcontrols.play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.Ctlcontrols.stop();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.Ctlcontrols.pause();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog opg = new OpenFileDialog();

            if(opg.ShowDialog() == DialogResult.OK)
            {
                var d = Players[Selected].axWindowsMediaPlayer1;
                d.URL = opg.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Players[Selected].TopMost = !Players[Selected].TopMost;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.fullScreen = !d.fullScreen;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                Players[Selected].Show();
            else
                Players[Selected].Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            UpdateDisp();
            timer1.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Players[Selected].Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Players[Selected].Hide();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.settings.volume = trackBar1.Value;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.Ctlcontrols.currentPosition = hScrollBar1.Value;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.Ctlcontrols.previous();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.Ctlcontrols.next();
        }

        private void ChangePl(PlayList pl)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            d.currentPlaylist = pl.p;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var d = Players[Selected].axWindowsMediaPlayer1;
            PlayList PL = new PlayList(dd, d);
            PL.ChangePL += ChangePl;
            PL.Show();
        }
    }
}
