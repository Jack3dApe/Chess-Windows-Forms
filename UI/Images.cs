using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessPieces;


namespace UI
{
    public static class Images
    {
        //Class para carregar as imagens das pecas, self explanatory
        private static readonly Dictionary<TypePeca, ImageSource> whiteSources = new()
        {
            {TypePeca.Peao, LoadImage("Images/PawnW.png") },
            {TypePeca.Bispo, LoadImage("Images/BishopW.png") },
            {TypePeca.Cavalo, LoadImage("Images/KnightW.png") },
            {TypePeca.Torre, LoadImage("Images/RookW.png") },
            {TypePeca.Rainha, LoadImage("Images/QueenW.png") },
            {TypePeca.Rei, LoadImage("Images/KingW.png") },

        };

        private static readonly Dictionary<TypePeca, ImageSource> blackSources = new()
        {
            {TypePeca.Peao, LoadImage("Images/PawnB.png") },
            {TypePeca.Bispo, LoadImage("Images/BishopB.png") },
            {TypePeca.Cavalo, LoadImage("Images/KnightB.png") },
            {TypePeca.Torre, LoadImage("Images/RookB.png") },
            {TypePeca.Rainha, LoadImage("Images/QueenB.png") },
            {TypePeca.Rei, LoadImage("Images/KingB.png") },

        };


        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        //Obter imagem corresponde ao tipo de peca e cor do jogador
        public static ImageSource GetImage(Jogador cor, TypePeca type)
        {
            return cor switch
            {
                Jogador.White => whiteSources[type],
                Jogador.Black => blackSources[type],
                _ => null
            };
        }

        public static ImageSource GetImage(Peca peca)
        {
            if (peca == null)
            {
                return null;
            }

            return GetImage(peca.Color, peca.Type);
        }
    }
}
