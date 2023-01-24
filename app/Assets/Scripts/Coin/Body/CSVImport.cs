using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Coin.Body
{
    [Serializable]
    public class CSVImport
    {
        public string name;
        public byte size;
        public string tag;
        public int value;

        public Defines.CoinTag GetCoinTag()
        {
            var coinTag = Defines.CoinTag.None;
            if (!string.IsNullOrEmpty(tag))
            {
                foreach (var ct in tag.Split('_').Select(t => Enum.TryParse<Defines.CoinTag>(t, out var tag) ? tag : Defines.CoinTag.None))
                {
                    coinTag |= ct;
                }
            }
            return coinTag;
        }
    }
}
