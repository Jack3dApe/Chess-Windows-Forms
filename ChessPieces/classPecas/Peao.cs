using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces
{
    public class Peao : Peca
    {
        public override TypePeca Type => TypePeca.Peao;
        public override Jogador Color { get; }

        private readonly Direcao frente;


        //Verificar qual direcao e cor do peao
        public Peao(Jogador color)
        {
            Color = color;

            if (color == Jogador.White)
            {
                frente = Direcao.Cima;
            }

            else if (color == Jogador.Black)
            {
                frente= Direcao.Baixo;
            }
        }

        
        public override Peca Copy()
        {
            Peao copy = new Peao(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }


        //Check se a posicao e valida
        private static bool PosValid(Posicao pos, Board board)
        {
            return Board.IsInside(pos) && board.IsEmpty(pos);
        }


        //Captura de pecas do oponente
        private bool CapturaValid(Posicao pos, Board board)
        {
            if(!Board.IsInside(pos) || board.IsEmpty(pos)) {
                return false;
            }

            return board[pos].Color != Color;

        }


        //metodo q retorna as promocoes possiveis
        private static IEnumerable<Move> PromMoves(Posicao de, Posicao para)
        {
            yield return new Promotion (de, para, TypePeca.Cavalo);
            yield return new Promotion(de, para, TypePeca.Bispo);
            yield return new Promotion(de, para, TypePeca.Torre);
            yield return new Promotion(de, para, TypePeca.Rainha);
        }


        //Meteodo q retorn as possiveis a frente possiveis
        private IEnumerable<Move> FrenteMoves(Posicao de, Board board)
        {
            Posicao oneMovePos = de + frente;

            if (PosValid(oneMovePos, board))
            {
                if (oneMovePos.Row == 0 || oneMovePos.Row == 7)
                {
                    foreach (Move promMove in PromMoves(de, oneMovePos))
                    {
                        yield return promMove;
                    }
                }
                else
                {
                    yield return new Normal(de, oneMovePos);
                }

                yield return new Normal(de, oneMovePos);


                //Deixa mexer 2 posicoes no primeiro move
                Posicao twoMovesPos = oneMovePos + frente;

                if (!HasMoved && PosValid(twoMovesPos, board))
                {
                    yield return new Normal(de, twoMovesPos);
                }
            }
        }


        //Movimentos diagonais para realizar capturas
        private IEnumerable<Move> DiagonalMoves(Posicao de, Board board)
        {
            foreach (Direcao dir in new Direcao[] { Direcao.Drt, Direcao.Esq })
            {
                Posicao para = de + frente + dir;

                if (CapturaValid(para, board))
                {
                    if (para.Row == 0 || para.Row == 7)
                    {
                        foreach (Move promMove in PromMoves(de, para))
                        {
                            yield return promMove;
                        }
                    }
                    else
                    {
                        yield return new Normal(de, para);
                    }

                }
            }
        }


        //Ve os movimentos possiveis do peao
        public override IEnumerable<Move> GetMoves(Posicao de, Board board)
        {
            return FrenteMoves(de, board).Concat(DiagonalMoves(de, board));
        }


        //Ve se pode capturar o rei do oponente
        public override bool CapReiOponente(Posicao de, Board board)
        {
            return DiagonalMoves(de, board).Any(move =>
            {
                Peca peca = board[move.ParaPos];
                return peca == null && peca.Type == TypePeca.Rei;
            });
        }
    }
}
