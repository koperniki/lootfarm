using System;
using System.Collections.Generic;

namespace LootBoyStandart
{
    public class Statistics
    {
        public int totalLootcoins { get; set; }
        public int premiumLootcoins { get; set; }
        public int dailyLootcoins { get; set; }
        public int codesLootcoins { get; set; }
        public int shredCardsLootcoins { get; set; }
        public int inviteLootcoins { get; set; }
        public int adColonyLootcoins { get; set; }
        public int ironsrcLootcoins { get; set; }
        public int questLootcoins { get; set; }
    }

    public class Json
    {
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int lastlogoff { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public int personastate { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
    }

    public class Photo
    {
        public string value { get; set; }
    }

    public class SteamProfile
    {
        public string provider { get; set; }
        public Json _json { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
        public List<Photo> photos { get; set; }
    }

    public class LootBoyUserInfo
    {
        public Statistics statistics { get; set; }
        public int schemaVersion { get; set; }
        public List<object> roles { get; set; }
        public int lootcoinBalance { get; set; }
        public int lootgemBalance { get; set; }
        public object lastAppStartBonus { get; set; }
        public object emailVerified { get; set; }
        public List<object> desiredRewardChannels { get; set; }
        public List<object> desiredGenres { get; set; }
        public List<object> seenHints { get; set; }
        public object disabled { get; set; }
        public object blacklisted { get; set; }
        public bool whiteListed { get; set; }
        public object deleted { get; set; }
        public bool consent { get; set; }
        public string _id { get; set; }
        public string deviceId { get; set; }
        public object recruiter { get; set; }
        public SteamProfile steamProfile { get; set; }
        public string displayName { get; set; }
        public string country { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
        public bool passwordHash { get; set; }
    }


    public class Content
    {
        public string text { get; set; }
        public string _id { get; set; }
        public string contentType { get; set; }
        public string id { get; set; }
    }

    public class Loot
    {
        public string name { get; set; }
        public string bannerImage { get; set; }
        public string bannerImageIOS { get; set; }
        public string bannerDetailImage { get; set; }
        public string bundleImage { get; set; }
        public string bundleImageTexture { get; set; }
        public bool archived { get; set; }
        public bool disabled { get; set; }
        public bool featured { get; set; }
        public string purchaseType { get; set; }
        public List<string> cardTemplates { get; set; }
        public int price { get; set; }
        public string currency { get; set; }
        public int minUserAge { get; set; }
        public List<string> availableInCountries { get; set; }
        public List<object> excludedCountries { get; set; }
        public bool show { get; set; }
        public int sortPosition { get; set; }
        public bool repurchaseble { get; set; }
        public List<object> extends { get; set; }
        public string _id { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime createdAt { get; set; }
        public string cardMappingType { get; set; }
        public DateTime showFrom { get; set; }
        public DateTime availableFrom { get; set; }
        public string key { get; set; }
        public int __v { get; set; }
        public string productId { get; set; }
        public string productIdIOS { get; set; }
        public List<Content> content { get; set; }
        public string id { get; set; }
    }

    public class LootResponce
    {
        public string _id { get; set; }
        public string status { get; set; }
        public string transactionId { get; set; }
        public Loot loot { get; set; }
        public string application { get; set; }
        public string vendor { get; set; }
        public List<object> purchases { get; set; }
        public int newLootcoinBalance { get; set; }
        public int newLootgemBalance { get; set; }
    }


    public class Genre
    {
        public string name { get; set; }
        public string _id { get; set; }
        public string id { get; set; }
    }

    public class Platform
    {
        public string name { get; set; }
        public string image { get; set; }
        public string _id { get; set; }
        public string id { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public string _id { get; set; }
        public string id { get; set; }
    }

    public class CardTemplate
    {
        public string name { get; set; }
        public string image { get; set; }
        public int lootcoinBonus { get; set; }
        public int lootgemBonus { get; set; }
        public double uvpValue { get; set; }
        public int rating { get; set; }
        public bool hideInInventory { get; set; }
        public List<Genre> genres { get; set; }
        public List<Platform> platforms { get; set; }
        public List<Type> types { get; set; }
        public string _id { get; set; }
        public string key { get; set; }
        public string id { get; set; }
    }

    public class Card
    {
        public object disabled { get; set; }
        public object shredded { get; set; }
        public string _id { get; set; }
        public string user { get; set; }
        public string fromPurchase { get; set; }
        public CardTemplate cardTemplate { get; set; }
        public DateTime assignedToUserAt { get; set; }
        public int __v { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string id { get; set; }
    }

    public class OpenResponce
    {
        public bool alreadyOpened { get; set; }
        public List<Card> cards { get; set; }
        public int lootcoinBonus { get; set; }
        public int lootgemBonus { get; set; }
        public int newLootcoinBalance { get; set; }
        public int newLootgemBalance { get; set; }
    }

    public class CardResponce
    {
        public string _id { get; set; }
        public CardTemplate cardTemplate { get; set; }
        public string id { get; set; }
    }

    public class RewarResponce
    {
        public string _id { get; set; }
        public string rewardType { get; set; }
        public string bulk { get; set; }
        public string code { get; set; }
        public string uri { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime usedAt { get; set; }
        public string id { get; set; }
    }
}
