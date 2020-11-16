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

        private Rectangle adjusted;
        private double coefficient;

        private HitboxAdjuster()
        {
            inputX = 0;
            inputY = 0;
            outputX = 0;
            outputY = 0;
            adjusted = new Rectangle();
            coefficient = 0;
        }

        public Rectangle AdjustHitbox(Rectangle input, float coefficient)
        {
            Rectangle adjusted = new Rectangle();
            Point inputSize = input.Size;
            Point inputLocation = input.Location;
            inputSizeX = inputSize.X;
            inputSizeY = inputSize.Y;

            outputSizeX = (int) ((float)inputX * coefficient);
            outputSizeY = (int)((float)inputY * coefficient);
            Point outputSize = new Point(outputX, outputY);
            adjusted = new Rectangle();

            return adjusted;
        }




    }
}
