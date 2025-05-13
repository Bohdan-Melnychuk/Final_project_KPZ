using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Queen : Figure
    {
        public Queen(bool color) : base(color, $"pack://application:,,,/Image/Queen {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Queen(bool color, int x, int y) : base(color, x, y, $"pack://application:,,,/Image/Queen {(color ? "White" : "Black")} Outline 288px.png")
        {
    
        }
        public override void MoveFigure(Board board)
        {
            // Ходи як слон (по діагоналях)
            for (int i = X + 1, j = Y - 1; i < board.boardSize && j >= 0; i++, j--) // Вправо вверх
            {
                if (board.AddMarker(i, j, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = X - 1, j = Y + 1; i >= 0 && j < board.boardSize; i--, j++) // Вліво вниз
            {
                if (board.AddMarker(i, j, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = X - 1, j = Y - 1; i >= 0 && j >= 0; i--, j--) // Вліво вверх
            {
                if (board.AddMarker(i, j, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }
            for (int i = X + 1, j = Y + 1; i < board.boardSize && j < board.boardSize; i++, j++) // Вправо вниз
            {
                if (board.AddMarker(i, j, board.ArrFigure[X, Y].Color))
                {
                    break;
                }
            }

            // Ходи як тура (по вертикалі та горизонталі)
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
