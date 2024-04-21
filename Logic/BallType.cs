﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IBallType
    {
        public double X { get; set; }
        public double Y { get; set; }
        public (double moveX, double moveY) Direction { get; set; }


        public abstract void Move(int radius, int maxX, int maxY);

    }
}
