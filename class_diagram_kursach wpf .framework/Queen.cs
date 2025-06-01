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
        public override bool IsAttacking(int targetX, int targetY)
        {
            return ((targetX == X || targetY == Y) ||
                    (Math.Abs(targetX - X) == Math.Abs(targetY - Y))) &&
                    !IsBlockedPath(X, Y, targetX, targetY);
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

            if (CanReach(attacker.X, attacker.Y) &&
                (board.ArrFigure[attacker.X, attacker.Y] == null ||
                 board.ArrFigure[attacker.X, attacker.Y].Color != Color))
            {
                board.AddMarker(attacker.X, attacker.Y, Color);
            }

            if (attacker is Rook || attacker is Bishop || attacker is Queen)
            {
                var path = board.GetPathBetween(attacker.X, attacker.Y, king.X, king.Y);
                foreach (var (x, y) in path)
                {
                    if (CanReach(x, y) && board.ArrFigure[x, y] == null)
                    {
                        board.AddMarker(x, y, Color);
                    }
                }
            }
        }
        private void StandardMoves()
        {
            ScanDiagonal(X, Y, 1, -1, true);  // Вправо-вверх
            ScanDiagonal(X, Y, -1, 1, true);  // Вліво-вниз
            ScanDiagonal(X, Y, -1, -1, true); // Вліво-вверх
            ScanDiagonal(X, Y, 1, 1, true);   // Вправо-вниз

            ScanStraight(X, Y, 1, 0, true);   // Вправо
            ScanStraight(X, Y, -1, 0, true);  // Вліво
            ScanStraight(X, Y, 0, 1, true);   // Вниз
            ScanStraight(X, Y, 0, -1, true);  // Вверх
        }

        public override void FillArrayAttacksCell()
        {
            ScanDiagonal(X, Y, 1, -1);  
            ScanDiagonal(X, Y, -1, 1);  
            ScanDiagonal(X, Y, -1, -1);
            ScanDiagonal(X, Y, 1, 1);  

            ScanStraight(X, Y, 1, 0);  
            ScanStraight(X, Y, -1, 0);  
            ScanStraight(X, Y, 0, 1);   
            ScanStraight(X, Y, 0, -1);  
        }
    }
}
