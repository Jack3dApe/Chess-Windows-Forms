using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Posicao
    {
        public int Row { get; }

        public int Col { get; }

        public Posicao(int row, int col)
        {
            Row = row;
            Col = col;
        }

        //retorna a cor da posição no board
        public Jogador CorPosicao()
        {
            if ((Row + Col) % 2 == 0)
            {
                return Jogador.White; // se a soma das coordenas for par, é branco
            }

            return Jogador.Black; //senao é preto
        }

        public override bool Equals(object obj)
        {
            return obj is Posicao posicao &&
                   Row == posicao.Row &&
                   Col == posicao.Col;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }

        public static bool operator ==(Posicao left, Posicao right)
        {
            return EqualityComparer<Posicao>.Default.Equals(left, right);
        }

        public static bool operator !=(Posicao left, Posicao right)
        {
            return !(left == right);
        }


        public static Posicao operator +(Posicao pos, Direcao dir)
        {
            return new Posicao(pos.Row + dir.RowDelta, pos.Col + dir.ColDelta);
        }

    }
}
