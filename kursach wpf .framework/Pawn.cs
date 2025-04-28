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
        public Pawn(bool color) : base(color, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {
            
        }
        public Pawn(bool color, int x, int y) : base(color, x,y, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(Board board) 
        {
            if (board.ArrFigure[X, Y].Color)
            board.AddMarker(X, Y-1, board.ArrFigure[X, Y].Color);
        }
    }
}
