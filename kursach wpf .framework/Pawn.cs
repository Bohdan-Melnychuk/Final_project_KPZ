using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;


namespace kursach_wpf.framework
{
    public class Pawn : Figure
    {
        public Pawn(string name, bool color) : base(name, color, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {
            
        }
        public Pawn(string name, bool color, int x, int y) : base(name, color, x,y, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(List<Figure> white, List<Figure> black) 
        {
            bool WhiteP = white.FirstOrDefault(obj => obj.Y == this.Y - 1) != null;
            bool BlackP = black.FirstOrDefault(obj => obj.Y == this.Y - 1) != null;
        }
    }
}
