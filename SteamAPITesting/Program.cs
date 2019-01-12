using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Process.Start(_url);
                Console.WriteLine(_url);
                _downloadNews = web.DownloadString(_url);
                var result = JsonConvert.DeserializeObject<GetPlayerStats.RootObject>(_downloadNews);

                Console.WriteLine(result.playerstats.stats[0].name);

                foreach (var t in result.playerstats.stats)
                {
                    var output =
                        $"{t.name}: {t.value}";
                    Console.WriteLine(output);
                }

            }
        }

        public static void PullSteamUserInformation(string _steamID)
        {
            using (var web = new WebClient())
            {
                var _url = string.Format(
                    $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_steamKey}&steamids={_steamID}");
                Process.Start(_url);
                _downloadNews = web.DownloadString(_url);
                var result = JsonConvert.DeserializeObject<GetSteamUserInformation.RootObject>(_downloadNews);

            }
        }
        static void Main(string[] args)
        {
            string appID = "252490";
            Console.WriteLine("Please enter the 64 bit steam ID of the Rust player you'd like to look up!");
            string steamID = Console.ReadLine();
            PullRustPlayerStats(appID, steamID);
            //PullSteamUserInformation(steamID);
        }

        
    }
}
