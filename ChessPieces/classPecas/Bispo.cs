using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Bispo : Peca
    {
        public override TypePeca Type => TypePeca.Bispo;
        public override Jogador Color { get; }

        // Restringe apenas a movimentos diagonais
        public static readonly Direcao[] dirs = new Direcao[]
        {
            Direcao.CimaEsq,
            Direcao.CimaDrt,
            Direcao.BaixoEsq,
            Direcao.BaixoDrt,
        };


        //Cor do bispo
        public Bispo(Jogador color)
        {
            Color = color;
        }

        public override Peca Copy()
        {
            Bispo copy = new Bispo(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }


        //retorna movimentos possiveis
        public override IEnumerable<Move> GetMoves(Posicao de, Board board)
        {
            return MovePositionsInDirs(de, board, dirs).Select(para => new Normal(de, para));

        }
    }
}
