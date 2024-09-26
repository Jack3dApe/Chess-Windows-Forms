using ChessPieces;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Opcao> OpcaoSelect;

        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();

            Resultado resultado = gameState.Resultado; //Obtem resultado

            //Vencedor e razao do fim de jogo
            WinnerTxt.Text = GetWinnerTxt(resultado.Winner);
            EndGameReason.Text = EndGameText(resultado.Reason, gameState.CurrentJogador);
        }

        private static string GetWinnerTxt(Jogador winner) //Cria texto do vencedor
        {
            return winner switch
            {
                Jogador.White => "BRANCO GANHOU",
                Jogador.Black => "PRETO GANHOU",
                _ => "EMPATE" 
            };
        }

        private  static string JogadorString (Jogador jogador)
        {
            return jogador switch
            {
                Jogador.White => "BRANCO",
                Jogador.Black => "PRETO",
                _ => "" //Serve para n receber um aviso visto que o switch n engloba todos os casos possiveis
            };
        }

        private static string EndGameText(EndGame endGame, Jogador currentJogador)
        {
            return endGame switch
            {
                EndGame.Stalemate => $"STALEMATE - {JogadorString(currentJogador)} NÂO PODE MEXER",
                EndGame.Checkmate => $"CHECKMATE - {JogadorString(currentJogador)} NÂO PODE MEXER",
                
                _ => ""
            };
        }

        //Botoes de restart e exit 
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OpcaoSelect?.Invoke(Opcao.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OpcaoSelect?.Invoke(Opcao.Exit);
        }
    }
}
