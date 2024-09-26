using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Torre : Peca
    {
        public override TypePeca Type => TypePeca.Torre;
        public override Jogador Color { get; }


        //Direcoes que a torre pode mover
        public static readonly Direcao[] dirs = new Direcao[]
        {
            Direcao.Cima,
            Direcao.Baixo,
            Direcao.Drt,
            Direcao.Esq
        };

        public Torre(Jogador color)
        {
            Color = color;
        }

        public override Peca Copy()
        {
            Torre copy = new Torre(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        //Movimentos possiveis a partir da posicao inical
        public override IEnumerable<Move> GetMoves(Posicao de, Board board)
        {
            return MovePositionsInDirs(de, board, dirs).Select(para => new Normal(de, para));

        }
    }
}
