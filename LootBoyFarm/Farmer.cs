using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace LootBoyFarm
{
    public class Farmer
    {

        static Random _rnd = new Random();

        RestClient _lootClient  = new RestClient("https://api.lootboy.de/");
        RestClient _adjoeClient  = new RestClient("https://prod.adjoe.zone/");

        private string _token =
                "eyJhbGciOiJSUzI1NiJ9.eyJ1c2VyIjoiNWNhMDNlZTNjZDg4MjAwMDI5ZGM4NDRkIiwicm9sZXMiOltdfQ.rz7SCJp5suO1ciNDy4HhIODIEVuZ0RcRrdbLdLHIwJAUdAluIS45xUWsgSLRN6q8LFjIEbmCAZZxVw4PBpGPb1bjQSFknekeAXtXm4Vopsz2-UjGWxn1EGU_ByMqgEuUz47uvy7u1gJvuoWYk8Mt5THJ9F9qLKXcbgfwe_g6ZEZvcQsubdRMMXfjAZCUy_aoi50zC6_MClioJcdJjOtjftVJzMjAYYPI2_OBLdN-wNdRWWcAtlj5oMft0mQF7ts5VkHtg2TxzBDKEMOt1AI6uLRcjXsUDYLgtJojCxAsVDHxwkmDBUbPMr5R71aOQljXxMxWt34uPCfa0Kdr-W6ybQ-SEdJPIzBqjhzyIGVKwC3DNi0Ajvi9l3KHt-0JjMtBVoIL0wG1ClUK_C86LYI6fTrEvKRP5SSfC2XPwSPwvqkecWPcjWN7-LWaf95U7p0a9siCLxRH6UdAB2S6enTNc2B9xSpGtJgH06vfHFEk91eMUtBLULWzQ6EPJE9NW9MRGgGJNEfoN-ZESDQihZLY1ENqOYiIX6ipo9hXDKfP8Njo4MN4ZSrJDcrSsJgueBatsbHeUVdudnZ-0Uzg4RqHWHHMyXOsTNEZtC8ZYis6qhE_YK9CvmEhye_x6nNnLG4doqDrVRE6--BJHZxJ-QEKXuITaLEC-jwtZltaCJecFDc"
            ;

        private string _deviceId = "876c207a-0458-4f55-8496-b68b5966234f";
        private string _user = "5ca03ee3cd88200029dc844d";

        private string _adjoeUser = "8306f346-5abe-49c6-8bb4-4951a2a5862d";

        private string deficeHash = "231851419121655d2f1e122b3c9308cba87ac4a754f30afa90d9efef0648811f";


        private string candyCrashUid = "1d28dc28-5f14-4f0f-8761-53a6e0cf3701";
        private string cccreativeUid = "ca5fac52-d66c-41b2-83b8-b789b895f374";
        private string candyCrashUid4 = "1d28dc28-5f14-4f0f-8761-53a6e0cf3701";

        public void GetUserInfo()
        {
            var url = $"/v2/users/{_user}";
            var request = new RestRequest(url,Method.GET);
            request.AddParameter("authorization", $"Bearer {_token}", ParameterType.HttpHeader);
            request.AddParameter("accept", "application/json", ParameterType.HttpHeader);
            

            var response = _lootClient.Execute<LootBoyUserInfo>(request);
            if (response.IsSuccessful)
            {
                var userInfo = response.Data;
            }

        }

        public void StartAdjoe()
        {

            //var deficeHash = RandomString(64);
            var url = $"/v1/sdk/437f94999f186847220ab2efd4fe322e/devicehash/{deficeHash}";

            var request = new RestRequest(url, Method.POST);


            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);

            //{"SDKHash":"437f94999f186847220ab2efd4fe322e","SDKVersion":11,"AppID":"com.lootboy.app","ProductName":"hammerhead","DeviceName":"hammerhead","IsRooted":true,
            //"OsVersion":"3.4.0-g8a80a0e","ApiLevel":22,"DeviceType":"phone","DisplayResolution":"1080x1920","Country":"unknown",
            //"LocaleCode":"ru_RU","Platform":"android","DeviceIDHash":"cb34ab9f7e158061419a32f5cf881a63ef7e3ec039a76489a7049d2809e2170c","DeviceID":"","ExternalUserID":"5c96f49d5de84f00298ff230"}
            request.AddJsonBody(new
            {
                SDKHash = "437f94999f186847220ab2efd4fe322e",
                SDKVersion = 11,
                AppID = "com.lootboy.app",
                ProductName = "hammerhead",
                DeviceName = "hammerhead",
                IsRooted = true,
                OsVersion = "3.4.0-g8a80a0e",
                ApiLevel = 22,
                DeviceType = "phone",
                DisplayResolution = "1080x1920",
                Country = "unknown",
                LocaleCode = "ru_RU",
                Platform = "android",
                DeviceIDHash = deficeHash,
                DeviceID = "",
                ExternalUserID = _user
            });


            var responce = _adjoeClient.Execute<AdjoeUserRegister>(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Data;
            }



        }



        public void AdjoeOfferwalShowRequested()
        {
            var url = $"v1/user/{_adjoeUser}/device/{deficeHash}/sdk/437f94999f186847220ab2efd4fe322e/event";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);

            //{"Platform":"android","Message":"offerwall_show_requested",
            //"Timestamp":"2019-03-24T09:08:43.528+06:00","Timezone":"Asia\/Omsk","Country":"unknown","Channel":"publisher",
            //"Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"12ea53c5be"}}

            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "offerwall_show_requested",
                Timestamp = string.Format("{0:s}.528+06:00", DateTime.Now),
                Timezone = "Asia\\/Omsk",
                Country = "unknown",
                Channel = "publisher",
                Context = new { AppId = "com.lootboy.app", CampaignUUID = -1 },
                Extra = new { SDKVersion = "11", SessionID = RandomString(10) }

            });


            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
        }


        public void EventAgbShow()
        {


            /*
 POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/sdk/437f94999f186847220ab2efd4fe322e/device/a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b/event HTTP/1.1
Host: prod.adjoe.zone
Connection: keep-alive
Content-Length: 322
adjoe-appversion: 105738614
adjoe-useruuid: 39e71f8e-57a0-410c-bdc8-fc673c4c6e61
Origin: null
adjoe-appid: com.lootboy.app
adjoe-sdk-useragent: Adjoe SDK v1.0.10 (11) Android 22
User-Agent: Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36
content-type: application/json
accept: application/json
adjoe-deviceid-hashed: a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b
adjoe-sdkversion: 11
adjoe-bundleversion: 6
adjoe-sdkhash: 437f94999f186847220ab2efd4fe322e
adjoe-request-id: 4def4106-61c1-4546-88a8-4f8d41f4af40
Accept-Encoding: gzip, deflate
Accept-Language: ru-RU,en-US;q=0.8
X-Requested-With: com.lootboy.app

{"Platform":"android","Message":"agb_shown",
"Timestamp":"2019-03-30T01:47:08.127Z","Timezone":"UTC","Country":"unknown",
"Channel":"user","DeviceID":"a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b",
"Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"5283ce15"}}
             */
            var url = $"/v1/user/{_adjoeUser}/sdk/437f94999f186847220ab2efd4fe322e/device/{deficeHash}/event";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("adjoe-appversion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("adjoe-useruuid", _adjoeUser, ParameterType.HttpHeader);
            request.AddParameter("adjoe-appid", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdk-useragent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("User-Agent", " Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36", ParameterType.HttpHeader);
            request.AddParameter("adjoe-deviceid-hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkversion", 11, ParameterType.HttpHeader);
            request.AddParameter("adjoe-bundleversion", 6, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkhash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);          
            request.AddParameter("adjoe-request-id", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);


            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "agb_shown",
                Timestamp = string.Format("{0:s}.528Z", DateTime.UtcNow),
                Timezone = "UTC",
                Country = "unknown",
                Channel = "user",
                DeviceID = deficeHash,
                Context = new { AppId = "com.lootboy.app", CampaignUUID = -1 },
                Extra = new { SDKVersion = "11", SessionID = RandomString(8) }

            });

            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
        }

        public void SetDeviceId()
        {
            var url = $"/v2/users/{_user}";
            var request = new RestRequest(url, Method.PUT);
            request.AddParameter("authorization", $"Bearer {_token}", ParameterType.HttpHeader);
            request.AddParameter("accept", "application/json", ParameterType.HttpHeader);
            request.AddJsonBody(new { deviceId = _deviceId });

            var response = _lootClient.Execute<LootBoyUserInfo>(request);
            if (response.IsSuccessful)
            {
                var userInfo = response.Data;
            }

        }

        public void AdjoeDevice()
        {

            //var deficeHash = RandomString(64);
            var url = $"/v1/sdk/437f94999f186847220ab2efd4fe322e/user/{_adjoeUser}/device/{_deviceId}";

            var request = new RestRequest(url, Method.POST);


            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID", _deviceId, ParameterType.HttpHeader);
            //{"SDKHash":"437f94999f186847220ab2efd4fe322e","SDKVersion":11,"AppID":"com.lootboy.app","ProductName":"hammerhead","DeviceName":"hammerhead","IsRooted":true,
            //"OsVersion":"3.4.0-g8a80a0e","ApiLevel":22,"DeviceType":"phone","DisplayResolution":"1080x1920","Country":"unknown",
            //"LocaleCode":"ru_RU","Platform":"android","DeviceIDHash":"cb34ab9f7e158061419a32f5cf881a63ef7e3ec039a76489a7049d2809e2170c","DeviceID":"","ExternalUserID":"5c96f49d5de84f00298ff230"}
            request.AddJsonBody(new
            {
                SDKHash = "437f94999f186847220ab2efd4fe322e",
                SDKVersion = 11,
                AppID = "com.lootboy.app",
                ProductName = "hammerhead",
                DeviceName = "hammerhead",
                IsRooted = true,
                OsVersion = "3.4.0-g8a80a0e",
                ApiLevel = 22,
                DeviceType = "phone",
                DisplayResolution = "1080x1920",
                Country = "unknown",
                LocaleCode = "ru_RU",
                Platform = "android",
                DeviceIDHash = deficeHash,
                DeviceID = _deviceId,
                ExternalUserID = _user,

            });


            var responce = _adjoeClient.Execute<AdjoeUserRegister>(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Data;
            }



        }


        public void EventAgbAccepted()
        {


            /*
POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/sdk/437f94999f186847220ab2efd4fe322e/device/a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b/event HTTP/1.1
Host: prod.adjoe.zone
Connection: keep-alive
Content-Length: 325
adjoe-appversion: 105738614
adjoe-useruuid: 39e71f8e-57a0-410c-bdc8-fc673c4c6e61
Origin: null
adjoe-appid: com.lootboy.app
adjoe-sdk-useragent: Adjoe SDK v1.0.10 (11) Android 22
User-Agent: Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36
content-type: application/json
accept: application/json
adjoe-deviceid-hashed: a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b
adjoe-sdkversion: 11
adjoe-bundleversion: 6
adjoe-sdkhash: 437f94999f186847220ab2efd4fe322e
adjoe-request-id: 195ce186-a521-44f8-8229-e1efe15a6bcc
Accept-Encoding: gzip, deflate
Accept-Language: ru-RU,en-US;q=0.8
X-Requested-With: com.lootboy.app

{"Platform":"android","Message":"agb_accepted",
"Timestamp":"2019-03-30T01:49:20.822Z","Timezone":"UTC","Country":"unknown","Channel":"user",
"DeviceID":"a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b",
"Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"5283ce15"}}
             */
            var url = $"/v1/user/{_adjoeUser}/sdk/437f94999f186847220ab2efd4fe322e/device/{deficeHash}/event";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("adjoe-appversion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("adjoe-useruuid", _adjoeUser, ParameterType.HttpHeader);
            request.AddParameter("adjoe-appid", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdk-useragent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("User-Agent", " Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36", ParameterType.HttpHeader);
            request.AddParameter("adjoe-deviceid-hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkversion", 11, ParameterType.HttpHeader);
            request.AddParameter("adjoe-bundleversion", 6, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkhash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("adjoe-request-id", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);


            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "agb_accepted",
                Timestamp = string.Format("{0:s}.538Z", DateTime.UtcNow),
                Timezone = "UTC",
                Country = "unknown",
                Channel = "user",
                DeviceID = deficeHash,
                Context = new { AppId = "com.lootboy.app", CampaignUUID = -1 },
                Extra = new { SDKVersion = "11", SessionID = RandomString(8) }

            });

            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
        }

        public void EventSendDeviceApps()
        {

            //var deficeHash = RandomString(64);
            var url = $"/v1/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/event";

            var request = new RestRequest(url, Method.POST);


            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);


            /*
             {"Platform":"android","Message":"send_device_apps","Timestamp":"2019-03-30T07:49:22.467+06:00","Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
             "Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"0b74a6ea57"}}
             */
            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "send_device_apps",
                Timestamp = string.Format("{0:s}.538+06:00", DateTime.Now),
                Timezone = "Asia\\/Omsk",
                Country = "unknown",
                Channel = "system",
                DeviceID = deficeHash,
                Context = new { AppId = "com.lootboy.app", CampaignUUID = -1 },
                Extra = new { SDKVersion = "11", SessionID = RandomString(10) }

            });


            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce;
            }



        }



        public void AppList()
        {
// POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/device/abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d/sdk/437f94999f186847220ab2efd4fe322e/applist HTTP/1.1


              //var deficeHash = RandomString(64);
              var url = $"/v1/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/applist";

            var request = new RestRequest(url, Method.POST);


            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);


            /*
             {"Platform":"android","Message":"send_device_apps","Timestamp":"2019-03-30T07:49:22.467+06:00","Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
             "Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"0b74a6ea57"}}
             */
            request.AddJsonBody(new
            {
                Platform = "android",
                UserApps = new [] {
                    new { AppID = "com.google.android.youtube",
                    InstalledAt = "1970-07-03T12:06:13.000+06:00",
                    AppUpdatedAt = "1970-07-03T12:06:13.000+06:00"}
                }

            });


            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce;
            }



        }


        public void AppListWith(string app)
        {
            // POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/device/abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d/sdk/437f94999f186847220ab2efd4fe322e/applist HTTP/1.1


            //var deficeHash = RandomString(64);
            var url = $"/v1/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/applist";

            var request = new RestRequest(url, Method.POST);


            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);


            /*
             {"Platform":"android","Message":"send_device_apps","Timestamp":"2019-03-30T07:49:22.467+06:00","Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
             "Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"0b74a6ea57"}}
             */
            request.AddJsonBody(new
            {
                Platform = "android",
                UserApps = new[] {
                    new { AppID = "com.google.android.youtube",
                    InstalledAt = "1970-07-03T12:06:13.000+06:00",
                    AppUpdatedAt = "1970-07-03T12:06:13.000+06:00"},
                    new { AppID = app,
                        InstalledAt = string.Format("{0:s}.528+06:00", DateTime.Now),
                        AppUpdatedAt = string.Format("{0:s}.528+06:00", DateTime.Now)}
                }

            });


            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce;
            }



        }



        public void UsageHistory()
        {

            //var deficeHash = RandomString(64);
            var url = $"/v1/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/usage_history";

            var request = new RestRequest(url, Method.POST);


            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);


            /*
             {"Platform":"android","Message":"send_device_apps","Timestamp":"2019-03-30T07:49:22.467+06:00","Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
             "Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"0b74a6ea57"}}
             */
            request.AddJsonBody(new
            {
                Platform = "android",
                UserAppsUsageHistory = new[] {
                    new { AppID = "com.google.android.youtube",
                    SecondsCum = 136298}
                }

            });


            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce;
            }



        }




        public void CompaningDestibution()
        {
            /*
             * GET https://prod.adjoe.zone/v1/campaign-distribution/offerwall/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/device/abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d/sdk/437f94999f186847220ab2efd4fe322e/ru HTTP/1.1
Adjoe-AppID: com.lootboy.app
             */
            //var deficeHash = RandomString(64);
            var url = $"/v1/campaign-distribution/offerwall/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/ru";

            var request = new RestRequest(url, Method.GET);


            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);


            var responce = _adjoeClient.Execute<companing>(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Data;
            }



        }


        public void EventUserPermissionAccept()
        {


            /*
POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/sdk/437f94999f186847220ab2efd4fe322e/device/a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b/event HTTP/1.1
Host: prod.adjoe.zone
Connection: keep-alive
Content-Length: 325
adjoe-appversion: 105738614
adjoe-useruuid: 39e71f8e-57a0-410c-bdc8-fc673c4c6e61
Origin: null
adjoe-appid: com.lootboy.app
adjoe-sdk-useragent: Adjoe SDK v1.0.10 (11) Android 22
User-Agent: Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36
content-type: application/json
accept: application/json
adjoe-deviceid-hashed: a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b
adjoe-sdkversion: 11
adjoe-bundleversion: 6
adjoe-sdkhash: 437f94999f186847220ab2efd4fe322e
adjoe-request-id: 195ce186-a521-44f8-8229-e1efe15a6bcc
Accept-Encoding: gzip, deflate
Accept-Language: ru-RU,en-US;q=0.8
X-Requested-With: com.lootboy.app

{"Platform":"android","Message":"agb_accepted",
"Timestamp":"2019-03-30T01:49:20.822Z","Timezone":"UTC","Country":"unknown","Channel":"user",
"DeviceID":"a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b",
"Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"5283ce15"}}
             */
            var url = $"/v1/user/{_adjoeUser}/sdk/437f94999f186847220ab2efd4fe322e/device/{_deviceId}/event";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("adjoe-appversion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("adjoe-useruuid", _adjoeUser, ParameterType.HttpHeader);
            request.AddParameter("adjoe-appid", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdk-useragent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("User-Agent", " Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36", ParameterType.HttpHeader);
            request.AddParameter("adjoe-deviceid-hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("adjoe-deviceid", _deviceId, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkversion", 11, ParameterType.HttpHeader);
            request.AddParameter("adjoe-bundleversion", 6, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkhash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("adjoe-request-id", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);


            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "usage_permission_accepted",
                Timestamp = string.Format("{0:s}.538Z", DateTime.UtcNow),
                Timezone = "UTC",
                Country = "unknown",
                Channel = "user",
                DeviceID = _deviceId,
                Context = new { AppId = "com.lootboy.app", CampaignUUID = -1 },
                Extra = new { SDKVersion = "11", SessionID = RandomString(8) }

            });

            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
        }


        public static string RandomString(int length)
        {
            const string chars = "0123456789abcdef";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_rnd.Next(s.Length)]).ToArray());
        }




        public void InstalClicked()
        {


            /*
POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/sdk/437f94999f186847220ab2efd4fe322e/device/abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d/event HTTP/1.1
Host: prod.adjoe.zone
Connection: keep-alive
Content-Length: 372
adjoe-appversion: 105738614
adjoe-useruuid: 39e71f8e-57a0-410c-bdc8-fc673c4c6e61
Origin: null
adjoe-appid: com.lootboy.app
adjoe-sdk-useragent: Adjoe SDK v1.0.10 (11) Android 22
User-Agent: Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36
content-type: application/json
accept: application/json
adjoe-deviceid: abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d
adjoe-deviceid-hashed: a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b
adjoe-sdkversion: 11
adjoe-bundleversion: 6
adjoe-sdkhash: 437f94999f186847220ab2efd4fe322e
adjoe-request-id: 118be8d1-6812-4b89-9141-9820a1259715
Accept-Encoding: gzip, deflate
Accept-Language: ru-RU,en-US;q=0.8
X-Requested-With: com.lootboy.app

{"Platform":"android","Message":"install_clicked",
"Timestamp":"2019-03-30T01:57:36.412Z","Timezone":"UTC","Country":"unknown",
"Channel":"user","DeviceID":"abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d",
"Context":{"AppId":"com.lootboy.app","CampaignUUID":-1,"ClickAppId":"com.king.candycrushsaga","Percentage":99,"Where":"button"},
"Extra":{"SDKVersion":"11","SessionID":"12f27e69"}}
             */
            var url = $"/v1/user/{_adjoeUser}/sdk/437f94999f186847220ab2efd4fe322e/device/{_deviceId}/event";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("adjoe-appversion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("adjoe-useruuid", _adjoeUser, ParameterType.HttpHeader);
            request.AddParameter("adjoe-appid", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdk-useragent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("User-Agent", " Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36", ParameterType.HttpHeader);
            request.AddParameter("adjoe-deviceid-hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkversion", 11, ParameterType.HttpHeader);
            request.AddParameter("adjoe-bundleversion", 6, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkhash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("adjoe-request-id", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);


            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "install_clicked",
                Timestamp = string.Format("{0:s}.528Z", DateTime.UtcNow),
                Timezone = "UTC",
                Country = "unknown",
                Channel = "user",
                DeviceID = _deviceId,
                Context = new { AppId = "com.lootboy.app",
                    CampaignUUID = -1,
                    ClickAppId = "com.king.candycrush4",
                    Percentage = 99,
                    Where = "button"
                },
                Extra = new { SDKVersion = "11", SessionID = RandomString(8) }

            });

            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
        }



        public void CampaningClickGroup()
        {


            /*
POST https://prod.adjoe.zone/v1/campaign/click/target-group/6b11cf6b-a72a-48fb-982b-3824f376a194/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/device/abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d/sdk/437f94999f186847220ab2efd4fe322e?type=0 HTTP/1.1
Host: prod.adjoe.zone
Connection: keep-alive
Content-Length: 140
adjoe-appversion: 105738614
adjoe-useruuid: 39e71f8e-57a0-410c-bdc8-fc673c4c6e61
Origin: null
adjoe-appid: com.lootboy.app
adjoe-sdk-useragent: Adjoe SDK v1.0.10 (11) Android 22
User-Agent: Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36
content-type: application/json
accept: application/json
adjoe-deviceid: abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d
adjoe-deviceid-hashed: a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b
adjoe-sdkversion: 11
adjoe-bundleversion: 6
adjoe-sdkhash: 437f94999f186847220ab2efd4fe322e
adjoe-request-id: d15103eb-4a53-45cf-b0b9-a59cd4fc1df7
Accept-Encoding: gzip, deflate
Accept-Language: ru-RU,en-US;q=0.8
X-Requested-With: com.lootboy.app

{"Country":"unknown","CreativeSetUUID":"b7c000f6-3e9d-4d5a-8b2b-e394a8a9c452","Timestamp":"2019-03-30T01:57:36.431Z","AdFormat":"offerwall"}
             */
            var url = $"/v1/campaign/click/target-group/{candyCrashUid}/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("adjoe-appversion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("adjoe-useruuid", _adjoeUser, ParameterType.HttpHeader);
            request.AddParameter("adjoe-appid", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdk-useragent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("User-Agent", " Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36", ParameterType.HttpHeader);
            request.AddParameter("adjoe-deviceid-hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkversion", 11, ParameterType.HttpHeader);
            request.AddParameter("adjoe-bundleversion", 6, ParameterType.HttpHeader);
            request.AddParameter("adjoe-sdkhash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("adjoe-request-id", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("type", 0, ParameterType.QueryString);


            request.AddJsonBody(new
            {
                Country = "unknown",
                CreativeSetUUID = cccreativeUid,
                Timestamp = string.Format("{0:s}.528Z", DateTime.UtcNow),             
                AdFormat = "offerwall"

            });

            var responce = _adjoeClient.Execute<ClickGroup>(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Data;
            }

            var client2 = new RestClient(responce.Data.TrackingLink);
            client2.UserAgent =
                "Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36";
            var request2 = new RestRequest(Method.GET);
            request2.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);
            var result = client2.Execute(request2);
            ;
            var url3 = result.Content.Replace("<html> <body onload=\"document.location.replace(\'", "")
                .Replace("\')\"/> </html>", "");
            var client3 = new RestClient(url3);
            client3.UserAgent =
                "Mozilla/5.0 (Linux; Android 5.1; Nexus 5 Build/LMY47D) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/39.0.0.0 Mobile Safari/537.36";
            var request3 = new RestRequest(Method.GET);
            request3.AddParameter("X-Requested-With", "com.lootboy.app", ParameterType.HttpHeader);
            var result3 = client3.Execute(request3);

            
            ;
            var location = result3.Headers.FirstOrDefault(t => t.Name == "Location");

            ;
            EventTrackingLinkLoad("com.king.candycrush4", responce.Data.TrackingLink, location.Value.ToString());
            EventAppInstalled("com.king.candycrush4", responce.Data.ClickUUID);
            EventSendDeviceApps();
            AppListWith("com.king.candycrush4");
            EventSendUsage();
            SendUsage("com.king.candycrush4");
        }



        public void EventTrackingLinkLoad(string app, string trackingLink, string resolveUrl)
        {


            /*
POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/device/abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d/sdk/437f94999f186847220ab2efd4fe322e/event HTTP/1.1
Adjoe-AppID: com.lootboy.app
Adjoe-SDK-UserAgent: Adjoe SDK v1.0.10 (11) Android 22
Adjoe-AppVersion: 105738614
Adjoe-SDKVersion: 11
Adjoe-Request-ID: 67a9a046-6942-4ceb-beee-a736b6fd23dd
Adjoe-SDKHash: 437f94999f186847220ab2efd4fe322e
Adjoe-DeviceID: abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d
Adjoe-UserUUID: 39e71f8e-57a0-410c-bdc8-fc673c4c6e61
Adjoe-DeviceID-Hashed: a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b
Content-Type: application/json; charset=utf-8
Content-Length: 849
Host: prod.adjoe.zone
Connection: Keep-Alive
Accept-Encoding: gzip
User-Agent: okhttp/3.9.1

{"Platform":"android","Message":"tracking_link_load",
"Timestamp":"2019-03-30T07:57:39.638+06:00",
"Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
"Context":{"AppId":"com.lootboy.app","CampaignUUID":-1,
"tracking_link":"https:\/\/track.56txs4.com\/dlclicks?accesstoken=c6163877-740b-4163-9241-3cb0949f0142&campaignpartnerid=1358383&creative_set_id=285376&ot=dir&subid=146249e7-9d51-4566-a314-86f0cf7aefb9&deviceAndroidId=abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d&deviceIfa=abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d&subid2=2.43&consent=true",
"resolved_url":"market:\/\/details?id=com.king.candycrushsaga&referrer=ruid%3Dcid517-1553911059914-1216%26utm_source%3Dyouappi%26utm_campaign%3Dccs_us_android_ron_all_cpi_bau_all%26utm_content%3D",
"reason":"resolved","app_id":"com.king.candycrushsaga"},"Extra":{"SDKVersion":"11","SessionID":"d8bb3b725d"}}
             */
            var url = $"/v1/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/event";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);


            /*
             {"Platform":"android","Message":"send_device_apps","Timestamp":"2019-03-30T07:49:22.467+06:00","Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
             "Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"0b74a6ea57"}}
             */
            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "tracking_link_load",
                Timestamp = string.Format("{0:s}.538+06:00", DateTime.Now),
                Timezone = "Asia\\/Omsk",
                Country = "unknown",
                Channel = "system",
                DeviceID = deficeHash,
                Context = new
                {
                    AppId = "com.lootboy.app", CampaignUUID = -1, app_id = app, reason = "resolved",
                    resolved_url=resolveUrl,
                    tracking_link=trackingLink,
                },
                Extra = new
                {
                    SDKVersion = "11",
                    SessionID = RandomString(10),
                   
                }

            });

            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
        }



        public void EventAppInstalled(string app, string clickuid)
        {


            /*
POST https://prod.adjoe.zone/v1/user/921226da-fa1a-47fb-a61a-06365ebbe8a1/device/bc6937c4-a8fe-46eb-8060-ddfea86ab6ad/sdk/437f94999f186847220ab2efd4fe322e/event HTTP/1.1
Adjoe-AppID: com.lootboy.app
Adjoe-SDK-UserAgent: Adjoe SDK v1.0.10 (11) Android 22
Adjoe-AppVersion: 105738614
Adjoe-SDKVersion: 11
Adjoe-Request-ID: 153357ff-b4cf-4ca0-9ee3-1572ceca1e44
Adjoe-SDKHash: 437f94999f186847220ab2efd4fe322e
Adjoe-DeviceID: bc6937c4-a8fe-46eb-8060-ddfea86ab6ad
Adjoe-UserUUID: 921226da-fa1a-47fb-a61a-06365ebbe8a1
Adjoe-DeviceID-Hashed: 22615c758cf35e33456b45e0a3728b8be12979e4af3b759fe4af967291f763a8
Content-Type: application/json; charset=utf-8
Content-Length: 460
Host: prod.adjoe.zone
Connection: Keep-Alive
Accept-Encoding: gzip
User-Agent: okhttp/3.9.1

{"Platform":"android","Message":"app_installed","Timestamp":"2019-03-30T13:20:03.702+07:00",
"Timezone":"Asia\/Bangkok","Country":"US","Channel":"system","Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},
"Extra":{"SDKVersion":"11","SessionID":"085cda7580","AppID":"com.king.candycrush4","InstalledAt":"2019-03-30T13:19:58.297+07:00",
"AppUpdatedAt":"2019-03-30T13:19:58.297+07:00","ClickUUID":"a1330924-40b2-408e-8f5f-3f66b7e5f83e","AdFormat":"offerwall"}}
             */
            var url = $"/v1/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/event";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);


            /*
             {"Platform":"android","Message":"send_device_apps","Timestamp":"2019-03-30T07:49:22.467+06:00","Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
             "Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"0b74a6ea57"}}
             */
            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "app_installed",
                Timestamp = string.Format("{0:s}.538+06:00", DateTime.Now),
                Timezone = "Asia\\/Omsk",
                Country = "unknown",
                Channel = "system",
                DeviceID = deficeHash,
                Context = new { AppId = "com.lootboy.app", CampaignUUID = -1 },
                Extra = new
                {
                    SDKVersion = "11", SessionID = RandomString(10) , AppID = app ,
                    InstalledAt = string.Format("{0:s}.538+06:00", DateTime.Now),
                    AppUpdatedAt = string.Format("{0:s}.538+06:00", DateTime.Now),
                    ClickUUID = clickuid,
                    AdFormat = "offerwall",
                }

            });

            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
        }



        public void EventSendUsage()
        {


            /*
POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/device/abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d/sdk/437f94999f186847220ab2efd4fe322e/event HTTP/1.1
Adjoe-AppID: com.lootboy.app
Adjoe-SDK-UserAgent: Adjoe SDK v1.0.10 (11) Android 22
Adjoe-AppVersion: 105738614
Adjoe-SDKVersion: 11
Adjoe-Request-ID: cf9a1128-9ac3-416d-bc7e-a609b16715af
Adjoe-SDKHash: 437f94999f186847220ab2efd4fe322e
Adjoe-DeviceID: abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d
Adjoe-UserUUID: 39e71f8e-57a0-410c-bdc8-fc673c4c6e61
Adjoe-DeviceID-Hashed: a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b
Content-Type: application/json; charset=utf-8
Content-Length: 261
Host: prod.adjoe.zone
Connection: Keep-Alive
Accept-Encoding: gzip
User-Agent: okhttp/3.9.1

{"Platform":"android","Message":"send_usage","Timestamp":"2019-03-30T08:00:20.195+06:00",
"Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
"Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"dd94c83830"}}
             */
            var url = $"/v1/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/event";

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);


            /*
             {"Platform":"android","Message":"send_device_apps","Timestamp":"2019-03-30T07:49:22.467+06:00","Timezone":"Asia\/Omsk","Country":"unknown","Channel":"system",
             "Context":{"AppId":"com.lootboy.app","CampaignUUID":-1},"Extra":{"SDKVersion":"11","SessionID":"0b74a6ea57"}}
             */
            request.AddJsonBody(new
            {
                Platform = "android",
                Message = "send_usage",
                Timestamp = string.Format("{0:s}.538+06:00", DateTime.Now),
                Timezone = "Asia\\/Omsk",
                Country = "unknown",
                Channel = "system",
                DeviceID = deficeHash,
                Context = new { AppId = "com.lootboy.app", CampaignUUID = -1 },
                Extra = new
                {
                    SDKVersion = "11",
                    SessionID = RandomString(10),
                }

            });

            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }
        }


        public void SendUsage(string app)
        {
            /*
             *POST https://prod.adjoe.zone/v1/user/39e71f8e-57a0-410c-bdc8-fc673c4c6e61/device/abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d/sdk/437f94999f186847220ab2efd4fe322e/usage HTTP/1.1
Adjoe-AppID: com.lootboy.app
Adjoe-SDK-UserAgent: Adjoe SDK v1.0.10 (11) Android 22
Adjoe-AppVersion: 105738614
Adjoe-SDKVersion: 11
Adjoe-Request-ID: 1a514384-cfd2-4dea-aa86-e3f47b40c7d0
Adjoe-SDKHash: 437f94999f186847220ab2efd4fe322e
Adjoe-DeviceID: abd3a35d-1a3b-42f7-a0b4-d708ffd50d4d
Adjoe-UserUUID: 39e71f8e-57a0-410c-bdc8-fc673c4c6e61
Adjoe-DeviceID-Hashed: a11fc9a6b05488d764590f671e5990daf7ce441447793294d34a2496cbc2756b
Content-Type: application/json; charset=utf-8
Content-Length: 159
Host: prod.adjoe.zone
Connection: Keep-Alive
Accept-Encoding: gzip
User-Agent: okhttp/3.9.1

{"Platform":"android","UserAppUsages":[{"AppID":"com.king.candycrushsaga","StartAt":"2019-03-30T00:59:19.781+06:00","StopAt":"2019-03-30T23:00:19.796+06:00"}]}
             */
            //var deficeHash = RandomString(64);
            var url = $"/v1/user/{_adjoeUser}/device/{_deviceId}/sdk/437f94999f186847220ab2efd4fe322e/usage";

            var request = new RestRequest(url, Method.POST);


            request.AddParameter("Adjoe-AppID", "com.lootboy.app", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDK-UserAgent", "Adjoe SDK v1.0.10 (11) Android 22", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-AppVersion", "105738614", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKHash", "437f94999f186847220ab2efd4fe322e", ParameterType.HttpHeader);
            request.AddParameter("Adjoe-SDKVersion", 11, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-Request-ID", Guid.NewGuid(), ParameterType.HttpHeader);
            request.AddParameter("Adjoe-DeviceID-Hashed", deficeHash, ParameterType.HttpHeader);
            request.AddParameter("Adjoe-UserUUID", _adjoeUser, ParameterType.HttpHeader);

            request.AddJsonBody(new
            {
                Platform = "android",
                UserAppUsages = new[]
                {
                    new
                    {
                        AppID = app,
                        StartAt = string.Format("{0:s}.528+06:00", DateTime.Today),
                        StopAt = string.Format("{0:s}.528+06:00", DateTime.Today.AddDays(1).AddMinutes(-1))
                    }
                }
            });

            var responce = _adjoeClient.Execute(request);
            if (responce.IsSuccessful)
            {
                var data = responce.Content;
            }



        }


    }

}
