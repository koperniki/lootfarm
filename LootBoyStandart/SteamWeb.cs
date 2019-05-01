﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using SteamKit2;

namespace LootBoyStandart
{
    /// <summary>
    /// SteamWeb class to create an API endpoint to the Steam Web.
    /// </summary>
    public class SteamWeb
    {
        /// <summary>
        /// Base steam community domain.
        /// </summary>
        public const string SteamCommunityDomain = "steamcommunity.com";

        /// <summary>
        /// Token of steam. Generated after login.
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Session id of Steam after Login.
        /// </summary>
        public string SessionId { get; private set; }

        /// <summary>
        /// Token secure as string. It is generated after the Login.
        /// </summary>
        public string TokenSecure { get; private set; }

        /// <summary>
        /// The Accept-Language header when sending all HTTP requests. Default value is determined according to the constructor caller thread's culture.
        /// </summary>
        public string AcceptLanguageHeader { get { return acceptLanguageHeader; } set { acceptLanguageHeader = value; } }
        private string acceptLanguageHeader = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en" ? Thread.CurrentThread.CurrentCulture.ToString() + ",en;q=0.8" : Thread.CurrentThread.CurrentCulture.ToString() + "," + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + ";q=0.8,en;q=0.6";


        /// <summary>
        /// CookieContainer to save all cookies during the Login. 
        /// </summary>
        private CookieContainer _cookies = new CookieContainer();


        /// <summary>
        /// Custom wrapper for creating a HttpWebRequest, edited for Steam.
        /// </summary>
        /// <param name="url">Gets information about the URL of the current request.</param>
        /// <param name="method">Gets the HTTP data transfer method (such as GET, POST, or HEAD) used by the client.</param>
        /// <param name="data">A NameValueCollection including Headers added to the request.</param>
        /// <param name="ajax">A bool to define if the http request is an ajax request.</param>
        /// <param name="referer">Gets information about the URL of the client's previous request that linked to the current URL.</param>
        /// <param name="fetchError">Return response even if its status code is not 200</param>
        /// <returns>An instance of a HttpWebResponse object.</returns>
        public HttpWebResponse Request(string url, string method, NameValueCollection data = null, bool ajax = true, string referer = "", bool fetchError = false)
        {
            // Append the data to the URL for GET-requests.
            bool isGetMethod = (method.ToLower() == "get");
            string dataString = (data == null ? null : String.Join("&", Array.ConvertAll(data.AllKeys, key =>
                // ReSharper disable once UseStringInterpolation
                string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(data[key]))
            )));

            // Example working with C# 6
            // string dataString = (data == null ? null : String.Join("&", Array.ConvertAll(data.AllKeys, key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(data[key])}" )));

            // Append the dataString to the url if it is a GET request.
            if (isGetMethod && !string.IsNullOrEmpty(dataString))
            {
                url += (url.Contains("?") ? "&" : "?") + dataString;
            }

            // Setup the request.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json, text/javascript;q=0.9, */*;q=0.5";
            request.Headers[HttpRequestHeader.AcceptLanguage] = AcceptLanguageHeader;
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            // request.Host is set automatically.
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            request.Referer = string.IsNullOrEmpty(referer) ? "http://steamcommunity.com/trade/1" : referer;
            request.Timeout = 50000; // Timeout after 50 seconds.
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            // If the request is an ajax request we need to add various other Headers, defined below.
            if (ajax)
            {
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("X-Prototype-Version", "1.7");
            }

            // Cookies
            request.CookieContainer = _cookies;

            // If the request is a GET request return now the response. If not go on. Because then we need to apply data to the request.
            if (isGetMethod || string.IsNullOrEmpty(dataString))
            {
                return request.GetResponse() as HttpWebResponse;
            }

            // Write the data to the body for POST and other methods.
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);
            request.ContentLength = dataBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(dataBytes, 0, dataBytes.Length);
            }

            // Get the response and return it.
            try
            {
                return request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                //this is thrown if response code is not 200
                if (fetchError)
                {
                    var resp = ex.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        return resp;
                    }
                }
                throw;
            }
        }

        ///<summary>
        /// Authenticate using SteamKit2 and ISteamUserAuth. 
        /// This does the same as SteamWeb.DoLogin(), but without contacting the Steam Website.
        /// </summary>
        /// <remarks>Should this one doesnt work anymore, use <see cref="SteamWeb.DoLogin"/></remarks>
        /// <param name="myUniqueId">Id what you get to login.</param>
        /// <param name="client">An instance of a SteamClient.</param>
        /// <param name="myLoginKey">Login Key of your account.</param>
        /// <returns>A bool, which is true if the login was successful.</returns>
        public bool Authenticate(string myUniqueId, SteamClient client, string myLoginKey)
        {
            Token = TokenSecure = "";
            SessionId = Convert.ToBase64String(Encoding.UTF8.GetBytes(myUniqueId));
            _cookies = new CookieContainer();

            using (dynamic userAuth = WebAPI.GetInterface("ISteamUserAuth"))
            {
                // Generate an AES session key.
                var sessionKey = CryptoHelper.GenerateRandomBlock(32);

                // rsa encrypt it with the public key for the universe we're on
                byte[] cryptedSessionKey;
                using (RSACrypto rsa = new RSACrypto(KeyDictionary.GetPublicKey(client.Universe)))
                {
                    cryptedSessionKey = rsa.Encrypt(sessionKey);
                }

                byte[] loginKey = new byte[20];
                Array.Copy(Encoding.ASCII.GetBytes(myLoginKey), loginKey, myLoginKey.Length);

                // AES encrypt the loginkey with our session key.
                byte[] cryptedLoginKey = CryptoHelper.SymmetricEncrypt(loginKey, sessionKey);

                KeyValue authResult;

                // Get the Authentification Result.
                try
                {
                    authResult = userAuth.AuthenticateUser(
                        steamid: client.SteamID.ConvertToUInt64(),
                        sessionkey: HttpUtility.UrlEncode(cryptedSessionKey),
                        encrypted_loginkey: HttpUtility.UrlEncode(cryptedLoginKey),
                        method: "POST",
                        secure: true
                        );
                }
                catch (Exception)
                {
                    Token = TokenSecure = null;
                    return false;
                }

                Token = authResult["token"].AsString();
                TokenSecure = authResult["tokensecure"].AsString();

                // Adding cookies to the cookie container.
                _cookies.Add(new Cookie("sessionid", SessionId, string.Empty, SteamCommunityDomain));
                _cookies.Add(new Cookie("steamLogin", Token, string.Empty, SteamCommunityDomain));
                _cookies.Add(new Cookie("steamLoginSecure", TokenSecure, string.Empty, SteamCommunityDomain));
                _cookies.Add(new Cookie("Steam_Language", "english", string.Empty, SteamCommunityDomain));

                return true;
            }
        }



        /// <summary>
        /// Helper method to verify our precious cookies.
        /// </summary>
        /// <returns>true if cookies are correct; false otherwise</returns>
        public bool VerifyCookies()
        {
            using (HttpWebResponse response = Request("http://steamcommunity.com/", "HEAD"))
            {
                return response.Cookies["steamLogin"] == null || !response.Cookies["steamLogin"].Value.Equals("deleted");
            }
        }

       


        /// <summary>
        /// Method to allow all certificates.
        /// </summary>
        /// <param name="sender">An object that contains state information for this validation.</param>
        /// <param name="certificate">The certificate used to authenticate the remote party.</param>
        /// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="policyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>Always true to accept all certificates.</returns>
        public bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }
    }



}
