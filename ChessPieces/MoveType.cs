using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public enum MoveType //Fui saber que existem mais movimentos que nao tinha conhecimento mas foi em cima da hora portanto n os implementei
    {
        Normal,
        DoublePeao,
        PeaoPromocao
        //TODO: Castling
        //TODO: En passant
    }
}
