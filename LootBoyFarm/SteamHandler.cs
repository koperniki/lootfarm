using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace LootBoyFarm
{
    public class SteamHandler
    {
        RestClient _client = new RestClient("https://steamcommunity.com/openid/login")
        {
            UserAgent = "okhttp/LootboyApp; lootboy-lib/0.1.0",
            FollowRedirects = true
        };
        

        public string GetNextTocken()
        {

            var openIdRequest = new RestRequest(
                "https://steamcommunity.com/openid/login?openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.sreg=http%3A%2F%2Fopenid.net%2Fextensions%2Fsreg%2F1.1&openid.sreg.optional=nickname%2Cemail%2Cfullname%2Cdob%2Cgender%2Cpostcode%2Ccountry%2Clanguage%2Ctimezone&openid.ns.ax=http%3A%2F%2Fopenid.net%2Fsrv%2Fax%2F1.0&openid.ax.mode=fetch_request&openid.ax.type.fullname=http%3A%2F%2Faxschema.org%2FnamePerson&openid.ax.type.firstname=http%3A%2F%2Faxschema.org%2FnamePerson%2Ffirst&openid.ax.type.lastname=http%3A%2F%2Faxschema.org%2FnamePerson%2Flast&openid.ax.type.email=http%3A%2F%2Faxschema.org%2Fcontact%2Femail&openid.ax.required=fullname%2Cfirstname%2Clastname%2Cemail&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.return_to=https%3A%2F%2Fapi.lootboy.de%2Fv1%2Fauth%2FsteamLogin%2Fcallback&openid.realm=https%3A%2F%2Fapi.lootboy.de%2F",
                Method.GET);
            AddCookies(openIdRequest);

            var openIdResponce = _client.Execute(openIdRequest);
            ;

            var request = new RestRequest(Method.POST);

            AddCookies(request, openIdResponce);

            var responce = _client.Execute(request);
            if (responce.IsSuccessful)
            {
               return responce.ResponseUri.AbsoluteUri.Replace("https://www.lootboy.de/steamLogin?token=", "");
            }

            return null;
        }



        private void AddCookies(RestRequest request)
        {
            request.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddCookie("sessionid", "55948cc7c10b298edf2e2b91");
            request.AddCookie("steamCountry", "RNO%7Cef64df5a658545959f9dc69ebbbd6819");
            request.AddCookie("strResponsiveViewPrefs", "touch");
            request.AddCookie("steamLoginSecure", "76561198933627299%7C%7C6F6454E891BE86289A118DC94BAA2F3933118E28");
            request.AddCookie("browserid", "1147764792152268844");
            request.AddCookie("_ga", "GA1.2.50837988.1555673488");
            request.AddCookie("_gid", "GA1.2.1003700993.1555673488");

        }

        private void AddCookies(RestRequest request, IRestResponse response)
        {
            request.AlwaysMultipartFormData = true;
            var nonce = "";
            foreach (var cookie in response.Cookies)
            {
                request.AddCookie(cookie.Name, cookie.Value);
                if (cookie.Name == "sessionidSecureOpenIDNonce")
                {
                    nonce = cookie.Value;
                }
            }

            var token =
                "eyJvcGVuaWQubW9kZSI6ImNoZWNraWRfc2V0dXAiLCJvcGVuaWQubnMiOiJodHRwOlwvXC9zcGVjcy5vcGVuaWQubmV0XC9hdXRoXC8yLjAiLCJvcGVuaWQubnNfc3JlZyI6Imh0dHA6XC9cL29wZW5pZC5uZXRcL2V4dGVuc2lvbnNcL3NyZWdcLzEuMSIsIm9wZW5pZC5zcmVnX29wdGlvbmFsIjoibmlja25hbWUsZW1haWwsZnVsbG5hbWUsZG9iLGdlbmRlcixwb3N0Y29kZSxjb3VudHJ5LGxhbmd1YWdlLHRpbWV6b25lIiwib3BlbmlkLm5zLmF4IjoiaHR0cDpcL1wvb3BlbmlkLm5ldFwvc3J2XC9heFwvMS4wIiwib3BlbmlkLmF4Lm1vZGUiOiJmZXRjaF9yZXF1ZXN0Iiwib3BlbmlkLmF4LnR5cGUuZnVsbG5hbWUiOiJodHRwOlwvXC9heHNjaGVtYS5vcmdcL25hbWVQZXJzb24iLCJvcGVuaWQuYXgudHlwZS5maXJzdG5hbWUiOiJodHRwOlwvXC9heHNjaGVtYS5vcmdcL25hbWVQZXJzb25cL2ZpcnN0Iiwib3BlbmlkLmF4LnR5cGUubGFzdG5hbWUiOiJodHRwOlwvXC9heHNjaGVtYS5vcmdcL25hbWVQZXJzb25cL2xhc3QiLCJvcGVuaWQuYXgudHlwZS5lbWFpbCI6Imh0dHA6XC9cL2F4c2NoZW1hLm9yZ1wvY29udGFjdFwvZW1haWwiLCJvcGVuaWQuYXgucmVxdWlyZWQiOiJmdWxsbmFtZSxmaXJzdG5hbWUsbGFzdG5hbWUsZW1haWwiLCJvcGVuaWQuaWRlbnRpdHkiOiJodHRwOlwvXC9zcGVjcy5vcGVuaWQubmV0XC9hdXRoXC8yLjBcL2lkZW50aWZpZXJfc2VsZWN0Iiwib3BlbmlkLmNsYWltZWRfaWQiOiJodHRwOlwvXC9zcGVjcy5vcGVuaWQubmV0XC9hdXRoXC8yLjBcL2lkZW50aWZpZXJfc2VsZWN0Iiwib3BlbmlkLnJldHVybl90byI6Imh0dHBzOlwvXC9hcGkubG9vdGJveS5kZVwvdjFcL2F1dGhcL3N0ZWFtTG9naW5cL2NhbGxiYWNrIiwib3BlbmlkLnJlYWxtIjoiaHR0cHM6XC9cL2FwaS5sb290Ym95LmRlXC8ifQ==";


            request.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);

            request.AddParameter("action", "steam_openid_login", ParameterType.GetOrPost);
            request.AddParameter("openid.mode", "checkid_setup", ParameterType.GetOrPost);
            request.AddParameter("openidparams", token, ParameterType.GetOrPost);
            request.AddParameter("nonce", nonce, ParameterType.GetOrPost);
        }


    }
}
