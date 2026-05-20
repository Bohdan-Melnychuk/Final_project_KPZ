using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace kursach_wpf.framework
{
    public class Rook : SlidingFigure
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

        protected override void AddStandardMoves()
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
