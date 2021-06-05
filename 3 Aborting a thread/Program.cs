using System;
using System.Threading;

namespace _3_Aborting_a_thread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting program...");

            Thread t1 = new Thread(PrintNumbersWithDelay);
            t1.Start();

            Thread.Sleep(TimeSpan.FromSeconds(6));
            t1.Abort();
            Console.WriteLine("A thread has been aborted");

            Thread t2 = new Thread(PrintNumbers);
            t2.Start();
            PrintNumbers();

            Console.ReadKey();
        }

        static void PrintNumbers()
        {
            Console.WriteLine("Starting PrintNumbers()...");
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void PrintNumbersWithDelay()
        {
            Console.WriteLine("Starting PrintNumbersWithDelay()...");
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }
    }
}
