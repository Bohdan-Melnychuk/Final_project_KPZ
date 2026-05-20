namespace kursach_wpf.framework
{
    public abstract class SlidingFigure : Figure
    {
        protected SlidingFigure(bool color, string patch) : base(color, patch)
        {
        }

        protected SlidingFigure(bool color, int x, int y, string patch) : base(color, x, y, patch)
        {
        }

        public override void MoveFigure()
        {
            if (!board.IsKingInCheck(Color))
            {
                AddStandardMoves();
                return;
            }

            AddDefensiveMoves();
        }

        protected abstract void AddStandardMoves();

        private void AddDefensiveMoves()
        {
            var king = board.GetKing(Color);
            var attackers = board.GetAttackers(king.X, king.Y, Color);

            if (attackers.Count > 1)
            {
                return;
            }

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
    }
}
