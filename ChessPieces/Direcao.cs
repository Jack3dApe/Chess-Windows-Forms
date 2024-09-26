using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Direcao
    {
        //Definicoes de cada direcao
        public readonly static Direcao Cima = new Direcao(-1, 0);
        public readonly static Direcao Baixo = new Direcao(1, 0);
        public readonly static Direcao Esq = new Direcao(0, 1);
        public readonly static Direcao Drt = new Direcao(0, -1);

        //Direcoes diagonais
        public readonly static Direcao CimaDrt = Cima + Drt;
        public readonly static Direcao CimaEsq = Cima + Esq;
        public readonly static Direcao BaixoDrt = Baixo + Drt;
        public readonly static Direcao BaixoEsq = Baixo + Esq;



        public int RowDelta { get; }
        public int ColDelta { get; }


        public Direcao(int rowDelta, int colDelta)
        {
            RowDelta = rowDelta;
            ColDelta = colDelta;
        }

        //Combinar 2 direcoes
        public static Direcao operator +(Direcao dir1, Direcao dir2) 
        {
            return new Direcao(dir1.RowDelta + dir2.RowDelta, dir1.ColDelta + dir2.ColDelta);
        }

        public static Direcao operator *(int scalar, Direcao dir) 
        {
            return new Direcao(scalar * dir.RowDelta, scalar * dir.ColDelta);
        }
    }
}
