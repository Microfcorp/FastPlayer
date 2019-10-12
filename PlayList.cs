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
    public partial class PlayList : Form
    {
        public delegate void PlayL(PlayList pl);
        public event PlayL ChangePL;

        public WMPLib.IWMPPlaylist p;
        AxWMPLib.AxWindowsMediaPlayer mp;

        int selected = -1;

        public PlayList(WMPLib.IWMPPlaylist p, AxWMPLib.AxWindowsMediaPlayer mp)
        {
            InitializeComponent();

            this.p = p;
            this.mp = mp;

            ChangePL += idd;
        }

        private void idd(PlayList pl)
        {
            treeView1.Nodes.Clear();
            for (int i = 0; i < pl.p.count; i++)
            {               
                TreeNode tr = new TreeNode(pl.p.Item[i].name);
                tr.ContextMenuStrip = contextMenuStrip1;
                treeView1.Nodes.Add(tr);
            }
        }

        private void PlayList_Load(object sender, EventArgs e)
        {
            ChangePL?.Invoke(this);
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opg = new OpenFileDialog();

            if (opg.ShowDialog() == DialogResult.OK)
            {
                var d = mp.newMedia(opg.FileName);
                p.appendItem(d);
                ChangePL?.Invoke(this);
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.removeItem(p.Item[selected]);
            ChangePL?.Invoke(this);
        }

        private void вышеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.moveItem(selected, selected - 1);
            ChangePL?.Invoke(this);
        }

        private void нижеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p.moveItem(selected, selected + 1);
            ChangePL?.Invoke(this);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selected = e.Node.Index;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selected = e.Node.Index;
            mp.Ctlcontrols.playItem(p.Item[selected]);
        }
    }
}
