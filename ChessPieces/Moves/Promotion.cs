using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Promotion : Move
    {
        public override MoveType Type => MoveType.PeaoPromocao;
        public override Posicao DePos { get; }
        public override Posicao ParaPos { get; }

        private readonly TypePeca newType;

        public Promotion(Posicao de, Posicao para, TypePeca newType)
        {
            DePos = de;
            ParaPos = para;
            this.newType = newType;
        }

        private Peca CriarPecaProm ( Jogador color)
        {
            return newType switch
            {
                TypePeca.Cavalo => new Cavalo(color),
                TypePeca.Bispo => new Bispo(color),
                TypePeca.Torre => new Torre(color),
                _ => new Rainha(color)
            };
        }

        public override void Execute(Board board)
        {
            Peca peao = board[DePos];
            board[DePos] = null;

            Peca promPeca = CriarPecaProm(peao.Color);
            promPeca.HasMoved = true;
            board[ParaPos] = promPeca;
        }
    }
}
