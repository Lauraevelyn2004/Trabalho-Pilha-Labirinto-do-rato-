using System;
using System.Collections.Generic;
using System.Threading;
class Labirinto
{
    private const int limit = 15;

    static void mostrarLabirinto(char[,] array)
    {
        for (int i = 0; i < limit; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < limit; j++)
            {
                Console.Write($" {array[i, j]} ");
            }
        }
        Console.WriteLine();
    }

    static void criaLabirinto(char[,] meuLab)
    {
        Random random = new Random();
        for (int i = 0; i < limit; i++)
        {
            for (int j = 0; j < limit; j++)
            {
                meuLab[i, j] = random.Next(4) == 1 ? '|' : '.';
            }
        }

        for (int i = 0; i < limit; i++)
        {
            meuLab[0, i] = '*';
            meuLab[limit - 1, i] = '*';
            meuLab[i, 0] = '*';
            meuLab[i, limit - 1] = '*';
        }

        int x = random.Next(limit);
        int y = random.Next(limit);
        meuLab[x, y] = 'Q';
    }

    static void resolveLabirinto(char[,] labirinto, int i, int j)
    {
        Stack<int> pilha_i = new Stack<int>();
        Stack<int> pilha_j = new Stack<int>();
        bool encontrou = false;

        while (encontrou == false) // se nao achar o queijo
        {


            labirinto[i, j] = 'v';

            // Verificar se o queijo está ao lado antes de qualquer coisa
            if (labirinto[i, j + 1] == 'Q')
            {
                j++; encontrou = true;
                break;
            }
            else if (labirinto[i + 1, j] == 'Q')
            {
                i++; encontrou = true;
                break;
            }
            else if (labirinto[i, j - 1] == 'Q')
            {
                j--; encontrou = true;
                break;
            }
            else if (labirinto[i - 1, j] == 'Q')
            {
                i--; encontrou = true;
                break;
            }
            // Caso não esteja, segue a lógica normal

            if (labirinto[i, j + 1] == '.' || labirinto[i, j + 1] == 'Q')
            {
                pilha_i.Push(i);
                pilha_j.Push(j);
                j++;
            }
            // tentar para baixo = i+1
            else if (labirinto[i + 1, j] == '.' || labirinto[i + 1, j] == 'Q')
            {
                pilha_i.Push(i);
                pilha_j.Push(j);
                i++;
            }
            // tentar pra traz = j-1
            else if (labirinto[i, j - 1] == '.' || labirinto[i, j - 1] == 'Q')
            {
                pilha_i.Push(i);
                pilha_j.Push(j);
                j--;
            }
            // tentar pra cima = i-1
            else if (labirinto[i - 1, j] == '.' || labirinto[i - 1, j] == 'Q')
            {
                pilha_i.Push(i);
                pilha_j.Push(j);
                i--;
            }
            // voltar if count > 0 ---- i = pop pilha_i    ---- j = pop pilha_j
            else if (pilha_i.Count > 0) // voltar
            {
                labirinto[i, j] = 'x'; // marca como sem saída
                i = pilha_i.Pop();
                j = pilha_j.Pop();
            }
            else // não tem jeito
            {
                Console.WriteLine("Não tem jeito de encontrar esse queijinho (;-;) ");
                return; 
            }

            Thread.Sleep(300);
            Console.Clear();
            mostrarLabirinto(labirinto);

        }
        // se achou o queijo
        Console.Clear();
        mostrarLabirinto(labirinto);
        Console.WriteLine($"\n O ratinho encontrou o queijo na posição ({i},{j}) (^-^) !! ");


    }

    static void Main(string[] args)
    {
        char[,] maze = new char[limit, limit];
        int x, y;
        criaLabirinto(maze);
        mostrarLabirinto(maze);
        Console.WriteLine("\nPosições iniciais (linha e coluna):");
        x = Convert.ToInt32(Console.ReadLine());
        y = Convert.ToInt32(Console.ReadLine());
        resolveLabirinto(maze, x, y);
        Console.ReadKey();
    }
}