using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortowanieV3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sortowanie przez wybór:");
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
            //toSort[0] = 1;
            //toSort[1] = 13;
            //toSort[2] = 21;
            //toSort[3] = 11;
            //toSort[4] = 4;
            //toSort[5] = 3;
            //toSort[6] = 17;
            //toSort[7] = 29;
            //toSort[8] = 9;
            //foreach (var item in toSort)
            //{
            //    Console.Write(item + " ");
            //}

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Sekwencyjnie:");
            IntArraySelectionSort(toSort);
            foreach (var item in toSort)
            {
                Console.Write(item + " ");
            }


            toSort = sorted;
            //toSort[0] = 1;
            //toSort[1] = 21;
            //toSort[2] = 13;
            //toSort[3] = 11;
            //toSort[4] = 4;
            //toSort[5] = 3;
            //toSort[6] = 17;
            //toSort[7] = 29;
            //toSort[8] = 9;
            Console.WriteLine("Time: "+sw.Elapsed);
            sw.Reset();

            sw.Start();
            Console.WriteLine("Równolegle: ");

            int N = toSort.Length;
            Task<int>[] tasks = new Task<int>[2];
            for (int i = 0; i < N / 2; i++)
            {
                tasks[0] = IntArrayMinAsync(toSort, i);
                tasks[1] = IntArrayMaxAsync(toSort, N  -1- i);
                var res = Task.WhenAll(tasks);
                if (i != res.Result[0])
                    exchange(toSort, i, res.Result[0]);
                if (N -1- i != res.Result[1]&&i!=res.Result[1])
                    exchange(toSort, N -1- i, res.Result[1]);
                else if(N - 1 - i != res.Result[1])
                    exchange(toSort, N - 1 - i, res.Result[0]);
            }
            foreach (var item in toSort)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("Time: " + sw.Elapsed);

            sw.Stop();
            Console.ReadLine();
        }
        public static async Task<int> IntArrayMinAsync(int[] data, int start)
        {
            int minPos = start;
            for (int pos = start + 1; pos < data.Length-start; pos++)
                if (data[pos] < data[minPos])
                    minPos = pos;
            return minPos;
        }
        public static async Task<int> IntArrayMaxAsync(int[] data, int start)
        {
            int minPos = start;
            for (int pos = start; pos >data.Length-start-2; pos--)
                if (data[pos] > data[minPos])
                    minPos = pos;
            return minPos;
        }
        public static int IntArrayMin(int[] data, int start)
        {
            int minPos = start;
            for (int pos = start + 1; pos < data.Length; pos++)
                if (data[pos] < data[minPos])
                    minPos = pos;
            return minPos;
        }

        public static void IntArraySelectionSort(int[] data)
        {
            int i;
            int N = data.Length;

            for (i = 0; i < N - 1; i++)
            {
                int k = IntArrayMin(data, i);
                if (i != k)
                    exchange(data, i, k);
            }
        }
        public static void exchange(int[] data, int m, int n)
        {
            int temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
    }
}
