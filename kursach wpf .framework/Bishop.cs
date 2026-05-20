using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Bishop : SlidingFigure
    {
        public Bishop(bool color) : base(color, $"pack://application:,,,/Image/Bishop {(color ? "White" : "Black")} Outline 288px.png")
        {

        }

        public Bishop( bool color, int x, int y) : base( color, x, y, $"pack://application:,,,/Image/Bishop {(color ? "White" : "Black")} Outline 288px.png")
        {

        }

        public override bool IsAttacking(int targetX, int targetY)
        {
            return Math.Abs(targetX - X) == Math.Abs(targetY - Y) &&
                   !IsBlockedPath(X, Y, targetX, targetY);
        }

        protected override void AddStandardMoves()
        {
            ScanDiagonal(X, Y, 1, -1, true);  // Вправо-вверх
            ScanDiagonal(X, Y, -1, 1, true);  // Вліво-вниз
            ScanDiagonal(X, Y, -1, -1, true); // Вліво-вверх
            ScanDiagonal(X, Y, 1, 1, true);   // Вправо-вниз
        }

        public override void FillArrayAttacksCell()
        {
            ScanDiagonal(X, Y, 1, -1);
            ScanDiagonal(X, Y, -1, 1);
            ScanDiagonal(X, Y, -1, -1);
            ScanDiagonal(X, Y, 1, 1);
        }
    }
}
