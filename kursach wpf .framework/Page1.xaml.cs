using ChessGame;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace kursach_wpf.framework
{
    public partial class Page1 : Page
    {
        public Page1(char type)
        {
            InitializeComponent();

            if (type == 'b')
            {
                WinLuse.Content = ("Перемога Чорних");
            }
            else if (type == 'w')
            {
                WinLuse.Content = ("Перемога Білих");
            }
            else if (type == 'd')
            {
                WinLuse.Content = ("Нічия");
            }
            else if (type =='t')
            {
                WinLuse.Content = ("   Час вичерпано\nПеремога Чорних");
            }
            else if (type == 'r')
            {
                WinLuse.Content = ("   Час вичерпано\n  Перемога Білих");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Start mainMenu = new Start();
            mainMenu.Show();

            Start mainWindow = Application.Current.Windows.OfType<Start>().FirstOrDefault();
            mainWindow?.CloseProgrammatically();
        }
    }
}
