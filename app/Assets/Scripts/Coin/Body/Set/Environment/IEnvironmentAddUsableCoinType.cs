using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Body.Set.Environment
{
    /// <summary>
    /// このコインがフィールドに存在する間、コイン使用ステップで指定のコイン種類が使用可能になる
    /// </summary>
    public interface IEnvironmentAddUsableCoinType : IEnvironment
    {
        /// <summary>
        /// ターン内のコイン使用可能ステップの内、何番目に追加するか。0始まり
        /// </summary>
        int AddUsableCoinTypeStepNo { get; }

        /// <summary>
        /// 追加するコイン種類
        /// </summary>
        Defines.CoinType AddUsableCoinType { get; }
    }
}
