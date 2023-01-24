using Assets.Scripts.Duel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class UndeadGuard : Scripts.Coin.Body.Guard.Core
    {
        public UndeadGuard(string coinName)
            : base(coinName)
        {
            Animation = new Scripts.Duel.DuelAnimation.Guard.GuardAnimationWall();
        }

        public override Assets.Scripts.Coin.Effect.IEffect[] Effects { get; } = new Assets.Scripts.Coin.Effect.IEffect[] {
            new Effect.AppendValuesByCurrentAreaTag(true, needCoinTag, 900),
            new Effect.UseToMoveCurrentAreaTagALL(Defines.CoinPosition.Trash, needCoinTag)
        };

        const Defines.CoinTag needCoinTag = Defines.CoinTag.死霊;

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;

            // IsNoNeed だと現在のエリアに死霊がいなくても防御値ゼロで使ってしまうので確認
            var player = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo];
            return duelData.FieldData.AreaDatas[player.CurrentAreaNo]
                    .GetCoinsByOwner(player)
                    .Any(scd => scd.GetCoinTag().HasFlag(needCoinTag));
        }
    }
}
