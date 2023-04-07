using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Body
{
    class FixDice : Scripts.Coin.Body.Support.NoneTarget
    {
        public FixDice(string coinName, bool bCoinOwner, int dice)
            : base(coinName)
        {
            this.bCoinOwner = bCoinOwner;
            PlayerCondition = Duel.PlayerCondition.PlayerConditionDetailFixDice.CreatePlayerCondition(dice);
        }

        public Duel.PlayerCondition.PlayerCondition PlayerCondition { get; }

        readonly bool bCoinOwner;

        protected override IEnumerable<string> Explains
        {
            get
            {
                {
                    var format = bCoinOwner ? 0 : 1;

                    var param = new Dictionary<string, string>()
                    {
                        { "value", PlayerCondition.Value.ToString() },
                    };

                    yield return GetLocalizedString(nameof(FixDice), format, param);
                }

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }

        public override bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!base.IsUsable(duelData, selectedCoinData)) return false;
            var targetPlayer = duelData.Players[selectedCoinData.CoinData.OwnerPlayerNo];
            return (!targetPlayer.ConditionList.Has<Duel.PlayerCondition.PlayerConditionDetailFixDice>() ||
                    targetPlayer.ConditionList.GetItem<Duel.PlayerCondition.PlayerConditionDetailFixDice>().Value != PlayerCondition.Value)
                    && duelData.GetMovableAreaNos(targetPlayer, PlayerCondition.Value).Any();
        }

        public override UniTask ExecuteAsync(DuelManager duelManager, ActionItem actionItem)
        {
            var playerNo = actionItem.GetPlayerNo();
            var targets = bCoinOwner
                            ? new List<int>() { playerNo }
                            : duelManager.DuelData.GetOtherTeamPlayerNos(playerNo).ToList();

            foreach(var target in targets)
            {
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = target,
                    ParticleType = Defines.ParticleType.MovePlayer
                });
                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = target,
                    PlayerCondition = PlayerCondition
                });
            }

            return UniTask.CompletedTask;
        }

        public override bool SelectAuto(DuelData duelData, ActionItem actionItem, bool bUseForce)
        {
            // 対象は不要なのでチェック無し
            if (bUseForce) return true;

            // 基本としてはダメージを避ける為の思考とした。積極的に設置をする思考もありうる
            // TODO ダメージを受けないなら使用、としているが、ダメージを減らす場合でも使用させたい
            var playerNo = actionItem.GetPlayerNo();
            var player = duelData.Players[playerNo];

            Func<int, bool> isSafe = dice => duelData
                    .GetMovableAreaNos(player, dice)
                    .Any(areaNo => 0 == duelData.CalcSetAttackTotalDamageByOtherTeams(playerNo, areaNo));

            // 既に同じ状態異常を持っている場合
            var pc = player.ConditionList.GetItem<Duel.PlayerCondition.PlayerConditionDetailFixDice>();
            if (pc != null)
            {
                // 目が同じなら使う必要がない
                if (pc.Value == PlayerCondition.Value) return false;

                // 違う目の場合、移動先でダメージを受けないならそのまま
                if (isSafe(pc.Value)) return false;
            }

            // ダイス目によってはダメージを受ける可能性がある場合
            if (duelData.QueryDices(playerNo)
                    .Any(dice => duelData
                        .GetMovableAreaNos(player, dice)
                        .All(areaNo => 0 < duelData.CalcSetAttackTotalDamageByOtherTeams(playerNo, areaNo))))
            {
                // 移動先でダメージを受けないなら使用
                if (isSafe(PlayerCondition.Value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
