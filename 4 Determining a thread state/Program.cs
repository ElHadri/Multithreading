using System;
using System.Threading;

namespace _4_Determining_a_thread_state
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("From main thread: Starting program...");
            Thread t1 = new Thread(PrintNumbersWithStatus);
            Thread t2 = new Thread(DoNothing);
            Console.WriteLine($"From main thread: t1 {t1.ThreadState}");


            t2.Start();

            t1.Start();

            for (int i = 1; i < 30; i++)
            {
                Console.WriteLine($"From main thread: t1 {t1.ThreadState}");
            }
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t1.Abort();
            Console.WriteLine("From main thread: A thread t1 has been aborted");
            Console.WriteLine($"From main thread: t1 {t1.ThreadState}");
            Console.WriteLine($"From main thread: t2 {t2.ThreadState}");

            Console.ReadKey();
        }

        static void DoNothing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        static void PrintNumbersWithStatus()
        {
            Console.WriteLine("From thread 1: Starting PrintNumbersWithStatus()...");
            Console.WriteLine($"From thread 1: t1 {Thread.CurrentThread.ThreadState}");

            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine($"From thread 1: {i}");
            }
        }
    }
}
