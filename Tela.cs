using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tab);
            imprimirPecasCapturadas(partida);
            Console.WriteLine("Turno: " + partida.turno);

            if (!partida.terminada)
            {
                Console.Write("Aguardando jogada: " + partida.jogadorAtual);
                if (partida.xeque)
                {
                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" \u001b[31;1m\u001b[5m\u001b[7m XEQUE! \x1b[0m");
                    //Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("\u001b[48;5;32m\u001b[1m XEQUEMATE! \u001b[0m");
                Console.Write("Vencedor: " + partida.jogadorAtual);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=============================");
            Console.ResetColor();
        }
        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=============================");
            Console.ResetColor();
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Pretas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=============================");
            Console.ResetColor();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\u001b[48;5;17m    A B C D E F G H     ");
            Console.WriteLine("   ------------------   ");
            Console.ResetColor();
            for (int i = 0; i < tab.linhas; i++)
            {
                int cont = 1;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"\u001b[48;5;17m {8 - i}| ");
                Console.ResetColor();
                for (int j = 0; j < tab.colunas; j++)
                {

                    if (i % 2 == 0)
                    {

                        if (j % 2 == 1 && cont % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                        }
                    }
                    else
                    {
                        if (j % 2 == 0 && cont % 2 == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                        }
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = ConsoleColor.Black;
                    cont++;
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"\u001b[48;5;17m |");
                Console.WriteLine($"{8 - i} ");
                Console.ResetColor();

            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\u001b[48;5;17m   ------------------   ");
            Console.WriteLine("    A B C D E F G H     ");
            Console.ResetColor();
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\u001b[48;5;17m    A B C D E F G H     ");
            Console.WriteLine(" \u001b[48;5;17m  ------------------   ");
            Console.ResetColor();

            for (int i = 0; i < tab.linhas; i++)
            {
                int cont = 1;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"\u001b[48;5;17m {8 - i}| ");
                Console.ResetColor();
                for (int j = 0; j < tab.colunas; j++)
                {

                    if (i % 2 == 0)
                    {

                        if (j % 2 == 1 && cont % 2 == 0)
                        {

                            if (posicoesPossiveis[i, j])
                            {
                                Console.BackgroundColor = fundoAlterado;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkCyan;
                            }
                        }
                        else
                        {
                            if (posicoesPossiveis[i, j])
                            {
                                Console.BackgroundColor = fundoAlterado;
                            }
                        }
                    }
                    else //ver aqui
                    {
                        if (j % 2 == 0 && cont % 2 == 1)
                        {
                            if (posicoesPossiveis[i, j])
                            {
                                Console.BackgroundColor = fundoAlterado;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkCyan;
                            }
                        }
                        else
                        {
                            if (posicoesPossiveis[i, j])
                            {
                                Console.BackgroundColor = fundoAlterado;
                            }
                        }
                    }
                    imprimirPeca(tab.peca(i, j));
                    cont++;
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"\u001b[48;5;17m |");
                Console.WriteLine($"{8 - i} ");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\u001b[48;5;17m   ------------------   ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\u001b[48;5;17m    A B C D E F G H     ");
            Console.ResetColor();
            Console.BackgroundColor = fundoOriginal;
        }
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine().ToLower();
            if (s == null || s == "")
            {
                throw new TabuleiroException("Por favor, escolha uma posição para realizar a jogada!");
            }
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("  ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
