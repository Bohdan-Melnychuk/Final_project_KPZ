using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class King : Figure
    {
        public King(string name, bool color) : base(name, color, $"pack://application:,,,/Image/King {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public King(string name, bool color, int x, int y) : base(name, color, x, y, $"pack://application:,,,/Image/King {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(List<Figure> white, List<Figure> black)
        {

        }
    }
}
