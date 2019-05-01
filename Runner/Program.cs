using System;
using System.Threading;
using LootBoyFarm;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var loot = new LootBoyHandler();
                //loot.Login();
                //loot.Delete();
                loot.Login();
                loot.Farm();
                loot.GetLoot();
                Thread.Sleep(180000);
            }

        }
    }
}
