using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NASA_Parser
{
    internal class DocumentReader
    {
        private string _path;

        public DocumentReader(string path)
        {
            _path = path;
        }
        public async Task<IEnumerable<string>> ReadDocAsync(CancellationToken cancellationToken)
        {
            var lines = new ConcurrentBag<string>();

            using (FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
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
            return lines;
        }
    }
}