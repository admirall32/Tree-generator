using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Tree
    {
        public int X {get; private set; }
        public int Y { get; private set; }
        public int BaseBranchLenght { get; private set; }
        public int BaseBranchThickness { get; private set; }

        private Bitmap _bitmap;

        public Branch RootBranch { get; private set; }

        public Tree(int Width, int Height, int baseBranchLenght, int baseBranchThickness, int treeDepth)
        {
            X = Width /2;
            Y = Height;
            BaseBranchLenght = baseBranchLenght;
            BaseBranchThickness = baseBranchThickness;
            RootBranch = new Branch(X, Y, new Random().Next(85,95), baseBranchLenght, baseBranchThickness);
            _bitmap = new Bitmap(Width, Height);

            FillTree(RootBranch, treeDepth);
        }

        public void FillTree(Branch branch, int levelCount)
        {
            levelCount--;

            var rand = new Random();

            for (int i = 0; i <= rand.Next(2, 3); i++)
            {
                var pair = GetEndPosition(branch.StartX, branch.StartY, branch.Angle, branch.Lenght);
                var childBranch = new Branch(pair.Key, pair.Value, branch.Angle, (int)(branch.Lenght * 0.7), (int)(branch.Thickness * 0.7));
                branch.AddChild(childBranch);

                if (levelCount > 0)
                {
                    FillTree(childBranch, levelCount);
                }
            }
        }

        public Bitmap DrawTree()
        {
            var pair = GetEndPosition(RootBranch.StartX, RootBranch.StartY, RootBranch.Angle, RootBranch.Lenght);
            return DrawBranch(RootBranch, _bitmap, pair.Key, pair.Value);
        }

        private Bitmap DrawBranch(Branch branch, Bitmap bitmap, int parentBranchEndX, int parentBranchEndY)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen brownPen = new Pen(Color.SaddleBrown);
            Pen greenPen = new Pen(Color.LightGreen);
            brownPen.Width = branch.Thickness;
            greenPen.Width = branch.Thickness * 3;

            var pair = GetEndPosition(branch.StartX, branch.StartY, branch.Angle, branch.Lenght);

            if (branch.Childs.Count == 0)
            {
                graphics.DrawLine(greenPen, branch.StartX, branch.StartY, pair.Key, pair.Value);
            }
            else
            {
                graphics.DrawLine(brownPen, branch.StartX, branch.StartY, pair.Key, pair.Value);
            }

            foreach (var childBranch in branch.Childs)
            {
                DrawBranch(childBranch, bitmap, pair.Key, pair.Value);
            }
            
            return bitmap;
        }

        private KeyValuePair<int,int> GetEndPosition(int startX, int startY, int angle, int lenght)
        {
            double cos = Math.Cos(angle * Math.PI / 180.0);
            double sin = -1 * Math.Sin(angle * Math.PI / 180.0);
            
            int key = (int)(lenght * cos);
            int value = (int)(lenght * sin);
            key += startX;            
            value += startY;
            return new KeyValuePair<int, int>(key, value);
        }

        public string GetFullBranch(decimal number, Branch branch)
        {
            string text = "";
            var pair = GetEndPosition(branch.StartX, branch.StartY, branch.Angle, branch.Lenght);

            text += "\r\n" + branch.StartX + ";" + branch.StartY;
            text += "\r\n" + pair.Key + ";" + pair.Value;

            if (branch.Childs.Count > 0)
            {
                if (branch.Childs.Count >= number)
                {
                    text += GetFullBranch(number, branch.Childs[(int)number]);
                }
                else
                {
                    text += GetFullBranch(number, branch.Childs.First());
                }
            }

            return text;
        }
    }
}
