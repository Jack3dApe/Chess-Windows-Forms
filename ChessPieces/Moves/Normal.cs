using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    internal class Normal : Move
    {
        public override MoveType Type => MoveType.Normal;
        public override Posicao DePos {  get; }
        public override Posicao ParaPos { get; }

        public Normal(Posicao de, Posicao para) 
        {
            DePos = de;
            ParaPos = para;
        }

        public override void Execute(Board board)
        {
            Peca peca = board[DePos];
            board[ParaPos] = peca;
            board[DePos] = null;
            peca.HasMoved = true;
        }

    }
}
