using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Knight : Figure
    {
        public Knight(bool color) : base(color, $"pack://application:,,,/Image/Knight {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Knight(bool color, int x, int y) : base(color, x, y, $"pack://application:,,,/Image/Knight {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(Board board)
        {

        }
    }
}
