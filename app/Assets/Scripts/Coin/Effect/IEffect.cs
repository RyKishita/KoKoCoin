using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Effect
{
    public interface IEffect
    {
        /// <summary>
        /// 説明文
        /// </summary>
        string Explain { get; }

        /// <summary>
        /// この効果に関係性があるコイン
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetCopiedCoinNames();

        /// <summary>
        /// この効果を持つコインの経過ターンが進んだ場合に、有利になるかどうか
        /// </summary>
        /// <param name="duelData"></param>
        /// <returns>true:有利 false:不利 null:どちらでもない</returns>
        bool? IsAdvantageProgressedTurn(Duel.DuelData duelData);

        /// <summary>
        /// フィールドに配置されている時に発揮する効果
        /// </summary>
        /// <returns></returns>
        bool IsOnAreaEffect();

        /// <summary>
        /// 有利な効果
        /// </summary>
        /// <param name="duelData"></param>
        /// <returns></returns>
        bool IsAdvantage(Duel.DuelData duelData);
    }
}
