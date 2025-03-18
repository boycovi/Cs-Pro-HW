using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NASA_Parser
{
    internal class Program
    {
        public static DatabaseReader _dbReader;

        static async Task Main(string[] args)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "access_log_Jul95");
            var dbPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "GeoLite2-Country.mmdb");

            _dbReader = new DatabaseReader(dbPath);

            DocumentReader reader = new DocumentReader(path);

            var lines = await reader.ReadDocAsync(CancellationToken.None);

            ConcurrentBag<ResponseInfo> responses = new ConcurrentBag<ResponseInfo>();
            var tasks = new List<Task>();

            foreach (var line in lines)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var response = new ResponseInfo(line);
                        await response.GetCountryCodeAsync();
                        responses.Add(response);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing line: {ex.Message}");
                    }
                }));
            }

            await Task.WhenAll(tasks);

            foreach (var response in responses)
            {
                Console.WriteLine(response.ToString());
            }
        }
    }
}