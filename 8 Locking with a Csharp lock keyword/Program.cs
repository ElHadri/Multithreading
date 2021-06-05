using System;
using System.Threading;

namespace _8_Locking_with_a_Csharp_lock_keyword
{
    // "Race condition" multithreaded error
    // ensure that when one thread uses some resource, another does not simultaneously use it.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Incorrect counter");
            var c = new Counter();
            var t1 = new Thread(() => TestCounter(c));
            var t2 = new Thread(() => TestCounter(c));
            var t3 = new Thread(() => TestCounter(c));
            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine($"Total count: {c.Count}");
            Console.WriteLine("--------------------------");
            Console.WriteLine("Correct counter");
            var c1 = new CounterWithLock();
            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine($"Total count: {c1.Count}");

            Console.ReadKey();
        }

        static void TestCounter(CounterBase c)
        {
            for (int i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }

        class Counter : CounterBase
        {
            public int Count { get; private set; }
            public override void Increment()
            {
                Count++; // autoincrementing a variable is not thread-safe (read-increment-write)
            }
            public override void Decrement()
            {
                Count--; // autodecrementing a variable is not thread-safe (read-decrement-write)
            }
        }

        // thread-safe class
        class CounterWithLock : CounterBase
        {
            private readonly object _syncRoot = new Object();
            public int Count { get; private set; }
            public override void Increment()
            {
                lock (_syncRoot)
                {
                    Count++;
                }
            }
            public override void Decrement()
            {
                lock (_syncRoot)
                {
                    Count--;
                }
            }
        }

        abstract class CounterBase
        {
            public abstract void Increment();
            public abstract void Decrement();
        }
    }
}
