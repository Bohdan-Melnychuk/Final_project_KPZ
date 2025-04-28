using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Bishop : Figure
    {
        public Bishop(bool color) : base(color, $"pack://application:,,,/Image/Bishop {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Bishop( bool color, int x, int y) : base( color, x, y, $"pack://application:,,,/Image/Bishop {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(Board board)
        {
            for (int i = X + 1, j = Y - 1; i < board.boardSize; i++, j--) // вправо вверх
            {
                if (board.AddMarker(i, j, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = Y + 1, j = Y - 1; i < board.boardSize; i++, j--) // вліво вниз
            {
                if (board.AddMarker(j, i, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = X - 1; i >= 0; i--) // вліво вверх
            {
                if (board.AddMarker(i, i, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = Y + 1; i >= 0; i++) // вправо вниз
            {
                if (board.AddMarker(i, i, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
        }
    }
}
