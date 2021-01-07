using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    class Program
    {
        static void Main(string[] args)
        {

            //sync
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Sekwencyjna wersja:");

            int N = 10000000;
            double sum = 0.0;
            double step = 1.0 / N;
            for (var i = 0; i < N; i++)
            {
                double x = (i + 0.5) * step;
                sum += 4.0 / (1.0 + x * x);
            }
            Console.WriteLine(sum * step);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ");
            sw.Restart();
            sw.Start();
            //async
            Console.WriteLine("Równoległa wersja:");

            object _ = new object();
            sum = 0.0;
             step = 1.0 / N;
            Parallel.For( 0 , N, i=>
            {
                double x = (i + 0.5) * step;
                double y = 4.0 / (1.0 + x * x);
                lock (_)
                    {
                     sum += y;
                    }
             });
            Console.WriteLine(sum * step);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ");
            sw.Restart();
            sw.Start();
            //asyncV2
            Console.WriteLine("Równoległa wersja 2:");

            _ = new object();
            sum = 0.0;
            step = 1.0 / N;
            Parallel.For( 0 , N,()=>0.0,( i,state,local)=>
            {
                double x = (i + 0.5) * step;
                return local + 4.0 / (1.0 + x * x);
            }, local =>
            {
                lock (_)
                {
                    sum += local;
                }
            });
            Console.WriteLine(sum * step);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ");
            sw.Stop();
            Console.ReadKey();
        }
    }
}
