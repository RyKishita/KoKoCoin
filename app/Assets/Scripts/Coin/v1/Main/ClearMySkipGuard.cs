using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Main
{
    class ClearMySkipGuard : Coin
    {
        public const string name = "v1.ClearMySkipGuard";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new[]
        {
            new Body.RemovePlayerCondition(name, true, Duel.PlayerCondition.PlayerConditionDetailSkipGuard.CreatePlayerCondition(1))
        };

        public override string PrefabName { get { return "ClearMySkipGuard"; } }
    }
}
