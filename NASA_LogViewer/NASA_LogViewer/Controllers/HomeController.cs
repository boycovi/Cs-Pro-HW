using NASA_LogViewer.Models;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Reflection;

namespace NASA_LogViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseReader _dbReader;
        private static ConcurrentBag<ResponseInfo> _responses;

        public HomeController(DatabaseReader dbReader)
        {
            _dbReader = dbReader;
            if (_responses is null)
            {
                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "access_log_Jul95");
                _responses = new ConcurrentBag<ResponseInfo>(ReadLogFile(path).Result);
            }
        }
        private async Task<IEnumerable<ResponseInfo>> ReadLogFile(string path)
        {
            var lines = new List<string>();
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) is not null)
                    {
                        lines.Add(line);
                    }
                }
            }

            var responses = new ConcurrentBag<ResponseInfo>();
            var tasks = new List<Task>();

            foreach (var line in lines)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var response = new ResponseInfo(line);
                        await response.GetCountryCodeAsync(_dbReader);
                        responses.Add(response);
                    }
                    catch
                    {
                        //invalid lines
                    }
                }));
            }

            await Task.WhenAll(tasks);
            return responses;
        }
        public IActionResult Index()
        {
            return View(_responses);
        }
    }
}
