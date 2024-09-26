using ChessPieces;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI
{
    //Menu de promocao de peao
    public partial class PromMenu : UserControl
    {
        public event Action<TypePeca> PecaSelecionada;


        public PromMenu(Jogador jogador)
        {
            InitializeComponent();

            ImgRainha.Source = Images.GetImage(jogador, TypePeca.Rainha);
            ImgBispo.Source = Images.GetImage(jogador, TypePeca.Bispo);
            ImgTorre.Source = Images.GetImage(jogador, TypePeca.Torre);
            ImgCavalo.Source = Images.GetImage(jogador, TypePeca.Cavalo);

        }

        private void ImgRainha_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(TypePeca.Rainha);
        }

        private void ImgBispo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(TypePeca.Bispo);
        }

        private void ImgTorre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(TypePeca.Torre);
        }

        private void ImgCavalo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(TypePeca.Cavalo);
        }
    }
}
