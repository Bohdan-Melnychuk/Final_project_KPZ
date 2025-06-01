using kursach_wpf.framework;
using System;
using System.Windows;

namespace ChessGame
{
    public partial class Start : Window
    {
        private bool isClosingProgrammatically = false;

        public Start()
        {
            InitializeComponent();
        }

        public void CloseProgrammatically()
        {
            isClosingProgrammatically = true;
            this.Close();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow dialog = new MainWindow();

                this.Visibility = Visibility.Hidden;

                dialog.ShowDialog();

                this.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при запуску гри: {ex.Message}",
                               "Помилка",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Ви дійсно хочете вийти з гри?",
                "Підтвердження виходу",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ExitButton_Click(sender, e);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (isClosingProgrammatically)
            {
                base.OnClosing(e);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Ви дійсно хочете вийти з гри?",
                "Підтвердження виходу",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }

            base.OnClosing(e);
        }
    }
}