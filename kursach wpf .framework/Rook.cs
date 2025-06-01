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
        public bool FirstMove = true;

        public Rook(bool color) : base(color, $"pack://application:,,,/Image/Rook {(color ? "White" : "Black")} Outline 288px.png")
        {
            
        }

        public Rook(bool color, int x, int y) : base(color, x, y, $"pack://application:,,,/Image/Rook {(color ? "White" : "Black")} Outline 288px.png")
        {
            
        }

        public override bool IsAttacking(int targetX, int targetY)
        {
            return (targetX == X || targetY == Y) && !IsBlockedPath(X, Y, targetX, targetY);
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
            ScanStraight(X, Y, 1, 0, true);   // Вправо
            ScanStraight(X, Y, -1, 0, true);  // Вліво
            ScanStraight(X, Y, 0, 1, true);   // Вниз
            ScanStraight(X, Y, 0, -1, true);  // Вверх
        }

        public override void FillArrayAttacksCell()
        {
            ScanStraight(X, Y, 1, 0);
            ScanStraight(X, Y, -1, 0);
            ScanStraight(X, Y, 0, 1);
            ScanStraight(X, Y, 0, -1);
        }
    }
}
