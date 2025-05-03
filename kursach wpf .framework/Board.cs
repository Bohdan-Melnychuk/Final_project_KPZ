using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace kursach_wpf.framework
{
    public class Board
    {
        public Canvas MyCanvas;
        public Cell[,] boardArray = new Cell[8, 8];
        public Figure[,] ArrFigure = new Figure[8, 8];
        public List<Image> ListMarcker = new List<Image>();

        int tileSize = 80;
        public int boardSize = 8;
        int margin = 50;
        int ramkaSize = 40;

        Rectangle ramka = new Rectangle();

        public void StartPositionFigure()
        {
            //Black Pawn
            for (int i = 0; i < 8; i++)
            {
                ArrFigure[i, 1] = new Pawn(false, i, 1);
                ArrFigure[i, 6] = new Pawn(true, i, 6);
            }
            //Rook
            ArrFigure[0, 0] = new Rook(false, 0, 0); //Black Rook Left
            ArrFigure[0, 7] = new Rook(true, 0, 7); //White Rook Left
            ArrFigure[7, 0] = new Rook(false, 7, 0); // Black Rook Right
            ArrFigure[7, 7] = new Rook(true, 7, 7); //White Rook Right
            //Knight
            ArrFigure[1, 0] = new Knight(false, 1, 0); //Black Knight Left
            ArrFigure[1, 7] = new Knight(true, 1, 7); //White Kniht Left
            ArrFigure[6, 0] = new Knight(false, 6, 0); //Black Knight Right
            ArrFigure[6, 7] = new Knight(true, 6, 7); //White Knight Right
            //Bishop
            ArrFigure[2, 0] = new Bishop(false, 2, 0); //Black Bishop Left
            ArrFigure[2, 7] = new Bishop(true, 2, 7); //White Bishop Left
            ArrFigure[5, 0] = new Bishop(false, 5, 0); //Black Bishop Right
            ArrFigure[5, 7] = new Bishop(true, 5, 7); //White Bishop Right
            //King
            ArrFigure[4, 0] = new King(false, 4, 0); //Black King
            ArrFigure[4, 7] = new King(true, 4, 7); //White King
            //Queen
            ArrFigure[3, 0] = new Queen(false, 3, 0); //Black Queen
            ArrFigure[3, 7] = new Queen(true, 3, 7); //White Queen


            //ArrFigure[3, 3] = new Bishop(true, 3, 3);
            ArrFigure[3, 3] = new Queen(false, 3, 3);
            //ArrFigure[2, 5] = new Bishop(false, 2, 5);

            ArrFigure[3, 3].MoveFigure(this);

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
                Width = tileSize*boardSize+ramkaSize*2,
                Height = tileSize*boardSize+ramkaSize*2,
                Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#8A3700"),
                RadiusX = 20,
                RadiusY = 20
            };
            MyCanvas.Children.Add(ramka);
            Canvas.SetLeft(ramka, margin-ramkaSize);
            Canvas.SetTop(ramka, margin-ramkaSize);
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

                    Canvas.SetLeft(cell.rect, col * tileSize+margin);
                    Canvas.SetTop(cell.rect, row * tileSize+margin);

                    boardArray[row, col] = cell;
                    MyCanvas.Children.Add(cell.rect);
                }
            }
        }
        public void AddFigure(Figure figure)
        {
            int x = tileSize * figure.X;
            int y = tileSize * figure.Y;

            figure.ImageFigure.MouseEnter += (object sender, MouseEventArgs e) =>
            {
                figure.MoveFigure(this);
            };
            figure.ImageFigure.MouseLeave += (object sender, MouseEventArgs e) =>
            {
                foreach (var element in ListMarcker)
                {
                    MyCanvas.Children.Remove(element);
                }
                ListMarcker.Clear();
            };
            Canvas.SetLeft(figure.ImageFigure, x + margin);
            Canvas.SetTop(figure.ImageFigure, y + margin);
            MyCanvas.Children.Add(figure.ImageFigure);
        }
        public bool AddMarker(int x, int y, bool color)
        {
            int X = tileSize * (x);
            int Y = tileSize * (y);
            Image image = new Image
            {

                Width = 80,
                Height = 80

            };

            if (!(x <= 7 && y >= 0 && x >=0 && y <=7))
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
                Enemy = true;
            }
            else if (ArrFigure[x, y] == null)
            {
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Image/chess_move_point.png"));
                Enemy = false;
            }   



            Canvas.SetZIndex(image, 5);
            Canvas.SetTop(image, Y + margin);
            Canvas.SetLeft(image, X + margin);
            ListMarcker.Add(image);
            MyCanvas.Children.Add(image);
            return Enemy;
        }
    }
}