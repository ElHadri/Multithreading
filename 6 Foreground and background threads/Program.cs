using System;
using System.Threading;

namespace _6_Foreground_and_background_threads
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleForeground = new ThreadSample(10); // a thread that we create explicitly is a foreground thread
            var threadOne = new Thread(sampleForeground.CountNumbers)
            {
                Name = "ForegroundThread"
            };

            var sampleBackground = new ThreadSample(20); // a thread that we create explicitly is a foreground thread
            var threadTwo = new Thread(sampleBackground.CountNumbers)
            {
                Name = "BackgroundThread",
                IsBackground = true
            };

            threadOne.Start();
            threadTwo.Start();

        }

        class ThreadSample
        {
            private readonly int _iterations;
            public ThreadSample(int iterations)
            {
                _iterations = iterations;
            }
            public void CountNumbers()
            {
                for (int i = 0; i < _iterations; i++)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    Console.WriteLine($"{Thread.CurrentThread.Name} prints {i}");
                }
            }
        }
    }
}
