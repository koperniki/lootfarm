using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using LootBoyFarm.Model;
using RestSharp;

namespace LootBoyFarm
{
    public class AdjoeHandler
    {
        RestClient _client = new RestClient("https://prod.adjoe.zone/")
        {
            //Proxy = new WebProxy("69.180.145.203", 80)
        };

        static Random _rnd = new Random();

        private List<UserApp> UserApps { get; set; }
        private List<object> UserAppsUsageHistory { get; set; }

        private string _externalUserId;
        private string _adjoeUserId;


        private string _deviceId;
        private string _deviceHash;

        private string _sessionId;
       

        public AdjoeHandler(string externalUserId, string deviceId)
        {
            _externalUserId = externalUserId;
            _deviceId = deviceId;
            _deviceHash = RandomString(64);
            _sessionId = RandomString(10);

            var apps = new List<string>
            {
                "com.android.providers.telephony",
                "com.android.providers.calendar",
                "com.android.providers.media",
                "com.android.wallpapercropper",
                "com.android.documentsui",
                "com.android.externalstorage",
                "com.android.htmlviewer",
                "com.android.mms.service",
                "com.android.browser",
                "com.lootboy.app",
                "com.android.defcontainer",
                "com.android.providers.downloads.ui",
                "com.android.vending",
                "com.android.pacprocessor",
                "com.android.settings",
                "com.android.packageinstaller",
                "com.vphone.launcher"
            };

            UserApps = apps.Select(t => new UserApp
            {
                AppID = t,
                AppUpdatedAt = string.Format("{0:s}.000+07:00", DateTime.Now.AddDays(-_rnd.Next(50, 100))),
                InstalledAt = string.Format("{0:s}.000+07:00", DateTime.Now.AddDays(-_rnd.Next(50))),
                InstallSource = new {}
               
            }).ToList();

            UserAppsUsageHistory = new List<object>()
            {
                new {AppId= "com.lootboy.app", SecondsCum = _rnd.Next(100,10000)},
                new {AppId= "com.vphone.launcher", SecondsCum = _rnd.Next(100,10000)},
                new {AppId= "com.android.packageinstaller", SecondsCum = _rnd.Next(100,10000)},
                new {AppId= "com.android.settings", SecondsCum = _rnd.Next(100,10000)},
            };
        }

        public void Initialize()
        {
            var url = $"/v1/sdk/437f94999f186847220ab2efd4fe322e/devicehash/{_deviceHash}";
            var request = new RestRequest(url, Method.POST);
            AddHeader(request);


            request.AddJsonBody(new
            {
                SDKHash = "437f94999f186847220ab2efd4fe322e",
                SDKVersion = 11,
                AppID = "com.lootboy.app",
                ProductName = "zenphone",
                DeviceName = "zenphone",
                IsRooted = false,
                OsVersion = "3.5.0-g8a80a0e",
                ApiLevel = 23,
                DeviceType = "phone",
                DisplayResolution = "980x1200",
                Country = "RU",
                LocaleCode = "ru_RU",
                Platform = "android",
                DeviceIDHash = _deviceHash,
                DeviceID = _deviceId,
                ExternalUserID = _externalUserId
            });


            var responce = _client.Execute<AdjoeUserRegister>(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Data;
                _adjoeUserId = data.UserUUID;
            }
            else
            {
                Console.WriteLine(responce.Content);
            }
        }

        public void InitFinishedEvent()
        {
            SendEvent("init_finished", "system", null);
        }

        public void OfferwallShowRequestEvent()
        {
            SendEvent("offerwall_show_requested", "publisher", null);
        }

        public void AgbShownEvent()
        {
            SendEvent("agb_shown", "user", null);
        }

        public void AgbAcceptedEvent()
        {
            SendEvent("agb_accepted", "user", null);
        }


        public void AppList()
        {
            var url = $"/v1/user/{_adjoeUserId}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/applist";
            var request = new RestRequest(url, Method.POST);
            AddHeader(request);

            request.AddJsonBody(new
            {
                Platform = "android",
                UserApps
            });

            var responce = _client.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
            else
            {
                Console.WriteLine(responce.Content);
            }
        }

        public void SendDeviceAppsEvent()
        {
            SendEvent("send_device_apps", "system", null);
        }

        public void SendUserAppsUsageHistory()
        {
            var url = $"/v1/user/{_adjoeUserId}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/usage_history";
            var request = new RestRequest(url, Method.POST);
            AddHeader(request);

            request.AddJsonBody(new
            {
                Platform = "android",
                UserAppsUsageHistory
            });

            var responce = _client.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
            else
            {
                Console.WriteLine(responce.Content);
            }
        }

        public void UsagePermissionAcceptedEvent()
        {
            SendEvent("usage_permission_accepted", "user", null);
        }

        public void CampaingDistribution()
        {
            var url = $"/v1/campaign-distribution/offerwall/user/{_adjoeUserId}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/ru";

            var request = new RestRequest(url, Method.GET);
            AddHeader(request);
            var responce = _client.Execute<companing>(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Data;
                var date = DateTime.Now;
                foreach (var campain in data.Campaigns)
                {
                    FarmCampaign(campain.AppID, campain.TargetingGroupUUID, campain.CreativeSetUUID, date);
                    date = date.AddHours(12);
                }

            }
            else
            {
                Console.WriteLine(responce.Content);
            }



        }


