using System;
using System.Diagnostics;
using System.Threading;


namespace _5_Thread_priority
{
    // Setting a thread priority determines how much CPU time a thread will be given.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"From main thread: Current thread priority: {Thread.CurrentThread.Priority}");
            Console.WriteLine("From main thread: Running on all cores available");
            RunThreads();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("From main thread: Running on a single core");

            // instructing the operating system to run all our threads on a single CPU core(number 1)
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            RunThreads();

            Console.ReadKey();
        }

        static void RunThreads()
        {
            var sample = new ThreadSample();
            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";
            var threadTwo = new Thread(sample.CountNumbers);
            threadTwo.Name = "ThreadTwo";
            threadOne.Priority = ThreadPriority.Highest;
            threadTwo.Priority = ThreadPriority.Lowest;
            threadOne.Start();
            threadTwo.Start();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();
        }

        class ThreadSample
        {
            private bool _isStopped = false;

            public void Stop()
            {
                _isStopped = true;
            }

            public void CountNumbers()
            {
                long counter = 0;
                while (!_isStopped)
                {
                    counter++;
                }
                Console.WriteLine($"{Thread.CurrentThread.Name} with " +
                $"{Thread.CurrentThread.Priority,11} priority " +
                $"has a count = {counter,13:N0}");
            }
        }
    }
}
