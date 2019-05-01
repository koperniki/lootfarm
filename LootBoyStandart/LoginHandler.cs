using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using SteamKit2;
using SteamKit2.Internal;
using SteamKit2.Unified.Internal;

namespace LootBoyStandart
{
    public class LoginHandler
    {

        public IPAddress PublicId { get; set; }

        private readonly string _user;
        private readonly string _pass;
        private readonly SteamClient _client;
        private readonly SteamUser _sUser;

        private string _authCode;
        private string _twoFactorAuth;

        private string _userNonce;

        private readonly Func<string> _getCode;

        public string Token { get; set; }


        public LoginHandler(string user, string pass,  Func<string> getCode)
        {
            _getCode = getCode;
            _user = user;
            _pass = pass;

            _client = new SteamClient();
            _sUser = _client.GetHandler<SteamUser>();


            var manager = new CallbackManager(_client);

            manager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
            manager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);
            manager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            manager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
            manager.Subscribe<SteamUser.LoginKeyCallback>(OnKeyCallback);

            _client.Connect();

            while (Token  == null)
            {
                manager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
            }

        }

        private void OnKeyCallback(SteamUser.LoginKeyCallback obj)
        {
            var test = new SteamWeb();
            var IsLoggedIn = false;
            do
            {
                 IsLoggedIn = test.Authenticate(obj.UniqueID.ToString(), _client, _userNonce);

                if (!IsLoggedIn)
                {
                    //Log.Warn("Authentication failed, retrying in 2s...");
                    Thread.Sleep(2000);
                }
            } while (!IsLoggedIn);

            var cock = test.VerifyCookies();
            var steamh = new SteamHandler(test);

            Token = steamh.GetNextTocken();

            _client.Disconnect();

        }



        void OnConnected(SteamClient.ConnectedCallback callback)
        {
            Console.WriteLine("Connected to Steam! Logging in '{0}'...", _user);

            _sUser.LogOn(new SteamUser.LogOnDetails
            {
                Username = _user,
                Password = _pass,

                // in this sample, we pass in an additional authcode
                // this value will be null (which is the default) for our first logon attempt
                AuthCode = _authCode,

                // if the account is using 2-factor auth, we'll provide the two factor code instead
                // this will also be null on our first logon attempt
                TwoFactorCode = _twoFactorAuth,
            });
            _authCode = null;
            _twoFactorAuth = null;
        }


        void OnDisconnected(SteamClient.DisconnectedCallback callback)
        {
            Console.WriteLine("Disconnected from Steam");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _client.Connect();

        }

        void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            bool isSteamGuard = callback.Result == EResult.AccountLogonDenied;
            bool is2FA = callback.Result == EResult.AccountLoginDeniedNeedTwoFactor;

            if (isSteamGuard || is2FA)
            {

                Console.WriteLine("This account is SteamGuard protected!");

                if (is2FA)
                {
                    Console.Write("Please enter your 2 factor auth code from your authenticator app: ");

                    _twoFactorAuth = _getCode();
                }
                else
                {
                    Console.Write("Please enter the auth code sent to the email at {0}: ", callback.EmailDomain);

                    _authCode = _getCode();
                    Console.Write(_authCode + "\n");
                }
                //<span style="font-size:24px;color:#66c0f4;font-family:Arial,Helvetica,sans-serif;font-weight:bold;"> 8DGXG </span>
                return;
            }

            if (callback.Result != EResult.OK)
            {
       
                Console.WriteLine("Unable to logon to Steam: {0} / {1}", callback.Result, callback.ExtendedResult);
                return;
            }
            _userNonce = callback.WebAPIUserNonce;
            Console.WriteLine("Successfully logged on!");
        }


        void OnLoggedOff(SteamUser.LoggedOffCallback callback)
        {
            Console.WriteLine("Logged off of Steam: {0}", callback.Result);

        }


      

    }
}
