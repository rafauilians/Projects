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
        private char[,] tabuleiro;
        private int tamanho = 5;
        private int numJogadores = 3;
        private char[] simbolosJogadores = { 'X', 'O', 'Z' };

        public JogoDaVelha()
        {
            tabuleiro = new char[tamanho, tamanho];
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
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
                Console.WriteLine($"Jogador {jogadorAtual + 1} ({simbolosJogadores[jogadorAtual]}), faça sua jogada (linha e coluna): ");
                int linha = int.Parse(Console.ReadLine());
                int coluna = int.Parse(Console.ReadLine());

                if (ValidarJogada(linha, coluna))
                {
                    FazerJogada(jogadorAtual, linha, coluna);

                    if (ChecarVitoria(jogadorAtual))
                    {
                        DesenharTabuleiro();
                        Console.WriteLine($"Jogador {jogadorAtual + 1} ({simbolosJogadores[jogadorAtual]}) venceu!");
                        break;
                    }

                    jogadorAtual = (jogadorAtual + 1) % numJogadores;
                }
                else
                {
                    Console.WriteLine("Jogada inválida, tente novamente.");
                }
            }
        }

        private void DesenharTabuleiro()
        {
            Console.Clear();
            Console.Write("   ");
            for (int i = 0; i < tamanho; i++)
            {
                Console.Write($" {i}  ");
            }
            Console.WriteLine();

            for (int i = 0; i < tamanho; i++)
            {
                Console.Write($"{i} ");
                for (int j = 0; j < tamanho; j++)
                {
                    Console.Write($" {tabuleiro[i, j]} ");
                    if (j < tamanho - 1) Console.Write("|");
                }
                Console.WriteLine();
                if (i < tamanho - 1) Console.WriteLine("  " + new string('-', tamanho * 4 - 1));
            }
        }

        private bool ValidarJogada(int linha, int coluna)
        {
            return linha >= 0 && linha < tamanho && coluna >= 0 && coluna < tamanho && tabuleiro[linha, coluna] == ' ';
        }

        private void FazerJogada(int jogador, int linha, int coluna)
        {
            tabuleiro[linha, coluna] = simbolosJogadores[jogador];
        }

        private bool ChecarVitoria(int jogador)
        {
            char simbolo = simbolosJogadores[jogador];

            // Checar linhas e colunas
            for (int i = 0; i < tamanho; i++)
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
            for (int j = 0; j < tamanho; j++)
            {
                if (tabuleiro[linha, j] != simbolo) return false;
            }
            return true;
        }

        private bool ChecarColuna(int coluna, char simbolo)
        {
            for (int i = 0; i < tamanho; i++)
            {
                if (tabuleiro[i, coluna] != simbolo) return false;
            }
            return true;
        }

        private bool ChecarDiagonalPrincipal(char simbolo)
        {
            for (int i = 0; i < tamanho; i++)
            {
                if (tabuleiro[i, i] != simbolo) return false;
            }
            return true;
        }

        private bool ChecarDiagonalSecundaria(char simbolo)
        {
            for (int i = 0; i < tamanho; i++)
            {
                if (tabuleiro[i, tamanho - 1 - i] != simbolo) return false;
            }
            return true;
        }
    }
}
