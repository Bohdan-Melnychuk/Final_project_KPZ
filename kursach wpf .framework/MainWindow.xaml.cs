using System;
using System.Collections.Generic;
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
            board.GenerateChessBoard(MyCanvas);
            StartPositionFigure();
        }
        List<Figure> BlackFigure = new List<Figure>();
        List<Figure> WhiteFigure = new List<Figure>();
        public void StartPositionFigure()
        {
            //Black Pawn
            for (int i = 0; i < 8; i++)
            {
                Pawn BlackPawn = new Pawn($"pawn{i + 1}", false, i, 1);
                BlackFigure.Add(BlackPawn);
                board.AddFigure(BlackPawn, MyCanvas);
            }
            //White Pawn
            for (int i = 0; i < 8; i++)
            {
                Pawn WhitePawn = new Pawn($"pawn{i + 1}", true, i, 6);
                WhiteFigure.Add(WhitePawn);
                board.AddFigure(WhitePawn, MyCanvas);
            }

                                                         //Rook
            //Black Rook Left
            Rook BlackRookL = new Rook("rook1", false, 0, 0);
            BlackFigure.Add(BlackRookL);
            board.AddFigure(BlackRookL, MyCanvas);
            
            //White Rook Left
            Rook WhiteRookL = new Rook("rook2", true, 0, 7);
            WhiteFigure.Add(WhiteRookL);
            board.AddFigure(WhiteRookL, MyCanvas);

            //Black Rook Right
            Rook BlackRookR = new Rook("rook3", false, 7, 0);
            BlackFigure.Add(BlackRookR);
            board.AddFigure(BlackRookR, MyCanvas);

            //White Rook Right
            Rook WhiteRookR = new Rook("rook4", true, 7, 7);
            WhiteFigure.Add(WhiteRookR);
            board.AddFigure(WhiteRookR, MyCanvas);

                                                        //Knight
            //Black Knight Left
            Knight BlackKnightL = new Knight("knight1", false, 1, 0);
            BlackFigure.Add(BlackKnightL);
            board.AddFigure(BlackKnightL, MyCanvas);

            //White Kniht Left
            Knight WhiteKnightL = new Knight("knight2", true, 1, 7);
            WhiteFigure.Add(WhiteKnightL);
            board.AddFigure(WhiteKnightL, MyCanvas);

            //Black Knight Right
            Knight BlackKnightR = new Knight("knight3", false, 6, 0);
            BlackFigure.Add(BlackKnightR);
            board.AddFigure(BlackKnightR, MyCanvas);

            //White Knight Right
            Knight WhiteKnightR = new Knight("knight4", true, 6, 7);
            WhiteFigure.Add(WhiteKnightR);
            board.AddFigure(WhiteKnightR, MyCanvas);

                                                       //Bishop
            //Black Bishop Left
            Bishop BlackBishopL = new Bishop("1", false, 2, 0);
            BlackFigure.Add(BlackBishopL);
            board.AddFigure(BlackBishopL, MyCanvas);
            //White Bishop Left
            Bishop WhiteBishopL = new Bishop("2", true, 2, 7);
            WhiteFigure.Add(WhiteBishopL);
            board.AddFigure(WhiteBishopL, MyCanvas);
            //Black Bishop Right
            Bishop BlackBishopR = new Bishop("3", false, 5, 0);
            BlackFigure.Add(BlackBishopR);
            board.AddFigure(BlackBishopR, MyCanvas);
            //White Bishop Right
            Bishop WhiteBishopR = new Bishop("4", true, 5, 7);
            WhiteFigure.Add(WhiteBishopR);
            board.AddFigure(WhiteBishopR, MyCanvas);

                                                      //King
            //Black King
            King BlackKing = new King("1", false, 4, 0);
            BlackFigure.Add(BlackKing);
            board.AddFigure(BlackKing, MyCanvas);
            //White King
            King WhiteKing = new King("2", true, 4, 7);
            WhiteFigure.Add(WhiteKing);
            board.AddFigure(WhiteKing, MyCanvas);
                                                    //Queen
            //Black Queen
            Queen BlackQueen = new Queen("1", false, 3, 0);
            BlackFigure.Add(BlackQueen);
            board.AddFigure(BlackQueen, MyCanvas);
            //White Queen
            Queen WhiteQueen = new Queen("2", true, 3, 7);
            WhiteFigure.Add(WhiteQueen);
            board.AddFigure(WhiteQueen, MyCanvas);
        }
    }
}
