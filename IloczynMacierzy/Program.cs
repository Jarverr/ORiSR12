using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IloczynMacierzy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program obliczający mnozenie dwóch macierzy");
            Console.Write("Podaj rozmiar macierzy 1:\nLiczba wierszy:");
            Int32.TryParse(Console.ReadLine(), out int w);
            Console.Write("\nLiczba kolumn:");
            Int32.TryParse(Console.ReadLine(), out int k);
            int w2 = k;
            Console.Write("Podaj rozmiar macierzy 2:\nLiczba kolumn:");
            Int32.TryParse(Console.ReadLine(), out int k2);
            int[][] matrix1 = new int[w][];
            int[][] matrix2 = new int[w2][];
            int[][] restMatrix = new int[w][];
            Random rnd = new Random();
            for (int i = 0; i < matrix1.Length; i++)
            {
                matrix1[i] = new int[k];
                restMatrix[i] = new int[k2];
                for (int j = 0; j < matrix1[i].Length; j++)
                {
                    matrix1[i][j] = rnd.Next(-10, 10);
                }
                for (int j = 0; j < restMatrix[i].Length; j++)
                {
                    restMatrix[i][j] = 0;
                }
            }
            for (int i = 0; i < matrix2.Length; i++)
            {
                matrix2[i] = new int[k2];
                for (int j = 0; j < matrix2[i].Length; j++)
                {
                    matrix2[i][j] = rnd.Next(-10, 10);
                }
            }
            //Console.WriteLine("Utworzone macierze to:\n\tMacierz 1:");
            //for (int i = 0; i < matrix1.Length; i++)
            //{
            //    for (int j = 0; j < matrix1[i].Length; j++)
            //    {
            //        Console.Write($"{matrix1[i][j]} ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("\n\tMacierz 2:");
            //for (int i = 0; i < matrix2.Length; i++)
            //{
            //    for (int j = 0; j < matrix2[i].Length; j++)
            //    {
            //        Console.Write($"{matrix2[i][j]} ");
            //    }
            //    Console.WriteLine();
            //}

            //program
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Sekwencyjnie");
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < k2; j++)
                {
                    for (int p = 0; p < matrix2.Length; p++)
                    {
                        restMatrix[i][j] += matrix1[i][p] * matrix2[p][j];
                    }
                }
            }
            //Console.WriteLine("\n\tMacierz Wynikowa:");
            //for (int i = 0; i < restMatrix.Length; i++)
            //{
            //    for (int j = 0; j < restMatrix[i].Length; j++)
            //    {
            //        Console.Write($"{restMatrix[i][j]} ");
            //    }
            //    Console.WriteLine();
            //}
            Console.WriteLine($"Time: {sw.Elapsed}");
            for (int i = 0; i < restMatrix.Length; i++)
            {
                for (int j = 0; j < restMatrix[i].Length; j++)
                {
                    restMatrix[i][j] = 0;
                }
            }
            sw.Reset(); sw.Start();




            Console.WriteLine("\n");
            Console.WriteLine("Równolegle");
            Parallel.For(0, w, i =>
           {
               for (int j = 0; j < k2; j++)
               {
                   for (int p = 0; p < matrix2.Length; p++)
                   {
                       restMatrix[i][j] += matrix1[i][p] * matrix2[p][j];
                   }
               }
           });
            //Console.WriteLine("\n\tMacierz Wynikowa:");
            //for (int i = 0; i < restMatrix.Length; i++)
            //{
            //    for (int j = 0; j < restMatrix[i].Length; j++)
            //    {
            //        Console.Write($"{restMatrix[i][j]} ");
            //    }
            //    Console.WriteLine();
            //}
            Console.WriteLine($"Time: {sw.Elapsed}");
            for (int i = 0; i < restMatrix.Length; i++)
            {
                for (int j = 0; j < restMatrix[i].Length; j++)
                {
                    restMatrix[i][j] = 0;
                }
            }
            sw.Reset(); sw.Start();



            Console.WriteLine("\n");
            Console.WriteLine("Równolegle v2");
            Parallel.For(0, w, i =>
            {
                Parallel.For(0, k2, j =>
                 {
                     for (int p = 0; p < matrix2.Length; p++)
                     {
                         restMatrix[i][j] += matrix1[i][p] * matrix2[p][j];
                     }
                 });
            });
            //Console.WriteLine("\n\tMacierz Wynikowa:");
            //for (int i = 0; i < restMatrix.Length; i++)
            //{
            //    for (int j = 0; j < restMatrix[i].Length; j++)
            //    {
            //        Console.Write($"{restMatrix[i][j]} ");
            //    }
            //    Console.WriteLine();
            //}
            Console.WriteLine($"Time: {sw.Elapsed}");
            sw.Reset(); sw.Start();
            for (int i = 0; i < restMatrix.Length; i++)
            {
                for (int j = 0; j < restMatrix[i].Length; j++)
                {
                    restMatrix[i][j] = 0;
                }
            }





            Console.WriteLine("\n");
            Console.WriteLine("Równolegle v3");
            Task<int>[] tasks=new Task<int>[w*k2];
            for (int i=0,counter=0;i< w; i++)
            {
                for(int j=0; j< k2; j++, counter++)
                 {
                    tasks[counter]= Multiplay(i, j, matrix1, matrix2);
                 }
            }
            var done = Task.WhenAll(tasks);
            var doneInt = done.Result;
            //Console.WriteLine("\n\tMacierz Wynikowa:");
            for (int i = 0,counter=0; i < restMatrix.Length; i++)
            {
                for (int j = 0; j < restMatrix[i].Length; j++,counter++)
                {
                    restMatrix[i][j] = doneInt[counter];
            //        Console.Write($"{restMatrix[i][j]} ");
                }
            //    Console.WriteLine();
            }
            Console.WriteLine($"Time: {sw.Elapsed}");
            sw.Reset(); sw.Start();
            Console.ReadLine();

        }
        static Task<int> Multiplay(int i, int j,int[][] matrix1, int[][] matrix2)
        {
            var task = new Task<int>(() =>
            {
                int result = 0;
                for (int p = 0; p < matrix2.Length; p++)
                {
                    result += matrix1[i][p] * matrix2[p][j];
                }
                return result;
            });
            task.Start();
            return task;
        }
    }
}
