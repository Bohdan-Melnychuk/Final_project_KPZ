using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Rook : Figure
    {
        public Rook(string name, bool color) : base(name, color, $"pack://application:,,,/Image/Rook {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Rook(string name, bool color, int x, int y) : base(name, color, x, y, $"pack://application:,,,/Image/Rook {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
    }
}