        public void CompaingDistibutionAuto()
        {
            var url = $"/v1/campaign-distribution/auto/user/{_adjoeUserId}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/ru";

            var request = new RestRequest(url, Method.GET);
            AddHeader(request);
            var responce = _client.Execute<companing>(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Data;
                var date = DateTime.Now;
                foreach (var campain in data.Campaigns)
                {
                    //FarmCampaign(campain.AppID, campain.TargetingGroupUUID, campain.CreativeSetUUID, date);
                    date = date.AddDays(2);
                }

            }
            else
            {
                Console.WriteLine(responce.Content);
            }
        }

        private void FarmCampaign(string app, string targetGroup, string setUuid, DateTime date)
        {
            CampaignInstalClickedEvent(app);
            var click = CamaignClick(targetGroup, setUuid);
            SendDeviceAppsEvent();
            AppInstaledEvent(app, click.ClickUUID);
            AppList();
            UserAppUsages(app, date, date.AddDays(2));
            SendUsageEvent();
        }

        private void CampaignInstalClickedEvent(string app)
        {
            SendEvent("install_clicked", "user", null, new
            {
                AppId = "com.lootboy.app",
                CampaignUUID = -1,
                ClickAppId = app,
                Percentage = 99,
                Where = "button"

            });
        }

        private ClickGroup CamaignClick(string tragetGroup, string setUuid)
        {
            var url = $"/v1/campaign/click/target-group/{tragetGroup}/user/{_adjoeUserId}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e";

            var request = new RestRequest(url, Method.POST);
            request.AddParameter("type", 0, ParameterType.QueryString);
            AddHeader(request);

            request.AddJsonBody(new
            {
                CreativeSetUUID = setUuid,
                Timestamp = string.Format("{0:s}.528Z", DateTime.UtcNow),
                AdFormat = "offerwall"

            });
            var responce = _client.Execute<ClickGroup>(request);
            if (responce.IsSuccessful)
            {
                return responce.Data;
            }
            else
            {
                Console.WriteLine(responce.Content);
            }

            return null;
        }

        private void AppInstaledEvent(string appId, string clickUid)
        {
            SendEvent("app_installed", "system",  new
                {
                    SDKVersion = "11",
                    SessionID = RandomString(10),
                    AppID = appId,
                    InstalledAt = string.Format("{0:s}.538+07:00", DateTime.Now),
                    AppUpdatedAt = string.Format("{0:s}.538+07:00", DateTime.Now),
                    ClickUUID = clickUid
                
            });
            UserApps.Add(new UserApp()
            {
                AppID = appId,
                InstalledAt = string.Format("{0:s}.538+07:00", DateTime.Now),
                AppUpdatedAt = string.Format("{0:s}.538+07:00", DateTime.Now),
                InstallSource = new InstallSource()
                {
                    ClickUUID = clickUid
                }
            });
        }


        private void UserAppUsages(string appId, DateTime start, DateTime end)
        {
            var url = $"/v1/user/{_adjoeUserId}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/usage";

            var request = new RestRequest(url, Method.POST);
            AddHeader(request);

            request.AddJsonBody(new
            {
                Platform = "android",
                UserAppUsages = new[]
                {
                    new
                    {
                        AppID = appId,
                        StartAt = string.Format("{0:s}.528+06:00", start),
                        StopAt = string.Format("{0:s}.528+06:00", end)
                    }
                }
            });

            var responce = _client.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
            else
            {
                Console.WriteLine(responce.Content);
            }
        }

        private void SendUsageEvent()
        {
            SendEvent("send_usage", "system", null);
        }

        private static string RandomString(int length)
        {
            const string chars = "0123456789abcdef";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_rnd.Next(s.Length)]).ToArray());
        }



        private void SendEvent(string message, string channel, object extra, object context = null)
        {
            var url = $"/v1/user/{_adjoeUserId}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/event";
            var request = new RestRequest(url, Method.POST);
            AddHeader(request);

            request.AddJsonBody(new
            {
                Platform = "android",
                Message = message,
                Timestamp = string.Format("{0:s}.538Z", DateTime.Now),
                Timezone = "UTC",
                Country = "RU",
                Channel = channel,
                DeviceID = _deviceId,
                Context = context ?? new { AppId = "com.lootboy.app", CampaignUUID = -1 },
                Extra = extra ?? new { SDKVersion = "11", SessionID = _sessionId }

            });

            var responce = _client.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
            else
            {
                Console.WriteLine(responce.Content);
            }

        }

        private void AddHeader(RestRequest request)
        {

            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", _deviceHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID", _deviceId, ParameterType.HttpHeader);
            if (!string.IsNullOrEmpty(_adjoeUserId))
            {
                request.AddParameter("Adjoe-UserUUID", _adjoeUserId, ParameterType.HttpHeader);
            }
            

        }

    }
}
