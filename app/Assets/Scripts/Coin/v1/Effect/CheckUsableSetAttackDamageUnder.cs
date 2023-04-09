using Assets.Scripts.Duel;
using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableSetAttackDamageUnder : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableSetAttackDamageUnder(bool bCoinOwner, int damage, int num)
        {
            if (damage <= 0) throw new ArgumentException("0は不可", nameof(damage));
            this.bCoinOwner = bCoinOwner;
            this.damage = damage;
            this.num = num;
        }

        readonly bool bCoinOwner;
        readonly int damage;
        readonly int num;

        public override string Explain
        {
            get
            {
                int formatType;
                if (0 < num)
                {
                    if (num == 1)
                    {
                        formatType = 0;
                    }
                    else
                    {
                        formatType = 1;
                    }
                }
                else if (num < 0)
                {
                    formatType = 2;
                }
                else
                {
                    formatType = 3;
                }
                if (!bCoinOwner)
                {
                    formatType += 4;
                }

                string setcoin = Defines.GetLocalizedString(Defines.StringEnum.SettedCoins);

                var param = new Dictionary<string, string>()
                {
                    { nameof(damage), Math.Abs(damage).ToString() },
                    { nameof(setcoin), setcoin},
                    { nameof(num), Math.Abs(num).ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableSetAttackDamageUnder), formatType, param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var playerNos = bCoinOwner
                                ? new List<int>() { selectedCoinData.CoinData.OwnerPlayerNo }
                                : duelData.GetOtherTeamPlayerNos(selectedCoinData.CoinData.OwnerPlayerNo).ToList();

                int count = duelData.FieldData
                            .GetAllAreaCoins()
                            .Where(scd => playerNos.Contains(scd.CoinData.OwnerPlayerNo))
                            .Where(scd => scd.GetSetAttackDamage(duelData) <= damage)
                            .Count();

                if (0 < num)
                {
                    if (count < num) return false;
                }
                else if (num < 0)
                {
                    if (Math.Abs(num) < count) return false;
                }
                else
                {
                    if (count != 0) return false;
                }
            }
            return true;
        }

        public override bool IsOnAreaEffect()
        {
            return false;
        }
    }
}
