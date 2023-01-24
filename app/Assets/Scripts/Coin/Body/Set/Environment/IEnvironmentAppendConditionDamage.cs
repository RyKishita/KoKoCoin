using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Body.Set.Environment
{
    /// <summary>
    /// ステータス異常によるダメージを上昇
    /// </summary>
    public interface IEnvironmentAppendConditionDamage : IEnvironmentValue
    {
        string PlayerConditionInnerName { get; }
    }
}
