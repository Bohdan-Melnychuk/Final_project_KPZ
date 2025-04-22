using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Queen : Figure
    {
        public Queen(string name, bool color) : base(name, color, $"pack://application:,,,/Image/Queen {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Queen(string name, bool color, int x, int y) : base(name, color, x, y, $"pack://application:,,,/Image/Queen {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(List<Figure> white, List<Figure> black)
        {

        }
    }
}
