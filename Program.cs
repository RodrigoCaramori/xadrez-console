﻿using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {

                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.Write("\nOrigem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);
                        Tela.imprimirPecasCapturadas(partida);
                        Console.Write("\nDestino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino);


                    }
                    catch (TabuleiroException e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(e.Message);
                        Console.ResetColor();
                        Console.ReadLine();
                    }
                    catch (FormatException e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(e.Message);
                        Console.ResetColor();
                        Console.ReadLine();
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(e.Message);
                        Console.ResetColor();
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(e.Message);
                        Console.ResetColor();
                        Console.ReadLine();
                    }

                }
                Console.Clear();
                Tela.imprimirPartida(partida);

            }
            catch (TabuleiroException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(e.Message);
                Console.ResetColor();
            }

        }
    }
}