using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
        public Cell[,] boardArray = new Cell[8, 8];
        int tileSize = 80;
        int boardSize = 8;

        public void GenerateChessBoard(Canvas canvas)
        {

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

                    Canvas.SetLeft(cell.rect, col * tileSize);
                    Canvas.SetTop(cell.rect, row * tileSize);

                    boardArray[row, col] = cell;
                    canvas.Children.Add(cell.rect);
                }
            }
        }
        public void AddFigure(Figure figure, Canvas MyCanvas)
        {
            int x = tileSize * figure.X;
            int y = tileSize * figure.Y;
            Canvas.SetLeft(figure.ImageFigure, x);
            Canvas.SetTop(figure.ImageFigure, y);
            MyCanvas.Children.Add(figure.ImageFigure);
        }
    }
}