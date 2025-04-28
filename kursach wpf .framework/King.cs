using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class King : Figure
    {
        public King(bool color) : base(color, $"pack://application:,,,/Image/King {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public King(bool color, int x, int y) : base(color, x, y, $"pack://application:,,,/Image/King {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(Board board)
        {

        }
    }
}
