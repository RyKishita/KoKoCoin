using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin
{
    public class CoinList
    {
        private CoinList()
        {

        }

        public static CoinList Instance { get; private set; } = null;

        public IEnumerable<Coin> GetCoins(Defines.CoinVersion version = Defines.CoinVersion.All)
        {
            if (v1.CoinList.Instance.IsMatch(version))
            {
                foreach (var coin in v1.CoinList.Instance.Coins)
                {
                    yield return coin;
                }
            }

            if (version.HasFlag(Defines.CoinVersion.Common))
            {
                foreach (var coin in CommonCoinList.Instance.Coins)
                {
                    yield return coin;
                }
            }
        }

        public IEnumerable<Coin> PayCoins(Defines.CoinVersion version = Defines.CoinVersion.All)
        {
            return GetCoins(version).Where(coin => coin is not IExtraCoin);
        }

        public Coin GetPayCoin(string coinName, Defines.CoinVersion version = Defines.CoinVersion.All)
        {
            if (string.IsNullOrEmpty(coinName)) return null;
            return PayCoins(version).Where(coin => coin.Name == coinName).FirstOrDefault();
        }

        public Coin GetCoin(string coinName, Defines.CoinVersion version = Defines.CoinVersion.All)
        {
            if (string.IsNullOrEmpty(coinName)) return null;

            if (v1.CoinList.Instance.IsMatch(version) && v1.CoinList.Instance.TryGet(coinName, out Coin v1Coin)) return v1Coin;

            if (CommonCoinList.Instance.IsMatch(version) && CommonCoinList.Instance.TryGet(coinName, out Coin commonCoin)) return commonCoin;

            return null;
        }

        public bool IsExist(string coinName, Defines.CoinVersion version = Defines.CoinVersion.All)
        {
            if (string.IsNullOrEmpty(coinName)) return false;
            return GetCoins(version).Any(coin => coin.Name == coinName);
        }

        public byte GetCoinSize(string coinName, Defines.CoinVersion version = Defines.CoinVersion.All)
        {
            return GetCoin(coinName, version).Size;
        }

        public Defines.CoinVersion GetCoinVersion(string coinName)
        {
            if (v1.CoinList.Instance.Has(coinName)) return Defines.CoinVersion.v1;
            if (CommonCoinList.Instance.Has(coinName)) return Defines.CoinVersion.v1;
            return Defines.CoinVersion.None;
        }

        public IEnumerable<string> GetMainDeckCoinNames(UserData.v1.Deck saveDeck)
        {
            return GetMainDeckCoinNames(saveDeck.data);
        }

        public IEnumerable<string> GetMainDeckCoinNames(UserData.v1.DeckData saveDeckData)
        {
            return MakeCoinNames(saveDeckData.mainCoins);
        }

        public IEnumerable<string> GetSideDeckCoinNames(UserData.v1.Deck saveDeck)
        {
            return GetSideDeckCoinNames(saveDeck.data);
        }

        public IEnumerable<string> GetSideDeckCoinNames(UserData.v1.DeckData saveDeckData)
        {
            return MakeCoinNames(saveDeckData.sideCoins);
        }

        public IEnumerable<string> MakeCoinNames(IEnumerable<UserData.v1.CoinNum> coinNames)
        {
            return coinNames.Where(cn => IsExist(cn.name)).SelectMany(cn => Enumerable.Repeat(cn.name, cn.count));
        }

#if DEBUG && UNITY_EDITOR
        public void Check()
        {
            v1.CoinList.Instance.Check();
            CommonCoinList.Instance.Check();
        }
#endif
        public static IEnumerable<UniTask> LoadTasks(System.Threading.CancellationToken cancellationToken)
        {
            if (Instance != null) return Enumerable.Empty<UniTask>();
            Instance = new CoinList();
            return v1.CoinList.Instance.LoadTasks(cancellationToken)
                .Concat(CommonCoinList.Instance.LoadTasks(cancellationToken));
        }
    }
}
