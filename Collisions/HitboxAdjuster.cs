using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sprint4
{
    /// <summary>
    /// takes coefficient to reduce hitbox size by (to get from grid to closer matching the sprite's size) and returns new hitbox
    /// </summary>
    public class HitboxAdjuster
    {
        public static HitboxAdjuster Instance { get; } = new HitboxAdjuster();

        private int inputSizeX;
        private int inputSizeY;
        private int outputSizeX;
        private int outputSizeY;

        private int inputLocX;
        private int inputLocY;
        private int outputLocX;
        private int outputLocY;


        private int xDiff;
        private int yDiff;

        private Point inputSize;
        private Point inputLocation;

        private Point outputSize;
        private Point outputLocation;


        private Rectangle adjusted;
        private double coefficient;

        private HitboxAdjuster()
        {
        }

        public Rectangle AdjustHitbox(Rectangle input, float coefficient)
        {
           
            inputSize = input.Size;
            inputLocation = input.Location;

            inputLocX = inputLocation.X;
            inputLocY = inputLocation.Y;

            inputSizeX = inputSize.X;
            inputSizeY = inputSize.Y;

            outputSizeX = (int) ((float)inputSizeX * coefficient);
            outputSizeY = (int)((float)inputSizeY * coefficient);

            xDiff = (int)((inputSizeX - outputSizeX) / 2.0);
            yDiff = (int)((inputSizeY - outputSizeY) / 2.0);

            outputLocX = inputLocX + xDiff;
            outputLocY = inputLocY + yDiff;

            Point outputLocation = new Point(outputLocX, outputLocY);
            Point outputSize = new Point(outputSizeX, outputSizeY);

            adjusted = new Rectangle(outputLocation, outputSize);
          

            return adjusted;
        }




    }
}
