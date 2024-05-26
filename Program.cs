using System;
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
                        Tela.imprimirTabuleiro(partida.tab);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("\n==================");
                        Console.ResetColor();
                        Console.WriteLine("Turno: " + partida.turno);
                        Console.Write("Aguardando jogada: " + partida.jogadorAtual);

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("\n==================");
                        Console.ResetColor();
                        Console.Write("\nOrigem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("\n==================");
                        Console.ResetColor();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.Write(e.Message);
                        Console.ReadLine();
                    }
                }
                

                Tela.imprimirTabuleiro(partida.tab);

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}