using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.Body.Set.Environment
{
    /// <summary>
    /// このコインがフィールドに存在する間、設置時に裏向き配置され、踏むまでコイン情報がわからないようになる
    /// </summary>
    public interface IEnvironmentReverseSet : IEnvironment
    {
    }
}
