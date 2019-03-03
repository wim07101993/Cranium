﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace Cranium.WPF.Helpers.Controls
{
    public static class ColorUtilities
    {
        private const double Tolerance = 0.0001;

        public static readonly Dictionary<string, System.Windows.Media.Color> KnownColors = GetKnownColors();

        public static string GetColorName(this System.Windows.Media.Color color)
        {
            var colorName = KnownColors
                .Where(kvp => kvp.Value.Equals(color))
                .Select(kvp => kvp.Key)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(colorName))
                colorName = color.ToString();

            return colorName;
        }

        public static string FormatColorString(string stringToFormat, bool isUsingAlphaChannel)
        {
            if (!isUsingAlphaChannel && stringToFormat.Length == 9)
                return stringToFormat.Remove(1, 2);
            return stringToFormat;
        }

        private static Dictionary<string, System.Windows.Media.Color> GetKnownColors()
        {
            var colorProperties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
            return colorProperties.ToDictionary(p => p.Name, p => (System.Windows.Media.Color)p.GetValue(null, null));
        }

        public static void ConvertRgbToHsv(int r, int b, int g, out double hue, out double saturation, out double value)
        {
            double h = 0, s;

            double min = Math.Min(Math.Min(r, g), b);
            double v = Math.Max(Math.Max(r, g), b);
            var delta = v - min;

            if (Math.Abs(v) < Tolerance)
                s = 0;
            else
                s = delta / v;

            if (Math.Abs(s) < Tolerance)
                h = 0.0;

            else
            {
                if (Math.Abs(r - v) < Tolerance)
                    h = (g - b) / delta;
                else if (Math.Abs(g - v) < Tolerance)
                    h = 2 + (b - r) / delta;
                else if (Math.Abs(b - v) < Tolerance)
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h < 0.0)
                    h = h + 360;
            }

            hue = h;
            saturation = s;
            value = v;
        }

        /// <summary>
        /// Converts an RGB color to an HSV color.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static HsvColor ConvertRgbToHsv(int r, int b, int g)
        {
            double h = 0, s;

            double min = Math.Min(Math.Min(r, g), b);
            double v = Math.Max(Math.Max(r, g), b);
            var delta = v - min;

            if (Math.Abs(v) < Tolerance)
                s = 0;
            else
                s = delta / v;

            if (Math.Abs(s) < Tolerance)
                h = 0.0;

            else
            {
                if (Math.Abs(r - v) < Tolerance)
                    h = (g - b) / delta;
                else if (Math.Abs(g - v) < Tolerance)
                    h = 2 + (b - r) / delta;
                else if (Math.Abs(b - v) < Tolerance)
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h < 0.0)
                    h = h + 360;
            }

            return new HsvColor
            {
                H = h,
                S = s,
                V = v / 255
            };
        }

        /// <summary>
        ///  Converts an HSV color to an RGB color.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Windows.Media.Color ConvertHsvToRgb(double h, double s, double v)
        {
            double r, g, b;

            if (Math.Abs(s) < Tolerance)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                if (Math.Abs(h - 360) < Tolerance)
                    h = 0;
                else
                    h = h / 60;

                var i = (int)Math.Truncate(h);
                var f = h - i;

                var p = v * (1.0 - s);
                var q = v * (1.0 - (s * f));
                var t = v * (1.0 - (s * (1.0 - f)));

                switch (i)
                {
                    case 0:
                    {
                        r = v;
                        g = t;
                        b = p;
                        break;
                    }
                    case 1:
                    {
                        r = q;
                        g = v;
                        b = p;
                        break;
                    }
                    case 2:
                    {
                        r = p;
                        g = v;
                        b = t;
                        break;
                    }
                    case 3:
                    {
                        r = p;
                        g = q;
                        b = v;
                        break;
                    }
                    case 4:
                    {
                        r = t;
                        g = p;
                        b = v;
                        break;
                    }
                    default:
                    {
                        r = v;
                        g = p;
                        b = q;
                        break;
                    }
                }
            }

            return System.Windows.Media.Color.FromArgb(255, (byte)Math.Round(r * 255), (byte)Math.Round(g * 255),
                (byte)Math.Round(b * 255));
        }

        /// <summary>
        /// Generates a list of colors with hues ranging from 0 360 and a saturation and value of 1. 
        /// </summary>
        /// <returns></returns>
        public static List<System.Windows.Media.Color> GenerateHsvSpectrum()
        {
            var colorsList = new List<System.Windows.Media.Color>(8);

            for (var i = 0; i < 29; i++)
                colorsList.Add(ConvertHsvToRgb(i * 12, 1, 1));

            colorsList.Add(ConvertHsvToRgb(0, 1, 1));

            return colorsList;
        }
    }
}
