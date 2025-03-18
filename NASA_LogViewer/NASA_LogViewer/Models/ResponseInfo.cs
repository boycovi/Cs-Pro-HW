using MaxMind.GeoIP2;
using System.Collections.Concurrent;
using System.Globalization;
using System.Net;
using System.Text;

namespace NASA_LogViewer.Models
{
    public class ResponseInfo
    {
        public string Ip { get; set; }
        public DateTime Date { get; set; }
        public string MethodRequest { get; set; }
        public string Uri { get; set; }
        public string Protocol { get; set; }
        public int StatusCode { get; set; }
        public int SizeBytes { get; set; }
        public string CountryCode { get; set; }

        private static readonly ConcurrentDictionary<string, string> CountryCodeCache = new ConcurrentDictionary<string, string>();
        public async Task GetCountryCodeAsync(DatabaseReader dbReader)
        {
            if (CountryCodeCache.TryGetValue(Ip, out var cachedCountryCode))
            {
                CountryCode = cachedCountryCode;
                return;
            }

            if (IPAddress.TryParse(Ip, out _))
            {
                CountryCode = GetCountryCodeFromIP(dbReader, Ip);
            }
            else
            {
                try
                {
                    IPAddress[] ipAddresses = await Dns.GetHostAddressesAsync(Ip);
                    if (ipAddresses.Length > 0)
                    {
                        string ipAddress = ipAddresses[0].ToString();
                        CountryCode = GetCountryCodeFromIP(dbReader, ipAddress);
                    }
                    else
                    {
                        CountryCode = "Unknown";
                    }
                }
                catch (Exception)
                {
                    CountryCode = "Unknown";
                }
            }

            CountryCodeCache[Ip] = CountryCode;
        }
        private string GetCountryCodeFromIP(DatabaseReader dbReader, string ip)
        {
            try
            {
                var countryResponse = dbReader.Country(ip);
                return countryResponse.Country.IsoCode;
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }
        public ResponseInfo(string line)
        {
            var splitLine = line.Split(' ');

            if (splitLine.Length < 10)
            {
                throw new ArgumentException("Line does not contain enough parts to be valid");
            }

            Ip = splitLine[0];

            var builder = new StringBuilder();
            builder.Append(splitLine[3]);
            builder.Append(" ");
            builder.Append(splitLine[4]);

            try
            {
                builder.Remove(0, 1);
                builder.Remove(builder.Length - 1, 1);

                Date = DateTime.ParseExact(builder.ToString(), "dd/MMM/yyyy:HH:mm:ss zzzz", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Date format in line is invalid");
            }

            MethodRequest = splitLine[5].Trim('"');
            Uri = splitLine[6];
            Protocol = splitLine[7].Trim('"');
            StatusCode = int.Parse(splitLine[8]);

            if (!int.TryParse(splitLine[9], out int sizeBytes))
            {
                sizeBytes = 0;
            }
            SizeBytes = sizeBytes;
        }
    }
}
