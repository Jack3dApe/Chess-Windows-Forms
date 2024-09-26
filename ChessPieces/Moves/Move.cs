using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Posicao DePos { get; }
        public abstract Posicao ParaPos { get; }

        public abstract void Execute(Board board);


        //Legal moves para qando o rei esta em check
        public virtual bool LegalMove(Board board)
        {
            Jogador jogador = board[DePos].Color;
            Board boardCopy = board.Copy();
            Execute(boardCopy);
            return !boardCopy.IsInCheck(jogador);
        }
    }
}
