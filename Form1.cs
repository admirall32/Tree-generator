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
        Bitmap bitmap;
        Graphics graphics;
        Pen brownPen;
        Tree tree;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tree = new Tree(pictureBox1.Width, pictureBox1.Height, 200, 1);
            pictureBox1.Image = tree.DrawTree();
        }

        private void DrawLine(int startX, int startY, int endX, int endY, int lineWidth = 1)
        {
            brownPen.Width = lineWidth;
            graphics.DrawLine(brownPen, startX, startY, endX, endY);
            pictureBox1.Image = bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logs.Text = tree.GetFullBranch(numericUpDown1.Value, tree.RootBranch);

        }

    }
}
