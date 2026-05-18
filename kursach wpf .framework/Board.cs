using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace kursach_wpf.framework
{
    public class Board
    {
        public Canvas MyCanvas;
        public MainWindow window;
        public Cell[,] boardArray = new Cell[8, 8];
        public Figure[,] ArrFigure = new Figure[8, 8];
        public List<(string, Image)> ListMarcker = new List<(string, Image)>();

        public Figure ActiveFigure;

        public int tileSize = 80;
        public int boardSize = 8;
        public int margin = 50;
        int ramkaSize = 40;
        private int halfMoveClock = 0;

        Rectangle ramka = new Rectangle();

        private DispatcherTimer whiteTimer;
        private DispatcherTimer blackTimer;
        private TimeSpan whiteTimeRemaining;
        private TimeSpan blackTimeRemaining;
        private TextBlock whiteTimerText;
        private TextBlock blackTimerText;
        private bool isWhiteTurn = true;

        public void InitializeTimers(TimeSpan initialTime)
        {
            whiteTimeRemaining = initialTime;
            blackTimeRemaining = initialTime;

            whiteTimerText = new TextBlock
            {
                FontSize = 24,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                Text = FormatTime(whiteTimeRemaining)
            };

            blackTimerText = new TextBlock
            {
                FontSize = 24,
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold,
                Text = FormatTime(blackTimeRemaining)
            };

            Canvas.SetLeft(whiteTimerText, margin + boardSize * tileSize - 350);
            Canvas.SetTop(whiteTimerText, margin + boardSize * tileSize);

            Canvas.SetLeft(blackTimerText, margin + boardSize * tileSize - 350);
            Canvas.SetTop(blackTimerText, margin - 40 + 3);

            MyCanvas.Children.Add(whiteTimerText);
            MyCanvas.Children.Add(blackTimerText);

            Panel.SetZIndex(whiteTimerText, 999);
            Panel.SetZIndex(blackTimerText, 999);

            whiteTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            whiteTimer.Tick += WhiteTimer_Tick;

            blackTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            blackTimer.Tick += BlackTimer_Tick;

            whiteTimer.Start();
        }

        private string FormatTime(TimeSpan time)
        {
            return $"{time.Minutes:D2}:{time.Seconds:D2}";
        }

        private void WhiteTimer_Tick(object sender, EventArgs e)
        {
            if (whiteTimeRemaining.TotalSeconds > 0)
            {
                whiteTimeRemaining = whiteTimeRemaining.Subtract(TimeSpan.FromSeconds(1));
                whiteTimerText.Text = FormatTime(whiteTimeRemaining);
            }
            else
            {
                whiteTimer.Stop();
                var page = new Page1('t');
                Application.Current.MainWindow.Content = page;
                window.Close();
            }
        }

        private void BlackTimer_Tick(object sender, EventArgs e)
        {
            if (blackTimeRemaining.TotalSeconds > 0)
            {
                blackTimeRemaining = blackTimeRemaining.Subtract(TimeSpan.FromSeconds(1));
                blackTimerText.Text = FormatTime(blackTimeRemaining);
            }
            else
            {
                blackTimer.Stop();
                var page = new Page1('r');
                Application.Current.MainWindow.Content = page;
                window.Close();
            }
        }

        private void SwitchTurns()
        {
            if (isWhiteTurn)
            {
                whiteTimer.Stop();
                blackTimer.Start();
                whiteTimerText.Foreground = Brushes.Gray;
                blackTimerText.Foreground = Brushes.Black;
            }
            else
            {
                blackTimer.Stop();
                whiteTimer.Start();
                blackTimerText.Foreground = Brushes.Gray;
                whiteTimerText.Foreground = Brushes.White;
            }
            isWhiteTurn = !isWhiteTurn;
        }

        public void StartPositionFigure()
        {
            for (int i = 0; i < 8; i++)
            {
                ArrFigure[i, 1] = new Pawn(false, i, 1, MyCanvas);
                ArrFigure[i, 6] = new Pawn(true, i, 6, MyCanvas);
            }
            ArrFigure[0, 0] = new Rook(false, 0, 0);
            ArrFigure[0, 7] = new Rook(true, 0, 7);
            ArrFigure[7, 0] = new Rook(false, 7, 0);
            ArrFigure[7, 7] = new Rook(true, 7, 7);

            ArrFigure[1, 0] = new Knight(false, 1, 0);
            ArrFigure[1, 7] = new Knight(true, 1, 7);
            ArrFigure[6, 0] = new Knight(false, 6, 0);
            ArrFigure[6, 7] = new Knight(true, 6, 7);

            ArrFigure[2, 0] = new Bishop(false, 2, 0);
            ArrFigure[2, 7] = new Bishop(true, 2, 7);
            ArrFigure[5, 0] = new Bishop(false, 5, 0);
            ArrFigure[5, 7] = new Bishop(true, 5, 7);

            ArrFigure[4, 0] = new King(false, 4, 0);
            ArrFigure[4, 7] = new King(true, 4, 7);

            ArrFigure[3, 0] = new Queen(false, 3, 0);
            ArrFigure[3, 7] = new Queen(true, 3, 7);

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (ArrFigure[i, j] != null)
                    {
                        AddFigure(ArrFigure[i, j]);
                    }
                }
            }

        }

        public void GenerateChessBoard()
        {
            ramka = new Rectangle()
            {
                Width = tileSize * boardSize + ramkaSize * 2,
                Height = tileSize * boardSize + ramkaSize * 2,
                Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#8A3700"),
                RadiusX = 20,
                RadiusY = 20
            };
            MyCanvas.Children.Add(ramka);
            Canvas.SetLeft(ramka, margin - ramkaSize);
            Canvas.SetTop(ramka, margin - ramkaSize);
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Cell cell = new Cell();

                    cell.rect = new Rectangle
                    {
                        Width = tileSize,
                        Height = tileSize,
                        Fill = (row + col) % 2 == 0 ? (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF2D4") : (SolidColorBrush)new BrushConverter().ConvertFrom("#DE925A")
                    };

                    Canvas.SetLeft(cell.rect, col * tileSize + margin);
                    Canvas.SetTop(cell.rect, row * tileSize + margin);

                    boardArray[row, col] = cell;
                    MyCanvas.Children.Add(cell.rect);
                }
            }
        }

        public void AddFigure(Figure figure)
        {
            int x = tileSize * figure.X;
            int y = tileSize * figure.Y;

            figure.ImageFigure.MouseDown += (sender, e) =>
            {
                if (figure.Color != isWhiteTurn)
                {
                    return;
                }

                if (ActiveFigure != null)
                {
                    DeleteAllMarcker();
                    ActiveFigure = null;
                }
                else
                {
                    figure.MoveFigure();
                    ActiveFigure = figure;
                }
            };

            Canvas.SetLeft(figure.ImageFigure, x + margin);
            Canvas.SetTop(figure.ImageFigure, y + margin);

            figure.board = this;
            figure.FillArrayAttacksCell();
            MyCanvas.Children.Add(figure.ImageFigure);
        }

        public bool AddMarker(int x, int y, bool color)
        {
            int X = tileSize * (x);
            int Y = tileSize * (y);
            Image image = new Image
            {
                Width = 80,
                Height = 80,
                Cursor = Cursors.Hand
            };

            if (!(x <= 7 && y >= 0 && x >= 0 && y <= 7))
            {
                return false;
            }

            bool Enemy = false;
            if (ArrFigure[x, y] != null)
            {
                if (ArrFigure[x, y].Color != color)
                {
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/Image/chess_attack_marker.png"));
                }
                if (ArrFigure[x, y].Color != color && ArrFigure[x, y] is King king)
                {
                    return true;
                }
                Enemy = true;
            }
            else if (ArrFigure[x, y] == null)
            {
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Image/chess_move_point.png"));
                Enemy = false;
            }

            image.MouseDown += MoveFigure;
            Canvas.SetZIndex(image, 5);
            Canvas.SetTop(image, Y + margin);
            Canvas.SetLeft(image, X + margin);
            ListMarcker.Add(($"{x}{y}", image));
            MyCanvas.Children.Add(image);
            return Enemy;
        }

        public void MoveFigure(object sender, RoutedEventArgs e)
        {
            string Element = ListMarcker.Where(x => x.Item2 == (Image)sender).First().Item1;

            if (ArrFigure[int.Parse($"{Element[0]}"), int.Parse($"{Element[1]}")] != null || ActiveFigure is Pawn)
                halfMoveClock = 0;
            else halfMoveClock++;



            ArrFigure[ActiveFigure.X, ActiveFigure.Y] = null;
            ActiveFigure.X = int.Parse($"{Element[0]}");
            ActiveFigure.Y = int.Parse($"{Element[1]}");


            if (ArrFigure[ActiveFigure.X, ActiveFigure.Y] != null)
            {
                MyCanvas.Children.Remove(ArrFigure[ActiveFigure.X, ActiveFigure.Y].ImageFigure);
                ArrFigure[ActiveFigure.X, ActiveFigure.Y] = null;
            }

            ArrFigure[ActiveFigure.X, ActiveFigure.Y] = ActiveFigure;

            Image Element2 = (Image)sender;

            Canvas.SetLeft(ActiveFigure.ImageFigure, Canvas.GetLeft(Element2));
            Canvas.SetTop(ActiveFigure.ImageFigure, Canvas.GetTop(Element2));
            foreach (var element in ListMarcker)
            {
                MyCanvas.Children.Remove(element.Item2);
            }
            ListMarcker.Clear();

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (ArrFigure[i, j] != null)
                    {
                        ArrFigure[i, j].FigureProtection = false;
                    }
                }
            }
            RefreshAllAttackCell();

            if (ActiveFigure is Pawn pawn)
            {
                pawn.FirstMove = false;
            }
            if (ActiveFigure is Rook rook)
            {
                rook.FirstMove = false;
            }
            if (ActiveFigure is King king)
            {
                king.FirstMove = false;
            }

            CheckGameState(!ActiveFigure.Color);

            SwitchTurns();

            ActiveFigure = null;
        }

        public void RefreshAllAttackCell()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (ArrFigure[i, j] != null)
                    {
                        ArrFigure[i, j].CellsUnderAttack.Clear();
                        ArrFigure[i, j].FillArrayAttacksCell();
                    }
                }
            }
        }

        public bool IsSquareUnderAttack(int x, int y, bool color)
        {
            foreach (var figure in GetAllFigures())
            {
                if (figure.Color != color)
                {
                    figure.FillArrayAttacksCell();
                    if (figure.IsAttacking(x, y))
                        return true;
                }
            }
            return false;
        }

        public List<Figure> GetAttackers(int x, int y, bool kingColor)
        {
            var attackers = new List<Figure>();
            foreach (var figure in GetAllFigures())
            {
                if (figure.Color != kingColor && figure.IsAttacking(x, y))
                    attackers.Add(figure);
            }
            return attackers;
        }

        public List<(int X, int Y)> GetPathBetween(int x1, int y1, int x2, int y2)
        {
            var path = new List<(int X, int Y)>();
            int dx = Math.Sign(x2 - x1);
            int dy = Math.Sign(y2 - y1);

            int x = x1 + dx;
            int y = y1 + dy;
            while (x != x2 || y != y2)
            {
                path.Add((x, y));
                x += dx;
                y += dy;
            }
            return path;
        }

        public bool IsKingInCheck(bool color)
        {
            var king = GetKing(color);
            return IsSquareUnderAttack(king.X, king.Y, color);
        }

        public List<Figure> GetAllFigures()
        {
            return ArrFigure.Cast<Figure>().Where(f => f != null).ToList();
        }

        public List<Figure> GetAllFigures(bool color)
        {
            return ArrFigure.Cast<Figure>().Where(f => f != null && f.Color == color).ToList();
        }

        public Figure GetKing(bool color)
        {
            return ArrFigure.Cast<Figure>().FirstOrDefault(f => f != null && f.Color == color && f is King);
        }

        public void CheckGameState(bool color)
        {
            GetAllFigures(color).ForEach(s => s.MoveFigure());
            if (IsDraw() || ListMarcker.Count == 0)
            {
                var page = new Page1('d');
                Application.Current.MainWindow.Content = page;
                DeleteAllMarcker();
                window.Close();
                return;
            }
            else if ((ListMarcker.Where(item => item.Item2.Tag?.ToString() != "king_marker").ToList().Count == 0))
            {
                char winner = ActiveFigure.Color ? 'w' : 'b';
                var page = new Page1(winner);
                Application.Current.MainWindow.Content = page;
                window.Close();
            }
            DeleteAllMarcker();
        }

        private void DeleteAllMarcker()
        {
            foreach (var element in ListMarcker)
            {
                if (element.Item2.Tag?.ToString() != "king_marker")
                {

                    MyCanvas.Children.Remove(element.Item2);
                }
            }
            ListMarcker.RemoveAll(s => s.Item2.Tag?.ToString() != "king_marker");
        }

        public bool IsDraw()
        {
            var figures = GetAllFigures();
            if (figures.Count <= 3)
            {
                bool insufficientMaterial = true;
                foreach (var figure in figures)
                {
                    if (figure is Queen || figure is Rook || figure is Pawn)
                    {
                        insufficientMaterial = false;
                        break;
                    }
                }
                if (insufficientMaterial)
                {
                    return true;
                }
            }
            if (halfMoveClock >= 50)
            {
                return true;
            }
            return false;
        }
    }
}