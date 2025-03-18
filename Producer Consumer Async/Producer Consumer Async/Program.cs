using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;

class Program
{
    static Queue<int> buffer = new Queue<int>();
    static object lockObject = new object();
    static bool isProduceComplete = false;

    static async Task Main()
    {
        Task producerTask = ProducerAsync();
        Task consumerTask = ConsumerAsync();

        await Task.WhenAll(producerTask, consumerTask);

        Console.WriteLine("DONE DONE DONE!!!!.");
    }

    static async Task ProducerAsync()
    {
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            int data = random.Next(100);
            lock (lockObject)
            {
                buffer.Enqueue(data);
                Console.WriteLine($"Producer generated: {data}");
                Monitor.Pulse(lockObject);
            }

            await Task.Delay(random.Next(1000));
        }

        lock (lockObject)
        {
            isProduceComplete = true;
            Monitor.Pulse(lockObject);
        }
    }

    static async Task ConsumerAsync()
    {
        while (true)
        {
            lock (lockObject)
            {
                while (buffer.Count == 0 && !isProduceComplete) Monitor.Wait(lockObject);
                if (buffer.Count == 0 && isProduceComplete) break;

                int data = buffer.Dequeue();
                Console.WriteLine($"Consumer ATE: {data}");
            }
        }
    }
}
