﻿using System;
using System.Threading;
using ClienteTesteCarga.ServiceReference1;

namespace ClienteTesteCarga
{

    class Program
    {
        private const int numThreads = 100;
        private static CountdownEvent countdown;
        private static ManualResetEvent mre = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            string s = null;
            Console.WriteLine("Press enter to start test.");
            while ((s = Console.ReadLine()) == string.Empty)
            {
                RunTest();
                Console.WriteLine("Allow a few seconds for threads to die.");
                Console.WriteLine("Press enter to run again.");
                Console.WriteLine();
            }
        }

        static void RunTest()
        {
            mre.Reset();
            Console.WriteLine("Starting {0} threads.", numThreads);
            countdown = new CountdownEvent(numThreads);
            for (int i = 0; i < numThreads; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }
            // Wait for all threads to signal.
            countdown.Wait();
            Console.Write("Press enter to release threads.");
            Console.ReadLine();
            Console.WriteLine("Releasing threads.");
            DateTime startTime = DateTime.Now;
            countdown = new CountdownEvent(numThreads);
            // Release all the threads.
            mre.Set();
            // Wait for all the threads to finish.
            countdown.Wait();
            TimeSpan ts = DateTime.Now - startTime;
            Console.WriteLine("Total time to run threads: {0}.{1:0}s.",
                ts.Seconds, ts.Milliseconds / 100);
        }

        private static void ThreadProc()
        {
            Service1Client client = new Service1Client();
            client.Open();
            // Signal that this thread is ready.
            countdown.Signal();
            // Wait for all the other threads before making service call.
            mre.WaitOne();
            client.GetData(1337);
            // Signal that this thread is finished.
            countdown.Signal();
            client.Close();
        }
    }
}
