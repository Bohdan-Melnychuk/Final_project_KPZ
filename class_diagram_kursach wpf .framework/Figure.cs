using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace kursach_wpf.framework
{
    public abstract class Figure
    {
        public Board board;

        public Image ImageFigure = new Image();

        public List<(int, int)> CellsUnderAttack = new List<(int, int)>();
        public bool FigureProtection = false;

        public bool Color;
        public int X, Y;

        public Figure(bool color, string patch)
        {
            Color = color;
            ImageFigure.Source = new BitmapImage(new Uri(patch));
            ImageFigure.Width = ImageFigure.Height = 80;
            Canvas.SetZIndex(ImageFigure, 1);
        }
        public Figure(bool color, int x, int y, string patch)
        {
            Color = color;
            X = x; Y = y;
            ImageFigure.Source = new BitmapImage(new Uri(patch));
            ImageFigure.Width = ImageFigure.Height = 80;
            Canvas.SetZIndex(ImageFigure, 1);
        }
        public abstract bool IsAttacking(int targetX, int targetY);
        public abstract void MoveFigure();
        public abstract void FillArrayAttacksCell();
        public bool CheckCell(int x, int y)
        {
            if (!(x <= 7 && y >= 0 && x >= 0 && y <= 7))
            {
                return false;
            }

            bool Enemy = false;
            if (board.ArrFigure[x, y] != null)
            {
                if (board.ArrFigure[x, y].Color != Color)
                {
                    Enemy = true;

                }
                else
                {
                    board.ArrFigure[x, y].FigureProtection = true;
                    return false;
                }
                if (board.ArrFigure[x, y].Color != Color && board.ArrFigure[x, y] is King king)
                {
                    king.KingAttack = true;
                    Enemy = true;
                }
            }

            CellsUnderAttack.Add((x, y));

            return Enemy;
        }
        public virtual bool CanMoveTo(int targetX, int targetY)
        {
            if (targetX < 0 || targetX >= board.boardSize || targetY < 0 || targetY >= board.boardSize)
                return false;

            if (board.ArrFigure[targetX, targetY] != null &&
                board.ArrFigure[targetX, targetY].Color == Color)
                return false;

            if (!CanReach(targetX, targetY))
                return false;

            if (WouldKingBeInCheck(targetX, targetY))
                return false;

            return true;
        }
        protected virtual bool CanReach(int targetX, int targetY)
        {
            return IsAttacking(targetX, targetY);
        }
        protected bool WouldKingBeInCheck(int targetX, int targetY)
        {
            var originalFigure = board.ArrFigure[targetX, targetY];
            board.ArrFigure[targetX, targetY] = this;
            board.ArrFigure[X, Y] = null;

            var king = board.GetKing(Color);
            bool isCheck = board.IsSquareUnderAttack(king.X, king.Y, Color);

            board.ArrFigure[X, Y] = this;
            board.ArrFigure[targetX, targetY] = originalFigure;

            return isCheck;
        }
        protected bool IsBlockedPath(int startX, int startY, int endX, int endY)
        {
            int dx = Math.Sign(endX - startX);
            int dy = Math.Sign(endY - startY);

            int x = startX + dx;
            int y = startY + dy;

            while (x != endX || y != endY)
            {
                if (board.ArrFigure[x, y] != null)
                    return true;

                x += dx;
                y += dy;
            }

            return false;
        }
        public List<(int x, int y)> GetLineToTarget( int targetX, int targetY)
        {
            var line = new List<(int x, int y)>();

            int dx = Math.Sign(targetX - X);
            int dy = Math.Sign(targetY - Y);

            if (dx == 0 && dy == 0) return line;

            int x = targetX;
            int y = targetY;

            while (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                line.Add((x, y));
                x += dx;
                y += dy;
            }

            return line;
        }
        protected void ScanDiagonal(int startX, int startY, int stepX, int stepY, bool Move = false)
        {
            for (int x = startX + stepX, y = startY + stepY;
                 x >= 0 && x < board.boardSize && y >= 0 && y < board.boardSize;
                 x += stepX, y += stepY)
            {
                if (!Move) CheckCell(x, y);
                else board.AddMarker(x, y, Color);


                if (board.ArrFigure[x, y] != null) break;
            }
        }
        protected void ScanStraight(int startX, int startY, int stepX, int stepY, bool Move = false)
        {
            for (int x = startX + stepX, y = startY + stepY;
                 x >= 0 && x < board.boardSize && y >= 0 && y < board.boardSize;
                 x += stepX, y += stepY)
            {
                if (!Move) CheckCell(x, y);
                else board.AddMarker(x, y, Color);
                if (board.ArrFigure[x, y] != null) break;
            }
        }
    }
}