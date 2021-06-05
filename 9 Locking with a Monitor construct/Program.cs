using System;
using System.Threading;

namespace _9_Locking_with_a_Monitor_construct
{
    // "Deadlock" multithreaded error :  cause a program to stop working
    class Program
    {
        static void Main(string[] args)
        {
            object lock1 = new object();
            object lock2 = new object();

            new Thread(() => LockTooMuch(lock1, lock2, "1")).Start(); //---------------------------------------------thread 1
            // Thread.Sleep(TimeSpan.FromSeconds(2)); ---> juste pour pouvoir alterer le resultat

            lock (lock2)

            {
                Console.WriteLine("lock_2 locked [main thread]");
                Thread.Sleep(1000);
                Console.WriteLine("Monitor.TryEnter allows not to get stuck, returning false after a specified timeout is elapsed [main thread]");
                if (Monitor.TryEnter(lock1, TimeSpan.FromSeconds(5)))
                {
                    Console.WriteLine("lock_1 locked [main thread]");
                    Console.WriteLine("Acquired a protected resource succesfully [main thread]");
                    Console.WriteLine("lock_1 unlocked [main thread]!!!!");
                }
                else
                {
                    Console.WriteLine("Timeout acquiring a resource!");
                }
            }
            Console.WriteLine("lock_2 unlocked [main thread]");

            new Thread(() => LockTooMuch(lock1, lock2, "2")).Start(); //--------------------------------------------- thread 2

            Console.WriteLine("----------------------------------[main thread]");

            lock (lock2)
            {
                Console.WriteLine("lock_2 locked [main thread]*");
                Console.WriteLine("This will be a deadlock! [main thread]*");
                Thread.Sleep(1000);
                lock (lock1)
                {
                    Console.WriteLine("lock_1 locked [main thread]*");
                    Console.WriteLine("Acquired a protected resource succesfully [main thread]*");
                }
                Console.WriteLine("lock_1 unlocked [main thread]*");
            }
            Console.WriteLine("lock_2 unlocked [main thread]*");
        }


        static void LockTooMuch(object lock1, object lock2, object threadNumber)
        {
            lock (lock1)
            {
                Console.WriteLine($"lock_1 locked [worker thread {threadNumber as string}]");
                Thread.Sleep(1000);
                lock (lock2)
                {
                    Console.WriteLine($"lock_2 locked [worker thread {threadNumber as string}]");
                };
                Console.WriteLine($"lock_2 unlocked [worker thread {threadNumber as string}]");
            }
            Console.WriteLine($"lock_1 unlocked [worker thread {threadNumber as string}]");
        }
    }
}
