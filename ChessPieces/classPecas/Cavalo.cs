using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Cavalo : Peca
    {
        public override TypePeca Type => TypePeca.Cavalo;
        public override Jogador Color { get; }


        //Define a cor do cavalo
        public Cavalo(Jogador color)
        {
            Color = color;
        }

        public override Peca Copy()
        {
            Cavalo copy = new Cavalo(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }


        //Movimentos em L
        private static IEnumerable<Posicao> PossiblePos(Posicao de) 
        {
            foreach (Direcao vDir in new Direcao[] { Direcao.Cima, Direcao.Baixo})
            {
                foreach (Direcao hDir in new Direcao[] { Direcao.Esq, Direcao.Drt})
                {
                    yield return de + 2 * vDir + hDir;
                    yield return de + 2 * hDir + vDir;
                }
                
            }
        }

        //Ve se os Moves possiveis estao dentro do board e se ja tem alguma peca la dentro
        private IEnumerable<Posicao> MovePos(Posicao de, Board board)
        {
            return PossiblePos(de).Where(pos => Board.IsInside(pos) 
            && (board.IsEmpty(pos) || board[pos].Color != Color));
        }

        public override IEnumerable<Move> GetMoves(Posicao de, Board board)
        {
            return MovePos(de, board).Select(para => new Normal(de, para));
        }
    }
}