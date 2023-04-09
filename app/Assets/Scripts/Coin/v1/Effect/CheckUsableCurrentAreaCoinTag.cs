using Assets.Scripts.Duel;
using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Effect
{
    class CheckUsableCurrentAreaCoinTag : Assets.Scripts.Coin.Effect.Core, Scripts.Coin.Effect.IEffectUsable
    {
        public CheckUsableCurrentAreaCoinTag(Defines.CoinTag coinTag, int num)
        {
            this.coinTag = coinTag;
            this.num = num;
        }

        readonly Defines.CoinTag coinTag;
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

                string tagname = Defines.GetLocalizedString(coinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(tagname), tagname },
                    { nameof(num), Math.Abs(num).ToString() },
                };

                return GetEffectLocalizedString(nameof(CheckUsableCurrentAreaCoinTag), formatType, param);
            }
        }

        public bool IsUsable(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            if (!duelData.IsNoNeed())
            {
                var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
                var player = duelData.Players[playerNo];
                int count = duelData.FieldData
                            .AreaDatas[player.CurrentAreaNo]
                            .GetCoinsByOwner(playerNo)
                            .Where(scd => scd.GetCoinTag().HasFlag(coinTag))
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
