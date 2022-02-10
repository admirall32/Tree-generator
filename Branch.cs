using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Branch
    {
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int Angle { get; private set; }
        public int Lenght { get; private set; }
        public int Thickness { get; private set; }
        public List<Branch> Childs { get; private set; }

        public Branch(int startX, int startY, int angle, int lenght, int thickness)
        {
            StartX = startX;
            StartY = startY;
            Angle = angle;
            Lenght = lenght;
            Thickness = thickness;
            Childs = new List<Branch>();

            if (lenght < 10)
            {
                Lenght = 10;
            }

            if (thickness < 1)
            {
                Thickness = 1;
            }
        }

        public void AddChild(Branch childBranch)
        {
            Childs.Add(childBranch);
        }
    }    
}
