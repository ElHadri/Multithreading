using System;
using System.Threading;

namespace p10_Handling_exceptions
{
    // Handle exceptions in other threads.

    // It is very important to always place a try/catch block inside the thread because it is not possible
    // to catch an exception outside a thread's code.
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Thread(FaultyThread);
            t.Start();
            t.Join();

            try
            {
                t = new Thread(BadFaultyThread);
                t.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("We won't get here!");
            }
        }

        static void FaultyThread()
        {
            try
            {
                Console.WriteLine("Starting a faulty thread...");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                throw new Exception("Boom!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception handled: {ex.Message}");
            }
        }

        static void BadFaultyThread()
        {
            Console.WriteLine("Starting a faulty thread...");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            throw new Exception("Boom!");
        }
    }
}
