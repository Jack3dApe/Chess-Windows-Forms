using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Board
    {
        private readonly Peca[,] pecas = new Peca[8, 8];//Representacao do tabuleiro


        //Aceder a uma peca em certa posicao
        public Peca this[int row, int col] 
        {
            get { return pecas[row, col]; }
            set { pecas[row, col] = value; }

        }

        public Peca this[Posicao pos]
        {
            get { return this[pos.Row, pos.Col]; }
            set { this[pos.Row, pos.Col] = value; }
        }

        //Criar board com as pecas na posicao inicial
        public static Board Inicial()
        { 
            Board board = new Board();
            board.AddStartPecas();
            return board;
        }

        //Colocar as pecas nas posicoes inicias
        private void AddStartPecas()
        {
            this[0, 0] = new Torre(Jogador.Black);
            this[0, 1] = new Cavalo(Jogador.Black);
            this[0, 2] = new Bispo(Jogador.Black);
            this[0, 3] = new Rainha(Jogador.Black);
            this[0, 4] = new Rei(Jogador.Black);
            this[0, 5] = new Bispo(Jogador.Black);
            this[0, 6] = new Cavalo(Jogador.Black);
            this[0, 7] = new Torre(Jogador.Black);


            this[7, 0] = new Torre(Jogador.White);
            this[7, 1] = new Cavalo(Jogador.White);
            this[7, 2] = new Bispo(Jogador.White);
            this[7, 3] = new Rainha(Jogador.White);
            this[7, 4] = new Rei(Jogador.White);
            this[7, 5] = new Bispo(Jogador.White);
            this[7, 6] = new Cavalo(Jogador.White);
            this[7, 7] = new Torre(Jogador.White);

            for (int c = 0; c< 8; c++) //Adicionar os peos
            {
                this[1, c] = new Peao(Jogador.Black);
                this[6, c] = new Peao(Jogador.White);

            }
        }


        //Restringir posicoes ao tamanho do board
        public static bool IsInside(Posicao pos)
        {
            return pos.Row >=0 && pos.Row < 8 && pos.Col >= 0 && pos.Col < 8;
        }


        public bool IsEmpty(Posicao pos) //Nome da variavel diz tudo
        {
            return this[pos] == null;
        }


        //Detetar todas as posicoes do board q tem pecas
        public IEnumerable<Posicao> PosPecas ()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c=0; c< 8; c++)
                {
                    Posicao pos = new Posicao(r, c);

                    if (!IsEmpty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }


        //Posicoes das pecas de determinado jogador
        public IEnumerable<Posicao> PosPecasPara (Jogador jogador)
        {
            return PosPecas().Where(pos => this[pos].Color == jogador);
        }


        //Detete check
        public bool IsInCheck(Jogador jogador)
        {
            return PosPecasPara(jogador.Oponente()).Any(pos =>
            {
                Peca peca = this[pos];
                return peca.CapReiOponente(pos, this);
            });
        }

        public Board Copy()
        {
            Board copy = new Board();

            foreach (Posicao pos in PosPecas())
            {
                copy[pos] = this[pos].Copy();
            }

            return copy;
        }
    }
}
