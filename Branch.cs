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
            childBranch.Angle = CheckFreeAngle(Angle);
            Childs.Add(childBranch);
        }


        private int CheckFreeAngle(int parentAngle)
        {
            List<int> angles = GetCurrentAnglesList();
                        
            int minAngle = 0;
            int maxAngle = 0;

            for (int i = 1; i <= angles.Count -1; i++)
            {
                if (angles[i] - angles[i-1] > maxAngle - minAngle)
                {
                    minAngle = angles[i - 1];
                    maxAngle = angles[i];
                }
            }


            int angleToReturn = 0;

            angleToReturn = new Random().Next(minAngle, maxAngle) + parentAngle-90;
                        
            return angleToReturn;
        }

        private List<int> GetCurrentAnglesList()
        {
            List<int> angles = new List<int> { 30 };

            foreach (var childBranch in Childs)
            {
                angles.Add(childBranch.Angle);
            }

            angles.Add(150);

            if (angles.Count == 0)
            {
                angles.Clear();
                angles.Add(60);
                angles.Add(120);
            }
            return angles;
        }
    }    
}
