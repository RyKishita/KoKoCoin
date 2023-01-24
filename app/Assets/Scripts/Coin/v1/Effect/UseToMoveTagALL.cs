using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Coin.v1.Effect
{
    class UseToMoveTagALL : UseToMove
    {
        public UseToMoveTagALL(bool bCoinOwner, Defines.CoinPosition srcCoinPosition, Defines.CoinPosition dstCoinPosition, Defines.CoinTag coinTag)
            : base(dstCoinPosition)
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
                string dstcoinposition = Defines.GetLocalizedString(dstCoinPosition);

                var param = new Dictionary<string, string>()
                {
                    { nameof(srccoinposition), srccoinposition },
                    { nameof(dstcoinposition), dstcoinposition },
                    { nameof(tagname), tagname},
                };

                return GetEffectLocalizedString(nameof(UseToMoveTagALL), format, param);
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
