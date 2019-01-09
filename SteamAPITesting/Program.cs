using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SteamAPITesting
{
    class Program
    {
        private static string _downloadNews = null;
        private  static readonly string _steamKey = System.Environment.GetEnvironmentVariable("STEAM_KEY");
        private string returnData = null;
        public static void GetAppData()
        {
            string appID = null;
            Console.WriteLine("Please enter the Steam Game ID that you'd like the news for.");
            //appID = Console.ReadLine();
            appID = "252490";

            using (var web = new WebClient())
            {
                var _url =
                    string.Format(
                        $"http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid={appID}&count=3&maxlength=300&format=json");
                _downloadNews = web.DownloadString(_url);
                var result = JsonConvert.DeserializeObject<GetAppNews.RootObject>(_downloadNews);

            }
        }

        public static void PullRustPlayerStats(string _appID, string _steamID)
        {
            //appID = Console.ReadLine();
            using (var web = new WebClient())
            {
                var _url =
                    string.Format($"http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid={_appID}&key={_steamKey}&steamid={_steamID}");
                _downloadNews = web.DownloadString(_url);
                var result = JsonConvert.DeserializeObject<GetRustPlayerStats.RootObject>(_downloadNews);

                Console.WriteLine(result.playerstats.stats[0].name);

                for (int i = 0; i < result.playerstats.stats.Count; ++i)
                {
                    var output =
                        $"{result.playerstats.stats[i].name}: {result.playerstats.stats[i].value}";
                    Console.WriteLine(output);
                }

            }
        }

        public static void PullSubnauticaPlayerStats(string _appID, string _steamID)
        {
            using (var web = new WebClient())
            {
                var _url =
                    string.Format($"http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid={_appID}&key={_steamKey}&steamid={_steamID}");
                _downloadNews = web.DownloadString(_url);
                var result = JsonConvert.DeserializeObject<GetSubnauticaPlayerStats.RootObject>(_downloadNews);

               
                for (int i = 0; i < result.playerstats.achievements.Count; ++i)
                {
                    var output =
                        $"{result.playerstats.achievements[i].name}: {result.playerstats.achievements[i].achieved}";
                    Console.WriteLine(output);
                }

            }
        }
        static void Main(string[] args)
        {
            string appID = "252490";
            Console.WriteLine("Please enter the 64 bit steam ID of the Rust player you'd like to look up!");
            string steamID = Console.ReadLine();
            PullRustPlayerStats(appID, steamID);
        }
    }
}
