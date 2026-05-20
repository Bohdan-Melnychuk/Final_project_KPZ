using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach_wpf.framework
{
    public class Queen : SlidingFigure
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

        protected override void AddStandardMoves()
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
