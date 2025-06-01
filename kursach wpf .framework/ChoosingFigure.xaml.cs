using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kursach_wpf.framework
{
    public partial class ChoosingFigure : Page
    {
        Canvas MyCanvas;
        public string SelectedFigure;
        public string SelectedPiece { get; private set; }
        private readonly TaskCompletionSource<string> _selectionTask = new TaskCompletionSource<string>();
        Grid container = new Grid();

        public async Task<string> WaitForSelectionAsync()
        {
            return await _selectionTask.Task;
        }

        public ChoosingFigure(bool isWhite, Canvas myCanvas)
        {
            InitializeComponent();

            MyCanvas = myCanvas;

            Canvas.SetLeft(container, 210);
            Canvas.SetTop(container, 320);
            Canvas.SetZIndex(container, 100);

            var background = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0)),
                RadiusX = 10,
                RadiusY = 10,
                Opacity = 0.99
            };
            container.Children.Add(background);

            var buttonsPanel = new WrapPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10)
            };
            container.Children.Add(buttonsPanel);

            background.Width = 330;
            background.Height = 90;

            AddPieceButton(buttonsPanel, "Queen", isWhite);
            AddPieceButton(buttonsPanel, "Rook", isWhite);
            AddPieceButton(buttonsPanel, "Bishop", isWhite);
            AddPieceButton(buttonsPanel, "Knight", isWhite);

            MyCanvas.Children.Add(container);
        }

        private void AddPieceButton(Panel parent, string pieceType, bool isWhite)
        {
            var button = new Button
            {
                Width = 70,
                Height = 70,
                Margin = new Thickness(5),
                Tag = pieceType,
                ToolTip = pieceType,
                Content = new Image
                {
                    Source = new BitmapImage(new Uri(
                       $"pack://application:,,,/Image/{pieceType} {(isWhite ? "White" : "Black")} Outline 288px.png")),
                    Width = 60,
                    Height = 60
                }
            };
            button.Click += (s, e) =>
            {
                _selectionTask.TrySetResult((string)((Button)s).Tag);
                MyCanvas.Children.Remove(container);
            };
            parent.Children.Add(button);
        }
    }
}

