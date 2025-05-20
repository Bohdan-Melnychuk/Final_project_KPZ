using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace kursach_wpf.framework
{
    public class Rook : Figure
    {
        public Rook(bool color) : base(color, $"pack://application:,,,/Image/Rook {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Rook(bool color, int x, int y) : base(color, x, y, $"pack://application:,,,/Image/Rook {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override void MoveFigure(Board board)
        {
            for (int i = X + 1; i < board.boardSize; i++) // Вправо
            {
                if (board.AddMarker(i, Y, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = Y + 1; i < board.boardSize; i++) // Вниз
            {
                if (board.AddMarker(X, i, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = X - 1; i >= 0; i--) // Вліво
            {
                if (board.AddMarker(i, Y, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = Y - 1; i >= 0; i--) // Вверх
            {
                if (board.AddMarker(X, i, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
        }
    }
}
