using System;
using System.Threading;

namespace _2_Making_a_thread_wait
{
    // wait for some computation in another thread to complete to use its result later in the code.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();

            t.Join();
            Console.WriteLine("Thread completed");

            Console.ReadKey();
        }

        static void PrintNumbersWithDelay()
        {
            Console.WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }
    }
}
