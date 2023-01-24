using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ClearMyInebriety : Coin
    {
        public const string name = "v1.ClearMyInebriety";

        public override string Name => name;

        public override Scripts.Coin.Body.Core[] Bodies { get; } = new []
        {
            new Body.RemovePlayerCondition(name, true, Duel.PlayerCondition.PlayerConditionDetailConfusionMove.CreatePlayerCondition(1))
        };

        public override string PrefabName { get { return "ClearMyInebriety"; } }
    }
}
