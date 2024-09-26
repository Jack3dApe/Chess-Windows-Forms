using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class GameState
    {
        public Board Board {  get; }
        public Jogador CurrentJogador { get; private set; }
        public Resultado Resultado { get; private set; } = null;

        public GameState(Jogador jogador, Board board)//Inicializa o jogador e o tabuleiro
        {
            CurrentJogador = jogador;
            Board = board;
        }

        public IEnumerable<Move> LegalMovesPeca(Posicao pos) //todos os movimentos legais para uma peça em uma posição específica
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentJogador)
            {
                return Enumerable.Empty<Move>();
            }

            Peca peca = Board[pos];
            IEnumerable<Move> movePossible = peca.GetMoves(pos, Board);
            return movePossible.Where(move => move.LegalMove(Board));
        }

        public void MakeMove(Move move) //Executa o movimento fornecido e atualiza o jogador atual
        {
            move.Execute(Board);
            CurrentJogador = CurrentJogador.Oponente();
            CheckGameOver();
        }


        public bool IsInCheck(Jogador jogador) //verifica se um jogador esta em check
        {
            return Board.IsInCheck(jogador);
        }

        public IEnumerable<Move> AllLegalMoves (Jogador jogador) //todos os movimentos legais possíveis para um jogador específico
        {
            IEnumerable<Move> movePossible = Board.PosPecasPara(jogador).SelectMany(pos =>
            {
                Peca peca = Board[pos];
                return peca.GetMoves(pos, Board);
            });

            return movePossible.Where(move=> move.LegalMove(Board));
        }


        //Ver se o jogo acabou e atualiza o resultado
        private void CheckGameOver()
        {
            if (!AllLegalMoves(CurrentJogador).Any())
            {
                if(Board.IsInCheck(CurrentJogador))
                {
                    Resultado = Resultado.Win(CurrentJogador.Oponente());
                }


                else
                {
                    Resultado = Resultado.Empate(EndGame.Stalemate);
                }
            }
        }

        public bool IsGameOver()
        {
            return Resultado != null;
        }
    }
}
