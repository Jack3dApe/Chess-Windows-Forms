using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Resultado
    {
        public Jogador Winner { get; }

        public EndGame Reason { get; }


        //Cria um resultado com um vencedor e um motivo
        public Resultado (Jogador winner, EndGame reason)
        {
            Winner = winner;
            Reason = reason;
        }

        //Resultado como checkmate
        public static Resultado Win(Jogador winner)
        {
            return new Resultado(winner, EndGame.Checkmate);
        }


        //Resultado como empate
        public static Resultado Empate(EndGame reason)
        {
            return new Resultado(Jogador.None, reason);
        }
    }
}
