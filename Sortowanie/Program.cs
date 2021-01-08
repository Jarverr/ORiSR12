using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortowanie
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Bubble();
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
            Console.Write("Sekwencyjnie:");
           // quick(toSort);
            Quick_Sort(toSort, 0, toSort.Length - 1);

            Console.WriteLine();
            Console.WriteLine("Sorted array : ");

            foreach (var item in toSort)
            {
                Console.Write(" " + item);
            }
            Console.WriteLine("\nTime: " + sw.Elapsed);
            toSort = sorted;
            sw.Reset();
            sw.Start();



            Console.Write("\nRównoległe:");

            Quick_Sort2(toSort, 0, toSort.Length - 1);

            Console.WriteLine();
            Console.WriteLine("Sorted array : ");

            foreach (var item in toSort)
            {
                Console.Write(" " + item);
            }
            Console.WriteLine("\nTime: "+sw.Elapsed);
            sw.Stop();
            Console.ReadLine();
        }
        private static void Quick_Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right);
                }
            }

        }
        private static async Task  Quick_Sort2(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                Task[] toDo = new Task[2];
                if (pivot > 1)
                {
                    toDo[0]=Quick_Sort2(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    toDo[1]=Quick_Sort2(arr, pivot + 1, right);
                }
                await Task.WhenAll(toDo);
            }

        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }

        private static void quick(int[] toSort)
        {
            int pivolt = toSort[toSort.Length / 2];
            for (int i = 0,j=toSort.Length-1; i < toSort.Length/2; i++,j--)
            {
                //if (toSort[i]<pivolt && toSort[j])
                {

                }
            }
        }

        private static void Bubble()
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

            bool amIDoingAnything = true;
            int temp;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Sekwencyjnie:");

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
            //foreach (var item in toSort)
            //{
            //    Console.Write($"{item} ");
            //}
            Console.WriteLine();
            Console.WriteLine($"Time: {sw.Elapsed}");
            for (int i = 0; i < toSort.Length; i++)
            {
                toSort[i] = sorted[i];
            }
            sw.Reset();
            sw.Start();




            amIDoingAnything = true;
            Console.WriteLine("Równolegle");
            while (amIDoingAnything)
            {
                amIDoingAnything = false;
                Parallel.For(0, 2, i => {
                    if (i == 0)
                    {
                        for (int j = 0; j < toSort.Length / 2; j++)
                        {
                            if (toSort[j] > toSort[j + 1])
                            {
                                temp = toSort[j + 1];
                                toSort[j + 1] = toSort[j];
                                toSort[j] = temp;
                                amIDoingAnything = true;
                            }
                        }
                    }
                    else
                    {
                        for (int j = toSort.Length / 2; j < toSort.Length - 1; j++)
                        {
                            if (toSort[j] > toSort[j + 1])
                            {
                                temp = toSort[j + 1];
                                toSort[j + 1] = toSort[j];
                                toSort[j] = temp;
                                amIDoingAnything = true;
                            }
                        }
                    }

                });
                if (toSort[toSort.Length / 2 - 1] > toSort[toSort.Length / 2])
                {
                    temp = toSort[toSort.Length / 2];
                    toSort[toSort.Length / 2] = toSort[toSort.Length / 2 - 1];
                    toSort[toSort.Length / 2 - 1] = temp;
                    amIDoingAnything = true;
                }
            }
            //foreach (var item in toSort)
            //{
            //    Console.Write($"{item} ");
            //}
            Console.WriteLine();
            Console.WriteLine($"Time: {sw.Elapsed}");
            sw.Reset();
            sw.Start();


            sw.Stop();
            Console.ReadLine(); 
        }
    }
}
