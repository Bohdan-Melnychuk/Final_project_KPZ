using System;
using System.Windows;


namespace kursach_wpf.framework
{
    public partial class MainWindow : Window
    {
        Board board = new Board();
        
        public MainWindow()
        {
            InitializeComponent();
            board.MyCanvas = MyCanvas;
            board.window = this;
            board.GenerateChessBoard();
            board.StartPositionFigure();
            board.InitializeTimers(TimeSpan.FromMinutes(5));
        }
    }
}
