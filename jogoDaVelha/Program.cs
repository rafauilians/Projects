namespace JogoDaVelha
{
    class Program
    {
        static void Main(string[] args)
        {
            JogoDaVelha jogo = new JogoDaVelha();
            jogo.IniciarJogo();
        }
    }

    class JogoDaVelha
    {
        private const int Tamanho = 5;
        private const int NumJogadores = 3;
        private readonly char[] SimbolosJogadores = { 'X', 'O', 'Z' };

        private char[,] tabuleiro;
        private int[] pontuacaoJogadores;

        public JogoDaVelha()
        {
            tabuleiro = new char[Tamanho, Tamanho];
            pontuacaoJogadores = new int[NumJogadores];
            InicializarTabuleiro();
        }

        private void InicializarTabuleiro()
        {
            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    tabuleiro[i, j] = ' ';
                }
            }
        }

        public void IniciarJogo()
        {
            do
            {
                InicializarTabuleiro();
                JogarPartida();
            } while (true);
        }

        private void JogarPartida()
        {
            int jogadorAtual = 0;
            while (true)
            {
                DesenharTabuleiro();
                string jogada = SolicitarJogada(jogadorAtual);

                if (ProcessarJogada(jogadorAtual, jogada))
                {
                    if (ChecarVitoria(jogadorAtual, jogada))
                    {
                        DesenharTabuleiro();
                        Console.WriteLine($"Jogador {jogadorAtual + 1} ({SimbolosJogadores[jogadorAtual]}) venceu!");
                        pontuacaoJogadores[jogadorAtual]++;
                        MostrarPontuacao();
                        break;
                    }

                    jogadorAtual = (jogadorAtual + 1) % NumJogadores;
                }
            }

            Console.WriteLine("Pressione Enter para reiniciar o jogo...");
            Console.ReadLine();
        }

        private string SolicitarJogada(int jogadorAtual)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Jogador {jogadorAtual + 1} ({SimbolosJogadores[jogadorAtual]}), faça sua jogada (ex: A1): ");
            return Console.ReadLine().ToUpper();
        }

        private bool ProcessarJogada(int jogadorAtual, string jogada)
        {
            if (jogada.Length == 2 &&
                jogada[0] >= 'A' && jogada[0] < 'A' + Tamanho &&
                jogada[1] >= '1' && jogada[1] < '1' + Tamanho)
            {
                int coluna = jogada[0] - 'A';
                int linha = jogada[1] - '1';

                if (ValidarJogada(linha, coluna))
                {
                    FazerJogada(jogadorAtual, linha, coluna);
                    return true;
                }
                else
                {
                    ExibirMensagemErro("Jogada inválida, tente novamente.");
                }
            }
            else
            {
                ExibirMensagemErro("Entrada inválida, tente novamente.");
            }
            return false;
        }

        private bool ValidarJogada(int linha, int coluna)
        {
            return linha >= 0 && linha < Tamanho && coluna >= 0 && coluna < Tamanho && tabuleiro[linha, coluna] == ' ';
        }

        private void FazerJogada(int jogador, int linha, int coluna)
        {
            tabuleiro[linha, coluna] = SimbolosJogadores[jogador];
        }

        private bool ChecarVitoria(int jogador, int linha, int coluna)
        {
            char simbolo = SimbolosJogadores[jogador];
            return ChecarDirecao(linha, coluna, 1, 0, simbolo) || // horizontal
                   ChecarDirecao(linha, coluna, 0, 1, simbolo) || // vertical
                   ChecarDirecao(linha, coluna, 1, 1, simbolo) || // diagonal principal
                   ChecarDirecao(linha, coluna, 1, -1, simbolo);  // diagonal secundária
        }

        private bool ChecarVitoria(int jogadorAtual, string jogada)
        {
            int coluna = jogada[0] - 'A';
            int linha = jogada[1] - '1';
            return ChecarVitoria(jogadorAtual, linha, coluna);
        }

        private bool ChecarDirecao(int linha, int coluna, int deltaLinha, int deltaColuna, char simbolo)
        {
            int contagem = 1;

            for (int i = 1; i < 3; i++)
            {
                int novaLinha = linha + i * deltaLinha;
                int novaColuna = coluna + i * deltaColuna;

                if (novaLinha >= 0 && novaLinha < Tamanho && novaColuna >= 0 && novaColuna < Tamanho && tabuleiro[novaLinha, novaColuna] == simbolo)
                {
                    contagem++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 3; i++)
            {
                int novaLinha = linha - i * deltaLinha;
                int novaColuna = coluna - i * deltaColuna;

                if (novaLinha >= 0 && novaLinha < Tamanho && novaColuna >= 0 && novaColuna < Tamanho && tabuleiro[novaLinha, novaColuna] == simbolo)
                {
                    contagem++;
                }
                else
                {
                    break;
                }
            }

            return contagem >= 3;
        }

        private void DesenharTabuleiro()
        {
            Console.Clear();
            Console.WriteLine("    A   B   C   D   E  ");
            Console.WriteLine("  +---+---+---+---+---+");

            for (int i = 0; i < Tamanho; i++)
            {
                Console.Write($"{i + 1} |");
                for (int j = 0; j < Tamanho; j++)
                {
                    if (tabuleiro[i, j] == 'X')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (tabuleiro[i, j] == 'O')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (tabuleiro[i, j] == 'Z')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else
                        Console.ResetColor();

                    Console.Write($" {tabuleiro[i, j]} ");
                    Console.ResetColor();
                    Console.Write("|");
                }
                Console.WriteLine();
                if (i < Tamanho - 1) Console.WriteLine("  +---+---+---+---+---+");
            }

            Console.WriteLine("  +---+---+---+---+---+");
        }

        private void MostrarPontuacao()
        {
            Console.WriteLine("Pontuação Atual:");
            for (int i = 0; i < NumJogadores; i++)
            {
                Console.WriteLine($"Jogador {i + 1} ({SimbolosJogadores[i]}): {pontuacaoJogadores[i]} pontos");
            }
        }

        private void ExibirMensagemErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }
    }
}