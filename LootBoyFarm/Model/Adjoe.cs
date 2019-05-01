using System;
using System.Collections.Generic;
using System.Text;

namespace LootBoyFarm
{
    public class Permission
    {
        public DateTime AcceptanceDate { get; set; }
        public int AcceptanceVersion { get; set; }
        public bool Accepted { get; set; }
    }

    public class Config
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public class AdjoeUserRegister
    {
        public string SDKHash { get; set; }
        public string UserUUID { get; set; }
        public string ExternalUserID { get; set; }
        public int FraudScore { get; set; }
        public Permission Permission { get; set; }
        public List<Config> Configs { get; set; }
    }

    public class RewardConfig
    {
        public int Level { get; set; }
        public int Seconds { get; set; }
        public int Coins { get; set; }
        public string Currency { get; set; }
    }

    public class Campaign
    {
        public string CampaignType { get; set; }
        public string AppID { get; set; }
        public string CampaignUUID { get; set; }
        public string TargetingGroupUUID { get; set; }
        public string ClickURL { get; set; }
        public string ViewURL { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string CreativeSetUUID { get; set; }
        public string LandscapeImageURL { get; set; }
        public string IconImageURL { get; set; }
        public string VideoURL { get; set; }
        public int CreativeABTestGroup { get; set; }
        public bool IsAutoClickEnabled { get; set; }
        public List<RewardConfig> RewardConfig { get; set; }
    }

    public class companing
    {
        public List<Campaign> Campaigns { get; set; }
    }


    public class ClickGroup
    {
        public string ClickUUID { get; set; }
        public string TrackingLink { get; set; }
    }
}
