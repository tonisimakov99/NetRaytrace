using System;
using System.CodeDom;
using System.IO;

namespace RayTracer
{
    public class Color
    {
        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
        public static Color Black => new Color(0, 0, 0);
        public static Color Red => new Color(255, 0, 0);
        public static Color White  => new Color (255, 255, 255); 
        public static Color Green => new Color (0, 255, 0); 
        public static Color Blue  => new Color (0, 0, 255); 

        private int r;
        private int g;
        private int b;
        public int R
        {
            get => r; 
            set
            {
                if (value < 0 || 255 < value)
                    throw new Exception("0-255");
                r = value;
            }
        }
        public int G
        {
            get => g;
            set
            {
                if (value < 0 || 255 < value)
                    throw new Exception("0-255");
                g = value;
            }
        }
        public int B
        {
            get => b;
            set
            {
                if (value < 0 || 255 < value)
                    throw new Exception("0-255");
                b = value;
            }
        }
        public int Sum => R + G + B;

        public static Color operator *(Color color, float scalar)
        {
            return new Color(
              (int)Math.Max(Math.Min(color.R * scalar, 255), 0),
              (int)Math.Max(Math.Min(color.G * scalar, 255), 0),
              (int)Math.Max(Math.Min(color.B * scalar, 255), 0));
        }
        public static Color operator *(float scalar, Color color)
        {
            return new Color(
              (int)Math.Max(Math.Min(color.R * scalar, 255), 0),
              (int)Math.Max(Math.Min(color.G * scalar, 255), 0),
              (int)Math.Max(Math.Min(color.B * scalar, 255), 0));
        }

        public static Color operator *(Color color, Color other)
        {
            return new Color(
                color.R * other.R / 255,
                color.G * other.G / 255,
                color.B * other.B / 255);
        }

        public static Color operator -(Color color, Color other)
        {
            return new Color(
                color.R - other.R < 0 ? 0 : color.R - other.R, 
                color.G - other.G < 0 ? 0 : color.G - other.G, 
                color.B - other.B < 0 ? 0 : color.B - other.B);
        }
        public static Color operator +(Color color, Color other)
        {
            return new Color(
                color.R + other.R > 255 ? 255 : color.R + other.R, 
                color.G + other.G > 255 ? 255 : color.G + other.G, 
                color.B + other.B > 255 ? 255 : color.B + other.B);
        }
    }
}