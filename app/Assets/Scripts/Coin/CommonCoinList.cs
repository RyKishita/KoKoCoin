using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin
{
    class CommonCoinList : VersionCoinList
    {
        CommonCoinList()
        {
            Regist(new Dust());
        }

        protected override string VersionName => "common";

        public static CommonCoinList Instance { get; } = new CommonCoinList();

        public override bool IsMatch(Defines.CoinVersion coinVersion)
        {
            return coinVersion.HasFlag(Defines.CoinVersion.Common);
        }
    }
}
