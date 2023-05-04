using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin
{
    /// <summary>
    /// コインを所有しているとコイン一覧で表示されるが、関連するコインとして合わせて表示するもの
    /// </summary>
    interface IAdditionalShow
    {
        string[] AdditionalShowCoinNames { get; }
    }
}
