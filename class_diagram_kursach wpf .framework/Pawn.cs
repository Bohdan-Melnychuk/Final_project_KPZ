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
        public bool FirstMove = true;
        public Pawn(bool color) : base(color, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Pawn(bool color, int x, int y, Canvas myCanvas) : base(color, x, y, $"pack://application:,,,/Image/Pawn {(color ? "White" : "Black")} Outline 288px.png")
        {
            MyCanvas = myCanvas;
        }
        public override bool IsAttacking(int targetX, int targetY)
        {
            int direction = Color ? -1 : 1;

            return (targetX == X + 1 && targetY == Y + direction) ||
                   (targetX == X - 1 && targetY == Y + direction);
        }
        public override void MoveFigure()
        {
            if (!board.IsKingInCheck(Color)) 
            {
                StandardMoves();
                return;
            }


            DefensiveMoves();
        }
        private void DefensiveMoves()
        {
            var king = board.GetKing(Color);
            var attackers = board.GetAttackers(king.X, king.Y, Color);

            if (attackers.Count > 1)
                return;

            var attacker = attackers[0];
            int direction = (Color) ? -1 : 1;

            if (Math.Abs(attacker.X - X) == 1 && attacker.Y == Y + direction)
            {
                board.AddMarker(attacker.X, attacker.Y, Color);
            }

            if (attacker is Rook || attacker is Bishop || attacker is Queen)
            {
                var path = board.GetPathBetween(attacker.X, attacker.Y, king.X, king.Y);
                foreach (var (x, y) in path)
                {
                    if (x == X && board.ArrFigure[x, y] == null)
                    {
                        if (y == Y + direction || (FirstMove && y == Y + 2 * direction && board.ArrFigure[X, Y + direction] == null))
                        {
                            board.AddMarker(x, y, Color);
                        }
                    }
                }
            }
        }
        public override void FillArrayAttacksCell()
        {
            int direction = (Color) ? -1 : 1;

            if (X + 1 < board.boardSize && Y + direction >= 0 && Y + direction < board.boardSize)
            {
                CheckCell(X + 1, Y + direction);
            }

            if (X - 1 >= 0 && Y + direction >= 0 && Y + direction < board.boardSize)
            {
                CheckCell(X - 1, Y + direction);
            }
        
        }
        private void StandardMoves()
        {
            int direction = (Color) ? -1 : 1; 

            if ((Y + direction >= 0 && Y + direction < board.boardSize) && board.ArrFigure[X, Y + direction] == null)
            {
                board.AddMarker(X, Y + direction, Color);

                if (FirstMove && (Y + 2 * direction >= 0 && Y + 2 * direction < board.boardSize) &&
                    board.ArrFigure[X, Y + 2 * direction] == null)
                {
                    board.AddMarker(X, Y + 2 * direction, Color);
                }
            }

            if ((X + 1 < board.boardSize) && (Y + direction >= 0) && (Y + direction < board.boardSize) &&
                board.ArrFigure[X + 1, Y + direction] != null &&
                board.ArrFigure[X + 1, Y + direction].Color != Color)
            {
                board.AddMarker(X + 1, Y + direction, Color);
            }

            if ((X - 1 >= 0) && (Y + direction >= 0) && (Y + direction < board.boardSize) &&
                board.ArrFigure[X - 1, Y + direction] != null &&
                board.ArrFigure[X - 1, Y + direction].Color != Color)
            {
                board.AddMarker(X - 1, Y + direction, Color);
            }

            if (Y == 0 ||  Y == board.boardSize - 1)
            {
                HandlePawnPromotion();
            }
        }
        private async Task HandlePawnPromotion()
        {
            var choosingFigure = new ChoosingFigure(Color, MyCanvas);
            string result = await choosingFigure.WaitForSelectionAsync();
            MyCanvas.Children.Remove(board.ArrFigure[X, Y].ImageFigure);
            switch (result)
            {
                case "Queen":
                    board.ArrFigure[X, Y] = new Queen(Color, X,Y);
                    break;

                case "Rook":
                    board.ArrFigure[X, Y] = new Rook(Color, X, Y);

                    break;

                case "Bishop":
                    board.ArrFigure[X, Y] = new Bishop(Color, X, Y);

                    break;

                case "Knight":
                    board.ArrFigure[X, Y] = new Knight(Color, X, Y);

                    break;

            }
            board.ArrFigure[X, Y].board = board;
            board.AddFigure(board.ArrFigure[X, Y]);
        }
    }
}
