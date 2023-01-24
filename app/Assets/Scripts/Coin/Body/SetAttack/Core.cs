using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;

namespace Assets.Scripts.Coin.Body.SetAttack
{
    public class Core : Set.Core
    {
        public Core(string coinName)
            : base(coinName)
        {

        }

        public override Defines.CoinType CoinType { get; } = Defines.CoinType.SetAttack;

        public override string Summary
        {
            get
            {
                using (var sb = ZString.CreateStringBuilder())
                {
                    sb.Append(base.Summary);
                    if (0 < SetAttackValue)
                    {
                        sb.Append(' ');
                        sb.Append(Defines.GetLocalizedString(Defines.StringEnum.Damage));
                        sb.Append(SetAttackValue);
                    }
                    return sb.ToString();
                }
            }
        }


        public int SetAttackValue { get; private set; }

        public Duel.DuelAnimation.SetAttack.SetAttackAnimation Animation { get; protected set; }


        public override IEnumerable<string> GetPrefabNames()
        {
            foreach (var prefabName in base.GetPrefabNames())
            {
                yield return prefabName;
            }
            if (Animation != null)
            {
                foreach (var prefabName in Animation.GetPrefabNames())
                {
                    yield return prefabName;
                }
            }
        }

        public int GetAppendValue(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return GetCoinEffects<Effect.IEffectAppendValue>().Sum(effect => effect.GetAppendValue(duelData, selectedCoinData, SetAttackValue));
        }

        public int GetAppendValuePrediction(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            return GetCoinEffects<Effect.IEffectAppendValuePrediction>().Sum(effect => effect.GetAppendValuePrediction(duelData, selectedCoinData, SetAttackValue));
        }

        public override async UniTask ReceiveEventAsync(DuelManager duelManager, SelectedCoinData selectedCoinData, Duel.DuelEvent.After after)
        {
            // 敵対プレイヤーが、このコインを設置したマスに止まった時
            if (IsOtherPlayerStop(duelManager, selectedCoinData, after))
            {
                var afterMovePlayer = after as Duel.DuelEvent.AfterMovePlayer;

                // コインが裏向きならまず表向きにする
                if (selectedCoinData.IsReverse)
                {
                    duelManager.RegistDuelEventAction(new Duel.DuelEvent.ActionChangeReverse()
                    {
                        TargetCoin = selectedCoinData,
                        IsReverseDst = false,
                        IsForce = true
                    });
                }

                // ダメージ強化エフェクト
                var pc = duelManager.DuelData
                            .Players[selectedCoinData.CoinData.OwnerPlayerNo]
                            .ConditionList
                            .GetItem<Duel.PlayerCondition.PlayerConditionDetailAppendSetAttack>();
                if (pc != null)
                {
                    duelManager.RegistDuelEventAction(new Duel.DuelEvent.ActionEffectSetCoin()
                    {
                        CoinIDs = new List<int>() { selectedCoinData.CoinData.ID },
                        ParticleType = 0 < pc.Value ? Defines.ParticleType.Buf : Defines.ParticleType.Debuf
                    });
                }

                // アニメーション
                duelManager.RegistDuelEventAction(new Duel.DuelEvent.ActionAnimateSetAttack()
                {
                    TargetPlayerNo = afterMovePlayer.PlayerNo,
                    DamageSource = selectedCoinData
                });

                // ダメージ
                int damage = selectedCoinData.GetCurrentTurnSetAttackDamage();

                duelManager.RegistDuelEventAction(new Duel.DuelEvent.ActionDamageCoin()
                {
                    ReasonPlayerNo = selectedCoinData.CoinData.OwnerPlayerNo,
                    DiffencePlayerNo = afterMovePlayer.PlayerNo,
                    DamageSource = selectedCoinData,
                    Damage = damage
                });

                // 捨てコインはベースクラス内
            }

            await base.ReceiveEventAsync(duelManager, selectedCoinData, after);
        }

        public override void SetData(CSVImport data)
        {
            base.SetData(data);

            SetAttackValue = data.value;
        }

        public override int GetValue() => SetAttackValue;
    }
}
