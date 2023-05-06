using Assets.Scripts.Coin.Effect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class DimensionBeacon : Scripts.Coin.Body.Core
    {
        public DimensionBeacon(string coinName)
            : base(coinName)
        {
            Effects = new IEffect[] {
                new NotUse(),
                new Effect.DimensionBeacon()
            };
        }

        public override Defines.CoinType CoinType => Defines.CoinType.None;

        public override IEffect[] Effects { get; }
    }
}
