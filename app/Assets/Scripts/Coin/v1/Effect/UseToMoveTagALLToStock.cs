using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Duel;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToMoveTagALLToStock : UseToMove
    {
        public UseToMoveTagALLToStock(bool bCoinOwner, Defines.CoinPosition srcCoinPosition, Defines.CoinTag coinTag)
            : base(Defines.CoinPosition.Stock)
        {
            if (srcCoinPosition == dstCoinPosition) throw new ArgumentException();

            this.bCoinOwner = bCoinOwner;
            this.srcCoinPosition = srcCoinPosition;
            this.coinTag = coinTag;
        }

        readonly bool bCoinOwner;
        readonly Defines.CoinPosition srcCoinPosition;
        readonly Defines.CoinTag coinTag;

        public override string Explain
        {
            get
            {
                int format = bCoinOwner ? 0 : 1;
                string srccoinposition = Defines.GetLocalizedString(srcCoinPosition);
                string tagname = Defines.GetLocalizedString(coinTag);

                var param = new Dictionary<string, string>()
                {
                    { nameof(srccoinposition), srccoinposition },
                    { nameof(tagname), tagname},
                };

                return GetEffectLocalizedString(nameof(UseToMoveTagALLToStock), format, param);
            }
        }

        protected override IEnumerable<int> GetTargetCoins(DuelData duelData, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            if (srcCoinPosition == Defines.CoinPosition.Field)
            {
                return duelData
                        .FieldData
                        .GetAllAreaCoins()
                        .Where(scd => bCoinOwner == (scd.CoinData.OwnerPlayerNo == playerNo))
                        .Where(scd => scd.GetCoinTag().HasFlag(coinTag))
                        .Select(scd => scd.CoinData.ID);
            }
            else
            {
                if (bCoinOwner)
                {
                    return duelData.Players[playerNo]
                        .GetCoinDataList(srcCoinPosition)
                        .Items
                        .Where(cd => cd.HasCoinTag(coinTag))
                        .Select(cd => cd.ID);
                }
                else
                {
                    return duelData.Players
                        .Where(player => player.PlayerNo != playerNo)
                        .SelectMany(player => player.GetCoinDataList(srcCoinPosition).Items)
                        .Where(cd => cd.HasCoinTag(coinTag))
                        .Select(cd => cd.ID);
                }
            }
        }
    }
}
