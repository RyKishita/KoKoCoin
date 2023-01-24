using Assets.Scripts.Duel;
using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Body.DirectAttack
{
    class RangeAny : IRange
    {
        public RangeAny(params int[] range)
        {
            Range = range;
        }

        public RangeAny(Defines.CoinTag coinTag)
        {
            Range = Defines.GetRange(coinTag).ToArray();
            if (Range.Length == 0) throw new ArgumentException(nameof(coinTag));
        }

        public int[] Range { get; protected set; }

        public IEnumerable<string> Explains
        {
            get
            {
                yield break;
            }
        }

        public bool IsInRange(DuelData duelData, int targetPlayerNo, SelectedCoinData selectedCoinData)
        {
            var playerNo = selectedCoinData.CoinData.OwnerPlayerNo;
            var player = duelData.Players[playerNo];

            var targetPlayer = duelData.Players[targetPlayerNo];

            int distance = player.CalcDistance(targetPlayer);

            return Range.Contains(distance);
        }

        public string RangeText
        {
            get
            {
                using (var sb = ZString.CreateStringBuilder())
                {
                    void appendRangeText(int s, int c)
                    {
                        if (0 < sb.Length)
                        {
                            sb.Append(',');
                        }

                        if (s == c)
                        {
                            sb.Append(s);
                        }
                        else
                        {
                            sb.Append(s);
                            sb.Append("～");
                            sb.Append(c);
                        }
                    }

                    bool bConsecutive = false;

                    int start = -1;
                    int current = -1;
                    foreach (int range in Range)
                    {
                        if (bConsecutive)
                        {
                            if (current + 1 < range)
                            {
                                appendRangeText(start, current);
                                start = range;
                            }
                        }
                        else
                        {
                            bConsecutive = true;
                            start = range;
                        }

                        current = range;
                    }

                    if (bConsecutive)
                    {
                        appendRangeText(start, current);
                    }

                    return sb.ToString();
                }
            }
        }
    }
}
