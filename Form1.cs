using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tree
{
    public partial class Form1 : Form
    {
        Tree tree;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int treeHeight = (pictureBox1.Height < pictureBox1.Width ? pictureBox1.Height : pictureBox1.Width)/4;
            int treeWidth = (pictureBox1.Height < pictureBox1.Width ? pictureBox1.Height : pictureBox1.Width) /40;

            tree = new Tree(pictureBox1.Width, pictureBox1.Height, treeHeight, treeWidth, 9);
            pictureBox1.Image = tree.DrawTree();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Logs.Text = tree.GetFullBranch(numericUpDown1.Value, tree.RootBranch);
        }
    }
}
