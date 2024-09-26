using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public abstract class Peca
    {
        public abstract TypePeca Type {  get; }
        public abstract Jogador Color { get; }
        public bool HasMoved { get; set; } = false; //Var name says it all :p

        public abstract Peca Copy();


        public abstract IEnumerable<Move> GetMoves(Posicao de, Board board);

        protected IEnumerable<Posicao> MovePositionsInDir(Posicao de, Board board, Direcao dir) //Moves possiveis a partir da posicao atual da peca
        {
            for(Posicao pos = de + dir; Board.IsInside(pos); pos += dir)
            {
                if(board.IsEmpty(pos))
                {
                    yield return pos;
                    continue;
                }

                Peca peca = board[pos];

                if(peca.Color != Color) //Captura de pecas inimigas
                {
                    yield return pos;
                }

                yield break;
            }
        }


        //Gera todas as posicoes em varias direcoes
        protected IEnumerable<Posicao> MovePositionsInDirs(Posicao de, Board board, Direcao[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionsInDir(de, board, dir));
        }


        //Check e Checkmate
        public virtual bool CapReiOponente (Posicao de, Board board)
        {
            return GetMoves(de, board).Any(move =>
            {
                Peca peca = board[move.ParaPos];
                return peca != null && peca.Type == TypePeca.Rei;
            });
        }
    }

}
