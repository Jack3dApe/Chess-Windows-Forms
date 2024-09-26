using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public enum Jogador
    {
        None,
        White,
        Black,
    }
    public static class JogadorExtra
    {
        public static Jogador Oponente(this Jogador jogador) //Determinas o oponente baseado no jogador atual
        {
            switch (jogador)
            {
                case Jogador.White:
                    return Jogador.Black;

                case Jogador.Black:
                    return Jogador.White;
                default:
                    return Jogador.None;
            }
        }
    }
}
