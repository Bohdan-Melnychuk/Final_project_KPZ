using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Linq;


namespace kursach_wpf.framework
{
    public class Pawn : Figure
    {
        public Canvas MyCanvas;
        bool FirstMove = true;
        Board Board;
        public Pawn(bool color) : base(color, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Pawn(bool color, int x, int y, Canvas myCanvas) : base(color, x, y, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {
            MyCanvas = myCanvas;
        }
        public override void MoveFigure(Board board)
        {
            Board = board;

            if ((Y - 1 >= 0 && Y - 1 < board.boardSize) && board.ArrFigure[X, Y - 1] == null)
            {
                board.AddMarker(X, Y - 1, board.ArrFigure[X, Y].Color);
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

            if (Y == 0)  
            {
                HandlePawnPromotion();
            }

        }
        private async Task HandlePawnPromotion()
        {
            var choosingFigure = new ChoosingFigure(Color, MyCanvas);
            string result = await choosingFigure.WaitForSelectionAsync();
            MyCanvas.Children.Remove(Board.ArrFigure[X, Y].ImageFigure);
            switch (result)
            {
                case "Queen":
                    Board.ArrFigure[X, Y] = new Queen(Color, X,Y);
                    break;

                case "Rook":
                    Board.ArrFigure[X, Y] = new Rook(Color, X, Y);

                    break;

                case "Bishop":
                    Board.ArrFigure[X, Y] = new Bishop(Color, X, Y);

                    break;

                case "Knight":
                    Board.ArrFigure[X, Y] = new Knight(Color, X, Y);

                    break;

            }
            Board.AddFigure(Board.ArrFigure[X, Y]);

        }
    }
}
