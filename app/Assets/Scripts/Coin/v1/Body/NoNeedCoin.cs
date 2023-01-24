﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Body
{
    class NoNeedCoin : Scripts.Coin.Body.Set.Core, Scripts.Coin.Body.Set.Environment.IEnvironmentNoNeed
    {
        public NoNeedCoin(string coinName)
            : base(coinName)
        {

        }

        protected override IEnumerable<string> Explains
        {
            get
            {
                yield return GetLocalizedString(nameof(NoNeedCoin));

                foreach (var explain in base.Explains)
                {
                    yield return explain;
                }
            }
        }
    }
}
