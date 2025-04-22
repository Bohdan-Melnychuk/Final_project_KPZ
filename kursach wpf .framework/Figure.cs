using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace kursach_wpf.framework
{
    public abstract class Figure
    {
        public Image ImageFigure = new Image();

        string Name;
        bool Color;
        public int X, Y;

        public Figure(string name, bool color, string patch)
        {
            Name = name;
            Color = color;
            ImageFigure.Source = new BitmapImage(new Uri(patch));
            ImageFigure.Width = ImageFigure.Height = 80;
            Canvas.SetZIndex(ImageFigure, 1);
        }
        public Figure(string name, bool color, int x, int y, string patch)
        {
            Name = name;
            Color = color;
            X = x; Y = y;
            ImageFigure.Source = new BitmapImage(new Uri(patch));
            ImageFigure.Width = ImageFigure.Height = 80;
            Canvas.SetZIndex(ImageFigure, 1);
        }
        public abstract void MoveFigure(List<Figure> white, List<Figure> black);
    }
}