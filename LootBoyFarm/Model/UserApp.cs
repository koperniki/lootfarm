using System;
using System.Collections.Generic;
using System.Text;

namespace LootBoyFarm.Model
{
    public class UserApp
    {
        public string AppID { get; set; }
        public string AppUpdatedAt { get; set; }
        public string InstalledAt { get; set; }
        public object InstallSource { get; set; }

    }

    public class InstallSource
    {
        public string AdFormat { get; set; } = "offerwall";
        public string ClickUUID { get; set; }
    }
}
