using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using RestSharp;

namespace LootBoyFarm
{
    public class LootBoyHandler
    {

        RestClient _client = new RestClient("https://api.lootboy.de/");

        static Random _rnd = new Random();

        private string _lootboyToken;

        private string _lootBoyUser;

        public void Login()
        {
            var steamHandler = new SteamHandler();
            _lootboyToken =
                "eyJhbGciOiJSUzI1NiJ9.eyJ1c2VyIjoiNWNiZGQ5NjA2NzU1ZWQwMDI5ZDVhMzI5Iiwicm9sZXMiOltdfQ.GuiEOUc4bCnmtOToirVkuSxtmC9H-BLZnkUWVsQ3ed5U0UYCp7m9J1kBXsRkgQI7Qv2scritE1IxPoVF2llG6DR26uBgqZkrOkqgjUkNjN-4gPKk-72GeGSSsNZYT2zWEHvUG6vdopIrbQBiHenWUf3vcFDP-hlyk84IbTtizCL68noRsc28ylz1xWXdwsZWs2Jn6QwKImbcAu9U4I2gqWIx7CAasMIjWvksZqZvpnRKDL_n8nk-z4Umakq7dXKJdsnVTATnQ9Vo4PM0EiUABu2tdBqPClRTWcf1CRme0r3Jd72sFR2eE0pBHGOgGyYV1-WTZTpK-5mkax3WOcmlH09Q-xMcZG40Fh6buPMbIyeF4uCRGcPjs-4QQDjFI6q9OvdVdJC_dIgFDvCyzNimMOwfiiEoSOTo8HAmCL7PywJ5apybD6iskZPUgb4BZscyl6Wy3P7Lo8o7sdg48N-c1xftHE7l6iyGXzvgKvyWfwFKb_SZvBMqY0Sdm64NPwU1vlA4cBwB3poNEhUKBvkhz2iHzVwkIo8_-dpVjVJFW5OivO0RrpSQlnwiMtaP_qFQtkN01lstb3TBH5j3zedEBHyRVQ833-FcTqFgRWPjIjvwTihsT3mEJlPsgxNY7vbwRukxYF0cQN1YtU8dhbzLsbgte_lqQ3dZIJYInEyCBkg";
            /*steamHandler.GetNextTocken();*/
            _lootBoyUser = GetUserName(_lootboyToken);
            ;
        }


        public void Farm()
        {
            var deviceId = Guid.NewGuid().ToString();
            SetDeviceId(deviceId);
            SetBirddy();
            var adjoe = new AdjoeHandler(_lootBoyUser, deviceId);
            adjoe.Initialize();
            Thread.Sleep(1000);
            adjoe.InitFinishedEvent();
            Thread.Sleep(1000);
            adjoe.OfferwallShowRequestEvent();
            Thread.Sleep(1000);
            adjoe.AgbShownEvent();
            Thread.Sleep(1000);
            adjoe.AgbAcceptedEvent();
            Thread.Sleep(1000);
            adjoe.AppList();
            Thread.Sleep(1000);
            adjoe.SendDeviceAppsEvent();
            Thread.Sleep(1000);
            adjoe.SendUserAppsUsageHistory();
            Thread.Sleep(1000);
            adjoe.UsagePermissionAcceptedEvent();
            Thread.Sleep(1000);
            adjoe.CampaingDistribution();
            //adjoe.CompaingDistibutionAuto();
        }

        public void GetLoot()
        {
            
            var gems = GetGemCount();
            Console.WriteLine(gems);
            var packetCount = gems / 100;
            for (int i = 0; i < packetCount; i++)
            {
                var lootId = Purchases("5c33584337e585001c459491");
                Open(lootId);
            }

            Cards();
        }

        public int GetGemCount()
        {
            var url = $"/v2/users/{_lootBoyUser}";
            var request = new RestRequest(url, Method.GET);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);
            request.AddParameter("accept", "application/json", ParameterType.HttpHeader);


            var response = _client.Execute<LootBoyUserInfo>(request);
            if (response.IsSuccessful)
            {
                var userInfo = response.Data;
                return userInfo.lootgemBalance;
            }
            return 0;
        }

        

        public void Delete()
        {
            var request = new RestRequest("/v2/users/self", Method.DELETE);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);
            var responce = _client.Execute(request);
            if (responce.IsSuccessful)
            {
                ;
            }

        }

        private string Purchases(string lootId)
        {
            var request = new RestRequest($"/v1/loots/{lootId}/purchases", Method.POST);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);
            request.AddJsonBody(new
            {
                application = "android",
                vendor = "lootgem",
                vendorData = new { },
                count = 1
            });

            var responce = _client.Execute<LootResponce>(request);
            if (responce.IsSuccessful)
            {
                return responce.Data._id;
            }
            return null;
        }


        private void Open(string lootId)
        {
            var request = new RestRequest($"/v4/purchases/{lootId}/open", Method.PUT);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);


            var responce = _client.Execute<OpenResponce>(request);
            if (responce.IsSuccessful)
            {

            }
            
        }

        private void Reward(string lootId)
        {
            var request = new RestRequest($"/v1/cards/{lootId}/reward", Method.GET);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);


            var responce = _client.Execute<RewarResponce>(request);
            if (responce.IsSuccessful)
            {
                Write((responce.Data.code ?? responce.Data.uri)+"\n");
            }

        }

        private void Cards()
        {
            var request = new RestRequest("/v4/cards", Method.GET);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);


            var responce = _client.Execute<List<CardResponce>>(request);
            if (responce.IsSuccessful)
            {
                foreach (var card in responce.Data)
                {
                    Write(card.cardTemplate.name+"("+string.Join(",", card.cardTemplate.types.Select(t=>t.name))+"): ");
                    Reward(card._id);
                }
            }
        }

        private void SetDeviceId(string deviceId)
        {
            var url = $"/v2/users/{_lootBoyUser}";
            var request = new RestRequest(url, Method.PUT);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);
            request.AddParameter("accept", "application/json", ParameterType.HttpHeader);
            request.AddJsonBody(new {  deviceId });

            var response = _client.Execute<LootBoyUserInfo>(request);
            if (response.IsSuccessful)
            {
                var userInfo = response.Data;
            }
        }


        public void SetBirddy()
        {
            var url = $"/v2/users/{_lootBoyUser}";
            var request = new RestRequest(url, Method.PUT);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);
            request.AddParameter("accept", "application/json", ParameterType.HttpHeader);
            request.AddJsonBody(new
            {
                username = RandomString(8),
                birthday = DateTime.Now.AddYears(-_rnd.Next(20,40)).AddDays(-_rnd.Next(365)).ToString("yyyy-MM-dd"),
                gender = "female",
                country = "USA",
                language = "en-EN"
            });

            var response = _client.Execute<LootBoyUserInfo>(request);
            if (response.IsSuccessful)
            {
                var userInfo = response.Data;
            }
        }

        private static string RandomString(int length)
        {
            const string chars = "abcdefjklmnopiiaasttzxyy";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_rnd.Next(s.Length)]).ToArray());
        }

        private string GetUserName(string token)
        {
            var parts = token.Split(".");
            var decode = Encoding.UTF8.GetString(Convert.FromBase64String(parts[1] + "=="));

            return decode.Substring(9, 24);
        }

        private void Write(string line)
        {
            using (StreamWriter sw = File.AppendText("D:/loot.txt"))
            {
                sw.Write(line);
            }
        }



    }
}
