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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //axWindowsMediaPlayer1
        }
        
        public Form1(string path) : this()
        {
            axWindowsMediaPlayer1.URL = path;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
           // this.WindowState = FormWindowState.Minimized;
        }
    }
}
