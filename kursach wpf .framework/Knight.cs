using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Knight : Figure
    {
        public Knight(string name, bool color) : base(name, color, $"pack://application:,,,/Image/Knight {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Knight(string name, bool color, int x, int y) : base(name, color, x, y, $"pack://application:,,,/Image/Knight {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(List<Figure> white, List<Figure> black)
        {

        }
    }
}
