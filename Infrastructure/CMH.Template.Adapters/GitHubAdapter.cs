using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CMH.MobileHomeTracker.Domain.Adapters;
using CMH.MobileHomeTracker.Domain.Models;
using Newtonsoft.Json;

namespace CMH.MobileHomeTracker.Adapters
{
    public class GitHubAdapter : IGitHubAdapter
    {
        public async Task<GpsData> GetLocationDataForId(Guid id)
        {
            // Move to config
            var baseUrl = "https://api.github.com";
            var user = "johnflynn-clayton";
            var repo = "tech-debtors-data";
            var file = $"{id}.csv";
            var url = $"{baseUrl}/repos/{user}/{repo}/contents/{file}";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("User-Agent", "WhyDoYouWantToKnow");

            var response = await client.SendAsync(request);
            var jsonData = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<GitHubResponse>(jsonData);

            var csv = Encoding.ASCII.GetString(Convert.FromBase64String(data.Content));
            
            return GetGpsData(csv);
        }

        private GpsData GetGpsData(string csv)
        {
            var parts = csv.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var ret = new GpsData
            {
                Latitude = float.Parse(parts[0]),
                Longitude = float.Parse(parts[2]),
                Date = DateTime.ParseExact($"{parts[4]} {(int)Convert.ToDouble(parts[5])}",
                        "ddMMyy HHmmss",
                        CultureInfo.InvariantCulture)
                    .ToLocalTime(),
                Elevation = Convert.ToDouble(parts[6])
            };

            // move decimals two places
            ret.Latitude /= 100;
            ret.Longitude /= 100;

            if (string.Equals(parts[1], "S", StringComparison.InvariantCultureIgnoreCase))
            {
                // make negative
                ret.Latitude = 0 - ret.Latitude;
            }

            if(string.Equals(parts[3], "W", StringComparison.InvariantCultureIgnoreCase))
            {
                // make negative
                ret.Longitude = 0 - ret.Longitude;
            }

            return ret;
        }
    }
}
