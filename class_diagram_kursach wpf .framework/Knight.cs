using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Knight : Figure
    {

        public Knight(bool color) : base(color, $"pack://application:,,,/Image/Knight {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public Knight(bool color, int x, int y) : base(color, x, y, $"pack://application:,,,/Image/Knight {(color ? "White" : "Black")} Outline 288px.png")
        {

        }
        public override bool IsAttacking(int targetX, int targetY)
        {
            int dx = Math.Abs(targetX - X);
            int dy = Math.Abs(targetY - Y);
            return (dx == 1 && dy == 2) || (dx == 2 && dy == 1);
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

        private void StandardMoves()
        {
            board.AddMarker(X + 1, Y - 2, Color);
            board.AddMarker(X - 1, Y - 2, Color);

            board.AddMarker(X - 1, Y + 2, Color);
            board.AddMarker(X + 1, Y + 2, Color);

            board.AddMarker(X - 2, Y + 1, Color);
            board.AddMarker(X + 2, Y + 1, Color);

            board.AddMarker(X + 2, Y - 1, Color);
            board.AddMarker(X - 2, Y - 1, Color);
        }
        

        private void DefensiveMoves()
        {
            var king = board.GetKing(Color);
            var attackers = board.GetAttackers(king.X, king.Y, Color);

            if (attackers.Count > 1)
                return;

            var attacker = attackers[0];
            int direction = (Color) ? -1 : 1;

            if (attacker.X == X + 1 && attacker.Y == Y + direction)
                board.AddMarker(X + 1, Y + direction, Color);

            if (attacker.X == X - 1 && attacker.Y == Y + direction)
                board.AddMarker(X - 1, Y + direction, Color);

            if (attacker is Rook || attacker is Bishop || attacker is Queen)
            {
                var path = board.GetPathBetween(attacker.X, attacker.Y, king.X, king.Y);
                foreach (var (x, y) in path)
                {
                    if (CanMoveTo(x, y)) 
                        board.AddMarker(x, y, Color);
                }
            }
        }
        public override void FillArrayAttacksCell()
        {
            CheckCell(X + 1, Y - 2);
            CheckCell(X - 1, Y - 2);

            CheckCell(X - 1, Y + 2);
            CheckCell(X + 1, Y + 2);

            CheckCell(X - 2, Y + 1);
            CheckCell(X + 2, Y + 1);

            CheckCell(X + 2, Y - 1);
            CheckCell(X - 2, Y - 1);
        }
    }
}
