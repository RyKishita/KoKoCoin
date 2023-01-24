using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Body.Set.Environment
{
    /// <summary>
    /// 環境コインのうち、値が増減するもの
    /// </summary>
    public interface IEnvironmentValue : IEnvironment
    {
        /// <summary>
        /// 変化量
        /// </summary>
        int EnvironmentValue { get; }
    }
}
