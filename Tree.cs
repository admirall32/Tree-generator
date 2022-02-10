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

        public Tree(int Width, int Height, int baseBranchLenght, int baseBranchThickness)
        {
            X = Width /2;
            Y = Height;
            BaseBranchLenght = baseBranchLenght;
            BaseBranchThickness = baseBranchThickness;
            RootBranch = new Branch(X, Y, 90, baseBranchLenght, baseBranchThickness);
            _bitmap = new Bitmap(Width, Height);

            FillTree(RootBranch, 4);
        }

        public void FillTree(Branch branch, int levelCount)
        {
            levelCount--;

            var rand = new Random();

            for (int i = 0; i <= 1; i++)
            {
                var pair = GetEndPosition(branch.StartX, branch.StartY, branch.Angle, branch.Lenght);
                var childBranch = new Branch(pair.Key, pair.Value, branch.Angle + rand.Next(-10, 10) * (branch.Angle > 0 ? 1 : -1), (int)(branch.Lenght*0.7), (int)(branch.Thickness * 0.7));
                branch.AddChild(childBranch);

                if (levelCount > 0)
                {
                    FillTree(childBranch, levelCount);
                }
            }
        }

        public Bitmap DrawTree()
        {
            var bitmap = new Bitmap(_bitmap.Width, _bitmap.Height);
            var pair = GetEndPosition(RootBranch.StartX, RootBranch.StartY, RootBranch.Angle, RootBranch.Lenght);
            bitmap = DrawBranch(RootBranch, _bitmap, pair.Key, pair.Value);
            return bitmap;
        }

        private Bitmap DrawBranch(Branch branch, Bitmap bitmap, int parentBranchEndX, int parentBranchEndY)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen brownPen = new Pen(Color.Brown);

            brownPen.Width = branch.Thickness;

            var pair = GetEndPosition(branch.StartX, branch.StartY, branch.Angle, branch.Lenght);

            graphics.DrawLine(brownPen, branch.StartX, branch.StartY, pair.Key, pair.Value);

            foreach (var childBranch in branch.Childs)
            {
                DrawBranch(childBranch, bitmap, pair.Key, pair.Value);
            }
            
            return bitmap;
        }

        private KeyValuePair<int,int> GetEndPosition(int startX, int startY, int angle, int lenght)
        {
            int key = (int)(lenght * Math.Cos(angle));
            int value = (int)(lenght * Math.Sin(angle));
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
