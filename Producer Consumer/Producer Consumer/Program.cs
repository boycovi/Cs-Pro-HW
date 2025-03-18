using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static Queue<int> buffer = new Queue<int>();
    static bool isProduceComplete = false;
    static object lockObject = new object();
    static void Main(string[] args)
    {
        Thread producerThread = new Thread(Producer);
        Thread consumerThread = new Thread(Consumer);

        producerThread.Start();
        consumerThread.Start();

        producerThread.Join();
        consumerThread.Join();

        Console.WriteLine("DONE DONE DONE E.");
    }
    static void Producer()
    {
        for (int i = 0; i < 10; i++)
        {
            int data = new Random().Next(100);

            lock (lockObject)
            {
                buffer.Enqueue(data);
                Console.WriteLine($"Generated {data}");
                Monitor.Pulse(lockObject);
            }
            Thread.Sleep(1000);
        }
        lock (lockObject)
        {
            isProduceComplete=true;
            Monitor.Pulse(lockObject);
        }
    }
    static void Consumer()
    {
        while (true)
        {
            lock (lockObject)
            {
                while (buffer.Count == 0 && !isProduceComplete)
                {
                    Monitor.Wait(lockObject);
                }
                if (buffer.Count == 0 && isProduceComplete)
                {
                    break;
                }
                int data = buffer.Dequeue();
                Console.WriteLine("CONSUMER ATE THE DATA");
            }
        }
    }
}