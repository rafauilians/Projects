using System;

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

        public JogoDaVelha()
        {
            tabuleiro = new char[Tamanho, Tamanho];
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
            int jogadorAtual = 0;
            while (true)
            {
                DesenharTabuleiro();
                Console.WriteLine($"Jogador {jogadorAtual + 1} ({SimbolosJogadores[jogadorAtual]}), faça sua jogada (ex: A1): ");
                string jogada = Console.ReadLine().ToUpper();

                if (jogada.Length == 2 &&
                    jogada[0] >= 'A' && jogada[0] < 'A' + Tamanho &&
                    jogada[1] >= '1' && jogada[1] < '1' + Tamanho)
                {
                    int coluna = jogada[0] - 'A';
                    int linha = jogada[1] - '1';

                    if (ValidarJogada(linha, coluna))
                    {
                        FazerJogada(jogadorAtual, linha, coluna);

                        if (ChecarVitoria(jogadorAtual))
                        {
                            DesenharTabuleiro();
                            Console.WriteLine($"Jogador {jogadorAtual + 1} ({SimbolosJogadores[jogadorAtual]}) venceu!");
                            break;
                        }

                        jogadorAtual = (jogadorAtual + 1) % NumJogadores;
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
            }
        }

        private void DesenharTabuleiro()
        {
            Console.Clear();
            Console.Write("   ");
            for (int i = 0; i < Tamanho; i++)
            {
                Console.Write($" {Convert.ToChar('A' + i)}  ");
            }
            Console.WriteLine();

            for (int i = 0; i < Tamanho; i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < Tamanho; j++)
                {
                    Console.Write($" {tabuleiro[i, j]} ");
                    if (j < Tamanho - 1) Console.Write("|");
                }
                Console.WriteLine();
                if (i < Tamanho - 1) Console.WriteLine("  " + new string('-', Tamanho * 4 - 1));
            }
        }

        private bool ValidarJogada(int linha, int coluna)
        {
            return linha >= 0 && linha < Tamanho && coluna >= 0 && coluna < Tamanho && tabuleiro[linha, coluna] == ' ';
        }

        private void FazerJogada(int jogador, int linha, int coluna)
        {
            tabuleiro[linha, coluna] = SimbolosJogadores[jogador];
        }

        private bool ChecarVitoria(int jogador)
        {
            char simbolo = SimbolosJogadores[jogador];

            // Checar linhas e colunas
            for (int i = 0; i < Tamanho; i++)
            {
                if (ChecarLinha(i, simbolo) || ChecarColuna(i, simbolo))
                {
                    return true;
                }
            }

            // Checar diagonais
            if (ChecarDiagonalPrincipal(simbolo) || ChecarDiagonalSecundaria(simbolo))
            {
                return true;
            }

            return false;
        }

        private bool ChecarLinha(int linha, char simbolo)
        {
            for (int j = 0; j < Tamanho; j++)
            {
                if (tabuleiro[linha, j] != simbolo) return false;
            }
            return true;
        }

        private bool ChecarColuna(int coluna, char simbolo)
        {
            for (int i = 0; i < Tamanho; i++)
            {
                if (tabuleiro[i, coluna] != simbolo) return false;
            }
            return true;
        }

        private bool ChecarDiagonalPrincipal(char simbolo)
        {
            for (int i = 0; i < Tamanho; i++)
            {
                if (tabuleiro[i, i] != simbolo) return false;
            }
            return true;
        }

        private bool ChecarDiagonalSecundaria(char simbolo)
        {
            for (int i = 0; i < Tamanho; i++)
            {
                if (tabuleiro[i, Tamanho - 1 - i] != simbolo) return false;
            }
            return true;
        }

        private void ExibirMensagemErro(string mensagem)
        {
            Console.WriteLine(mensagem);
        }
    }
}

