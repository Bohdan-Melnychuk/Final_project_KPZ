using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace kursach_wpf.framework
{
    public partial class MainWindow : Window
    {
        Board board = new Board();
        
        public MainWindow()
        {
            InitializeComponent();
            board.MyCanvas = MyCanvas;
            board.GenerateChessBoard();
            board.StartPositionFigure();
        }
    }
}
