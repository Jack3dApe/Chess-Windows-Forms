using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Rainha : Peca
    {
        public override TypePeca Type => TypePeca.Rainha;
        public override Jogador Color { get; } //Cor da peca

        //Direcoes que a rainha pode mover, AKA todas
        private static readonly Direcao[] dirs = new Direcao[]
        {
            Direcao.Baixo,
            Direcao.Cima,
            Direcao.Esq,
            Direcao.Drt,
            Direcao.BaixoDrt,
            Direcao.BaixoEsq,
            Direcao.CimaEsq,
            Direcao.CimaDrt,

        }; 

        public Rainha(Jogador color) 
        {
            Color = color;
        }

        public override Peca Copy()
        {
            Rainha copy = new Rainha(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        //Moves possiveis a partir da posicao atual
        public override IEnumerable<Move> GetMoves(Posicao de, Board board)
        {
            return MovePositionsInDirs(de, board, dirs).Select(para => new Normal(de, para));

        }
    }
}