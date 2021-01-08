using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortowanieV2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sortowanie bąbelkowe:");
            Console.Write("Podaj liczbę elementów tablicy:");
            Int32.TryParse(Console.ReadLine(), out int amount);
            int[] toSort = new int[amount];
            int[] sorted = new int[amount];
            var rnd = new Random();
            for (int i = 0; i < amount; i++)
            {
                toSort[i] = rnd.Next(1000);
                sorted[i] = toSort[i];
            }

           

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Sekwencyjnie:");
            bool amIDoingAnything = true;
            int temp;
            while (amIDoingAnything)
            {
                amIDoingAnything = false;
                for (int i = 0; i < toSort.Length - 1; i++)
                {
                    if (toSort[i] > toSort[i + 1])
                    {
                        temp = toSort[i + 1];
                        toSort[i + 1] = toSort[i];
                        toSort[i] = temp;
                        amIDoingAnything = true;
                    }
                }

            }
            foreach (var item in toSort)
            {
                Console.Write(" "+item);
            }
            Console.WriteLine("\nTime: "+sw.ElapsedMilliseconds);
            toSort = sorted;
            sw.Reset();
            sw.Start();

            Task[] tasks = new Task[4];
            if (toSort.Length > 7)
            {
                tasks[0] = sortAsync(toSort, 0, toSort.Length / 4);
                tasks[1] = sortAsync(toSort, toSort.Length / 4+1, toSort.Length / 2);
                tasks[2] = sortAsync(toSort, toSort.Length / 2+1, (toSort.Length / 4) + (toSort.Length / 2));
                tasks[3] = sortAsync(toSort, (toSort.Length / 4) + (toSort.Length / 2)+1, toSort.Length - 1);
            }
      
            Task.WaitAll(tasks);
            foreach (var item in toSort)
            {
                Console.Write(" " + item);
            }
            Console.WriteLine("\nTime: " + sw.ElapsedMilliseconds);
            sw.Reset();
            sw.Stop();
            Console.ReadLine();
        }
        
        static async Task sortAsync(int[] toSort, int p,int k)
        {
            object _ = new object();
            bool amIDoingAnything = true;
            int temp;
            while (amIDoingAnything)
            {
                amIDoingAnything = false;
                for (int i = p; i < k; i++)
                {
                    if (toSort[i] > toSort[i + 1])
                    {
                        lock (_)
                        {
                            temp = toSort[i + 1];
                            toSort[i + 1] = toSort[i];
                            toSort[i] = temp;
                            amIDoingAnything = true;
                        }
                    }
                }

            }
        }
    }
}
