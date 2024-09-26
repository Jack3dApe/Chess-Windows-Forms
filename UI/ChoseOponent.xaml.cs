using System;
using System.Windows;
using System.Windows.Controls;
using ChessPieces;

namespace UI
{

    public partial class ChoseOponent : UserControl
    {
        public ChoseOponent()
        {
            InitializeComponent();
        }

        private void Local_Click(object sender, RoutedEventArgs e) //Iniciar com jogador local
        {
            StartGame(false);
        }

        private void Bot_Click(object sender, RoutedEventArgs e) //Iniciar com o bot como oponente
        {
            StartGame(true);
        }


        private void StartGame(bool playAgainstAI)
        {
            MainWindow mainWindow = new MainWindow(playAgainstAI);
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}

