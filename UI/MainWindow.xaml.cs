using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessPieces;


namespace UI
{


    public partial class MainWindow : Window
    {
        private readonly Image[,] pecaImg = new Image[8, 8];

        //Highlight das pecas
        private readonly Rectangle[,] highlights = new Rectangle[8, 8]; 
        private readonly Dictionary<Posicao,Move> moveCache = new Dictionary<Posicao, Move>();


        private GameState gameState;
        private Posicao posSelect = null;




        public MainWindow()
        {
            InitializeComponent();
            IniciarBoard();

            gameState = new GameState(Jogador.White, Board.Inicial());
            DrawBoard(gameState.Board);

            SetCursor(gameState.CurrentJogador);

        }

        //Inicializa o board com imagens e highlights dos moves
        private void IniciarBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for(int c = 0; c < 8; c++)
                {
                    Image img = new Image();
                    pecaImg[r,c] = img;
                    GridPecas.Children.Add(img);

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        //Desenha as pecas no board com base no estado atual do jogo
        private void DrawBoard(Board board)
        {
            for (int r= 0 ; r < 8 ; r++)
            {
                for (int c = 0; c < 8;c++)
                {
                    Peca peca = board[r, c];
                    pecaImg[r,c].Source = Images.GetImage(peca);
                }
            }
        }


        //Detetar click no tabuleiro
        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMenuOnScreen()) //Ve se esta num menu
            {
                return;
            }
            Point point = e.GetPosition(BoardGrid);
            
            Posicao pos = PosicaoQuadrado(point);
            
            if(posSelect == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        //Converte o click numa posicao do board
        private Posicao PosicaoQuadrado(Point point)
        {
            double tamanhoBoard = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / tamanhoBoard);
            int col = (int)(point.X / tamanhoBoard);
            return new Posicao(row, col);
        }


        //procesa a posicao de origem
        private void OnFromPositionSelected (Posicao pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesPeca(pos);

            if(moves.Any())
            {
                posSelect = pos;
                CacheMoves(moves);
                MostrarHighlights();
            }
        }

        //Processa a posicao de destinho
        private void OnToPositionSelected (Posicao pos) 
        { 
           posSelect = null;
           EscondeHighlights();

           if (moveCache.TryGetValue(pos, out Move move))
           {
                if (move.Type == MoveType.PeaoPromocao)
                {
                    ProcessPromocao(move.DePos, move.ParaPos);
                }
                else
                {
                    ProcessMove(move);
                }
           }
        }


        //Processa promocao dos peos
        private void ProcessPromocao (Posicao de, Posicao para)
        {
            pecaImg[de.Row, para.Col].Source = Images.GetImage(gameState.CurrentJogador, TypePeca.Peao);
            pecaImg[de.Row, de.Col].Source = null;

            PromMenu promMenu = new PromMenu(gameState.CurrentJogador);
            MenuContainer.Content = promMenu;

            promMenu.PecaSelecionada += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new Promotion(de, para, type);
                ProcessMove(promMove);

                Sounds.PlaySound("promotion");
            };
        }


        //Processa moves normais
        private void ProcessMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentJogador);

            if (gameState.IsGameOver())
            {
                GameOver();
            }

            //SoundFX estao na pasta bin
            if (gameState.IsInCheck(gameState.CurrentJogador.Oponente()))
            {
                Sounds.PlaySound("move_check");
            }
            else
            {
                Sounds.PlaySound("move_self");
            }
        }


        //Armazendo os legal moves duma peca para mostrar os highlights
        private void CacheMoves (IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (Move move in moves)
            {
                moveCache[move.ParaPos] = move;
            }
        }

        private void MostrarHighlights () //Self Explanatory
        {
            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach (Posicao para in moveCache.Keys)
            {
                highlights[para.Row, para.Col].Fill = new SolidColorBrush(color);
            }
        }

        private void EscondeHighlights()//Also self explanatory
        {
            foreach (Posicao para in moveCache.Keys)
            {
                highlights[para.Row, para.Col].Fill = Brushes.Transparent;
            }
        }

        //Define o curso com base no jogador atual
        private void SetCursor(Jogador jogador)
        {
            if (jogador == Jogador.White)
            {
                Cursor = JogadorCursor.CursorW;
            }

            else
            {
                Cursor = JogadorCursor.CursorB;
            }
        }

        //Ver se ha algum menu aberto
        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        //Mostra menu de game over quando o jogo acaba
        private void GameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OpcaoSelect += opcao =>
            {
                if (opcao == Opcao.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }

                else
                {
                    Application.Current.Shutdown();
                }
            };
        }


        private void RestartGame()
        {
            posSelect = null;

            EscondeHighlights();
            moveCache.Clear();
            gameState = new GameState(Jogador.White, Board.Inicial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentJogador);
        }

        //Pause menu
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsMenuOnScreen() && e.Key == Key.Escape)
            {
                ShowPause();
            }
        }

        private void ShowPause()
        {
            Pause pauseMenu = new Pause();
            MenuContainer.Content = pauseMenu;

            pauseMenu.OpcaoSelected += opcao =>
            {
                MenuContainer.Content = null;

                if (opcao == Opcao.Restart)
                {
                    RestartGame();
                }
            };
        }
    }
}