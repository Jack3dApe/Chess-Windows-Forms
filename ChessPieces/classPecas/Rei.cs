using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Rei : Peca
    {
        public override TypePeca Type => TypePeca.Rei;
        public override Jogador Color { get; }

        //Direcoes que o rei pode mover
        private static readonly Direcao[] dirs = new Direcao[]
        {
            Direcao.Cima,
            Direcao.Baixo,
            Direcao.Esq,
            Direcao.Drt,
            Direcao.CimaDrt,
            Direcao.CimaEsq,
            Direcao.BaixoDrt,
            Direcao.CimaEsq
        };

        public Rei(Jogador color)
        {
            Color = color;
        }

        public override Peca Copy()
        {
            Rei copy = new Rei(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }


       
        private IEnumerable<Posicao> MovePos(Posicao de, Board board)
        {
            foreach (Direcao dir in dirs)
            {
                Posicao para = de + dir;

                if (!Board.IsInside(para)) //Ve se esta dentro do board
                {
                    continue;
                }

                if (board.IsEmpty(para) || board[para].Color != Color)
                {
                    yield return para;
                }
            }
        }

        //Moves possiveis a partir da posicao atual
        public override IEnumerable<Move> GetMoves(Posicao de, Board board)
        {
            foreach (Posicao para in MovePos(de, board))
            {
                yield return new Normal(de, para);
            }
        }


        public override bool CapReiOponente(Posicao de, Board board)
        {
            return MovePos(de, board).Any(para =>
            {
                Peca peca = board[para];
                return peca != null && peca.Type == TypePeca.Rei;
            });
        }
    }
}