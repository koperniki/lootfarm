using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using LootBoyStandart;
using RestSharp;
using SteamKit2.GC.CSGO.Internal;

namespace LootBoyFarm
{
    public class LootBoyHandler
    {

        RestClient _client = new RestClient("https://api.lootboy.de/");

        static Random _rnd = new Random();

        private string _lootboyToken;

        private string _lootBoyUser;

        private string _file = "loot.txt";

        public  LootBoyHandler(string token)
        {

            _lootboyToken = token;
            _lootBoyUser = GetUserName(_lootboyToken);
            ;
        }


        public List<Loot> GetLootList()
        {
            var url = "/v3/loots/";
            var request = new RestRequest(url, Method.GET);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);
            request.AddParameter("accept", "application/json", ParameterType.HttpHeader);

            var response = _client.Execute<List<Loot>>(request);
            if (response.IsSuccessful)
            {
                var res = response.Data;
                return res;
            }

            return null;
        }

        public void GetLoot()
        {
            
            var gems = GetGemCount().Item1;
            Console.WriteLine(gems);
            var packetCount = gems / 100;
            for (int i = 0; i < packetCount; i++)
            {
                var lootId = Purchases("5c33584337e585001c459491");
                Open(lootId);
            }

            Cards("loot.txt");
        }

        public (int gem,int coin) GetGemCount()
        {
            var url = $"/v2/users/{_lootBoyUser}";
            var request = new RestRequest(url, Method.GET);
            request.AddParameter("authorization", $"Bearer {_lootboyToken}", ParameterType.HttpHeader);
            request.AddParameter("accept", "application/json", ParameterType.HttpHeader);


            var response = _client.Execute<LootBoyUserInfo>(request);
            if (response.IsSuccessful)
            {
                var userInfo = response.Data;
                return (userInfo.lootgemBalance, userInfo.lootcoinBalance);
            }
            return (0,0);
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

        public string Purchases(string lootId)
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

        public void Cards(string file)
        {
            _file = file;
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
            var parts = token.Split('.');
            var decode = Encoding.UTF8.GetString(Convert.FromBase64String(parts[1] + "=="));

            return decode.Substring(9, 24);
        }

        private void Write(string line)
        {
            using (StreamWriter sw = File.AppendText(_file))
            {
                sw.Write(line);
            }
        }



    }
}
