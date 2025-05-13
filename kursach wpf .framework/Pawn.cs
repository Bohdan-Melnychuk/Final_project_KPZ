using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;


namespace kursach_wpf.framework
{
    public class Pawn : Figure
    {
        bool FirstMove = true;

        public Pawn(bool color) : base(color, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {
            
        }
        public Pawn(bool color, int x, int y) : base(color, x,y, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(Board board)
        {
            if ((Y - 1 >= 0 && Y - 1 < board.boardSize) && board.ArrFigure[X, Y - 1] == null)
            {
                board.AddMarker(X, Y - 1, board.ArrFigure[X, Y].Color); //!
                if (FirstMove && (Y - 2 >= 0 && Y - 2 < board.boardSize) && board.ArrFigure[X, Y - 2] == null)
                {
                    FirstMove = false;
                    board.AddMarker(X, Y - 2, board.ArrFigure[X, Y].Color);
                }
            }

            if ((X + 1 >= 0 && X + 1 < board.boardSize) && (Y - 1 >= 0 && Y - 1 < board.boardSize) && board.ArrFigure[X + 1, Y - 1] != null)
            {
                board.AddMarker(X + 1, Y - 1, board.ArrFigure[X, Y].Color);
            }

            if ((X - 1 >= 0 && X - 1 < board.boardSize) && (Y - 1 >= 0 && Y - 1 < board.boardSize) && board.ArrFigure[X - 1, Y - 1] != null)
            {
                board.AddMarker(X - 1, Y - 1, board.ArrFigure[X, Y].Color);
            }
            // дописати зміну пішака на іншу фігуру коли він доходить до кінця жошки
        }
    }
}
