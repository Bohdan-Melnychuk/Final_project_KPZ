using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace kursach_wpf.framework
{
    public class King : Figure
    {
        public bool FirstMove = true;
        public bool KingAttack {
            get { return KingAttack; }
            set
            {
                if (value == true) {
                    int x = board.tileSize * (X);
                    int y = board.tileSize * (Y);
                    Image image = new Image
                    {
                        Width = 80,
                        Height = 80,
                        Cursor = Cursors.Hand,
                        Source = new BitmapImage(new Uri("pack://application:,,,/Image/chess_attack_King_marker.png")),
                        Tag = "king_marker",
                        IsHitTestVisible = false
                    };
                    Canvas.SetZIndex(image, 7);
                    Canvas.SetTop(image, y + board.margin);
                    Canvas.SetLeft(image, x + board.margin);
                    board.ListMarcker.Add(($"{x}{y}", image));
                    board.MyCanvas.Children.Add(image);
                }

            }
        }

        public King(bool color) : base(color, $"pack://application:,,,/Image/King {(color ? "White" : "Black")} Outline 288px.png")
        {

        }

        public King(bool color, int x, int y) : base(color, x, y, $"pack://application:,,,/Image/King {(color ? "White" : "Black")} Outline 288px.png")
        {

        }

        public override bool IsAttacking(int targetX, int targetY)
        {
            int dx = Math.Abs(targetX - X);
            int dy = Math.Abs(targetY - Y);
            return dx <= 1 && dy <= 1;
        }

        public override bool CanMoveTo(int targetX, int targetY)
        {
            if (!base.CanMoveTo(targetX, targetY))
                return false;

            if (board.IsSquareUnderAttack(targetX, targetY, Color))
                return false;

            if (IsMovingIntoLineOfFire(targetX, targetY))
                return false;

            return true;
        }

        public override void MoveFigure()
        {
            TryAddMove(X + 1, Y);      // right
            TryAddMove(X - 1, Y);      // left
            TryAddMove(X, Y + 1);      // down
            TryAddMove(X, Y - 1);      // up
            TryAddMove(X + 1, Y + 1);  // right + down
            TryAddMove(X - 1, Y + 1);  // left + down
            TryAddMove(X + 1, Y - 1);  // right + up
            TryAddMove(X - 1, Y - 1);  // left + up
            CanMoveToCastling();
        }

        private void TryAddMove(int targetX, int targetY)
        {
            if (targetX < 0 || targetX >= board.boardSize || targetY < 0 || targetY >= board.boardSize)
                return;

            if (board.ArrFigure[targetX, targetY] != null &&
                board.ArrFigure[targetX, targetY].Color == Color)
                return;

            if (board.ArrFigure[targetX, targetY] != null &&
                    board.ArrFigure[targetX, targetY].Color != Color &&
                    board.ArrFigure[targetX, targetY].FigureProtection)
            {
                return;
            }
            if (!IsCellUnderAttack(targetX, targetY, Color) &&
                !IsMovingIntoLineOfFire(targetX, targetY))
            {
                board.AddMarker(targetX, targetY, Color);
            }
        }

        private bool IsCellUnderAttack(int x, int y, bool kingColor)
        {
            for (int i = 0; i < board.boardSize; i++)
            {
                for (int j = 0; j < board.boardSize; j++)
                {
                    Figure figure = board.ArrFigure[i, j];
                    if (figure != null && figure.Color != kingColor)
                    {
                        if (figure.CellsUnderAttack.FindAll(s => s == (x, y)).Count != 0)
                            return true;
                    }
                }
            }
            return false;
        }

        public override void FillArrayAttacksCell()
        {
            CheckCell(X + 1, Y);
            CheckCell(X - 1, Y);
            CheckCell(X, Y + 1);
            CheckCell(X, Y - 1);
            CheckCell(X + 1, Y + 1);
            CheckCell(X - 1, Y + 1);
            CheckCell(X + 1, Y - 1);
            CheckCell(X - 1, Y - 1);
        }

        private bool IsMovingIntoLineOfFire(int targetX, int targetY)
        {
            foreach (var figure in board.GetAllFigures(!Color))
            {
                if ((figure is Rook || figure is Bishop || figure is Queen) && figure.IsAttacking(X, Y))
                {
                    if (figure.GetLineToTarget(X, Y).Contains((targetX, targetY)))
                        return true;
                }
            }
            return false;
        }

        public bool IsAttackingCell(int targetX, int targetY)
        {
            return Math.Abs(X - targetX) <= 1 && Math.Abs(Y - targetY) <= 1;
        }

        public void CanMoveToCastling()
        {
            try
            {
                if (FirstMove && board.ArrFigure[X + 3, Y] is Rook rook && rook.FirstMove)
                {
                    if (board.ArrFigure[X + 1, Y] == null && board.ArrFigure[X + 2, Y] == null)
                    {
                        board.AddMarker(X + 2, Y, Color);
                        (string, Image) a = board.ListMarcker.Find(s => s.Item1 == $"{X + 2}{Y}");
                        Image image = a.Item2;
                        image.MouseDown -= board.MoveFigure;
                        image.MouseDown += Racerovka;
                        image.MouseDown += board.MoveFigure;
                    }
                }
                if (FirstMove && board.ArrFigure[X - 4, Y] is Rook rook1 && rook1.FirstMove)
                {
                    if (board.ArrFigure[X - 1, Y] == null && board.ArrFigure[X - 2, Y] == null && board.ArrFigure[X - 3, Y] == null)
                    {
                        board.AddMarker(X - 2, Y, Color);
                        (string, Image) a = board.ListMarcker.Find(s => s.Item1 == $"{X - 2}{Y}");
                        Image image = a.Item2;
                        image.MouseDown -= board.MoveFigure;
                        image.MouseDown += Racerovka;
                        image.MouseDown += board.MoveFigure;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public void Racerovka(object sender, RoutedEventArgs e)
        {
            string Element = board.ListMarcker.Where(x => x.Item2 == (Image)sender).First().Item1;
            int Xr = int.Parse($"{Element[0]}");
            int Yr = int.Parse($"{Element[1]}");
            if (Xr == 6)
            {
                board.ArrFigure[Xr + 1, Yr].X = Xr - 1;
                board.ArrFigure[Xr + 1, Yr].Y = Yr;

                Canvas.SetLeft(board.ArrFigure[Xr + 1, Yr].ImageFigure, board.tileSize * (Xr - 1) + board.margin);
                Canvas.SetTop(board.ArrFigure[Xr + 1, Yr].ImageFigure, board.tileSize * (Yr) + board.margin);
                board.ArrFigure[Xr - 1, Yr] = board.ArrFigure[Xr + 1, Yr];
                board.ArrFigure[Xr + 1, Yr] = null;
            }
            else if (Xr == 2)
            {
                board.ArrFigure[Xr - 2, Yr].X = Xr + 1;
                board.ArrFigure[Xr - 2, Yr].Y = Yr;

                Canvas.SetLeft(board.ArrFigure[Xr -2, Yr].ImageFigure, board.tileSize * (Xr + 1) + board.margin);
                Canvas.SetTop(board.ArrFigure[Xr -2, Yr].ImageFigure, board.tileSize * (Yr) + board.margin);
                board.ArrFigure[Xr + 1, Yr] = board.ArrFigure[Xr - 2, Yr];
                board.ArrFigure[Xr - 2, Yr] = null;
            }
        }
    }
}
