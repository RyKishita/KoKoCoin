﻿using Cysharp.Text;
using Cysharp.Threading.Tasks;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.Coin
{
    abstract class VersionCoinList
    {
        protected abstract string VersionName { get; }

        protected void Regist(Coin coin)
        {
            if (coinTable.ContainsKey(coin.Name))
            {
                WriteLogWarning($"重複登録 {coin.Name}");
                return;
            }
            coinTable[coin.Name] = coin;
        }

        protected void Regist<T>() where T : Coin, new()
        {
            Regist(new T());
        }

        Dictionary<string, Coin> coinTable = new Dictionary<string, Coin>();

        public IEnumerable<Coin> Coins { get { return coinTable.Values; } }

        public Coin GetCoin(string name)
        {
            return coinTable[name];
        }

        public bool Has(string name)
        {
            return coinTable.ContainsKey(name);
        }

        public bool TryGet(string name, out Coin coin)
        {
            return coinTable.TryGetValue(name, out coin);
        }

        protected IEnumerable<Coin> GetCoins<T>()
        {
            return Coins.Where(coin => coin is T);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void Check()
        {
            WriteCoinInfos();
            CheckNames();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        void WriteCoinInfos()
        {
            string makeCoinNum(IEnumerable<Coin> coins, string name)
            {
                int size1 = coins.Count(coin => coin.Size == 1);
                int size2 = coins.Count(coin => coin.Size == 2);
                int size3 = coins.Count(coin => coin.Size == 3);
                return $"{coins.Count()} ({size1},{size2},{size3}) {name}";
            }

            WriteLog(string.Join("\n", new[]
            {
                makeCoinNum(Coins, "Coin"),
                makeCoinNum(Coins.Where(coin => coin.IsExistFromBodies<Body.SetAttack.Core>()), "SettedAttack"),
                makeCoinNum(Coins.Where(coin => coin.IsExistFromBodies<Body.DirectAttack.Core>()), "DirectAttack"),
                makeCoinNum(Coins.Where(coin => coin.IsExistFromBodies<Body.Support.Core>()), "Support"),
                makeCoinNum(Coins.Where(coin => coin.IsExistFromBodies<Body.Guard.Core>()), "Guard"),
            }));

            WriteLog(string.Join(",", Enum.GetValues(typeof(Defines.CoinTag)).Cast<Defines.CoinTag>().Select(coinTag => $"{coinTag}({Coins.Count(coin => coin.Bodies.Any(body => body.CoinTag.HasFlag(coinTag)))})")));

            using (var sb = ZString.CreateStringBuilder())
            {
                foreach (var coin in Coins.OrderBy(c => c.Yomigana))
                {
                    var values = coin.GetValues(true, true, true).ToList();
                    var value = values.Any() ? values.Max() : 0;
                    sb.AppendLine($"{coin.Name}\t{coin.DisplayName}\t{coin.CoinType}\t{coin.Size}\t{coin.Bodies.Max(body => body.Effects.Count())}\t{value}");
                }
                WriteLog(sb.ToString());
            }

            IEnumerable<string> makeDirectAttackRanges(int coinSize, int maxRange)
            {
                var directAttackCoins = Coins.Where(coin => coin.Size == coinSize).Where(coin => coin.IsExistFromBodies<Body.DirectAttack.Core>());
                var directAttacksAny = directAttackCoins.SelectMany(coin => coin.GetBodies<Body.DirectAttack.Core>().Where(directAttack => directAttack.Range is Body.DirectAttack.RangeAny)).ToList();
                foreach (var num in Enumerable.Range(0, maxRange + 1).Select(distance => directAttacksAny.Count(directAttack => (directAttack.Range as Body.DirectAttack.RangeAny).Range.Contains(distance))))
                {
                    yield return num.ToString();
                }
                yield return $"ALL{directAttackCoins.Count(coin => coin.GetBodies<Body.DirectAttack.Core>().Any(directAttack => directAttack.Range is Body.DirectAttack.RangeAll))}";
            }
            IEnumerable<string> makeDirectAttackTexts()
            {
                var ranges = Coins.SelectMany(coin => coin
                                    .GetBodies<Body.DirectAttack.Core>()
                                    .Where(directAttack => directAttack.Range is Body.DirectAttack.RangeAny)
                                    .SelectMany(directAttack => (directAttack.Range as Body.DirectAttack.RangeAny).Range)
                            )
                            .ToList();
                if (ranges.Any())
                {
                    int maxRange = ranges.Max();
                    return Enumerable.Range(1, 3).Select(coinSize => $"{coinSize}: {string.Join(" ", makeDirectAttackRanges(coinSize, maxRange))}");
                }
                else
                {
                    return Enumerable.Empty<string>();
                }
            }

            WriteLog(string.Join("\n", makeDirectAttackTexts()));

            using (var sb = ZString.CreateStringBuilder())
            {
                foreach (var coin in Coins.OrderBy(c => c.Yomigana))
                {
                    foreach (var directAttackCoin in coin.Bodies.Where(body => body is Body.DirectAttack.Core).Cast<Body.DirectAttack.Core>())
                    {
                        int size = coin.Size;
                        size += directAttackCoin.GetCoinEffects<Effect.IEffectNeedExtraCost>().Sum(ef => ef.Value);

                        int rangeValue = 0;
                        if (directAttackCoin.Range is Assets.Scripts.Coin.Body.DirectAttack.RangeAll)
                        {
                            rangeValue = 12;
                        }
                        else if (directAttackCoin.Range is Assets.Scripts.Coin.Body.DirectAttack.RangeAny directAttackAny)
                        {
                            rangeValue += directAttackAny.Range.Count();
                            rangeValue += Enumerable.Range(1, 2).Where(n => directAttackAny.Range.Contains(n)).Count();
                        }

                        sb.AppendLine($"{coin.Name}\t{size}\t{directAttackCoin.DirectAttackValue}\t{rangeValue}");
                    }
                }
                WriteLog(sb.ToString());
            }

            foreach (var coin in Coins.Where(coin=>coin.Size == 0))
            {
                WriteLogWarning($"コインサイズが0 {coin.Name}");
            }
        }

        // サーバー登録名とソース上の差異チェック
        [System.Diagnostics.Conditional("DEBUG")]
        void CheckNames()
        {
            if (!Utility.Functions.IsDisplayLanguageJapanese) return;

            var filePath = System.IO.Path.Combine(
                UnityEngine.Application.dataPath,
                $@"..\PluginSource\CheckCoinNames\CheckCoinNames\bin\Debug\net6.0\title-3AC1D-{VersionName}.csv");
            if (!System.IO.File.Exists(filePath)) return;

            var serverCoins = new Dictionary<string, string>();
            foreach (var line in System.IO.File.ReadAllLines(filePath))
            {
                if (string.IsNullOrEmpty(line)) continue;
                var values = line.Split(',');
                serverCoins.Add(values[0], values[1]);
            }

            foreach (var coin in Coins.Where(coin => !(coin is IExtraCoin)))
            {
                if (serverCoins.ContainsKey(coin.Name))
                {
                    if (coin.DisplayName != serverCoins[coin.Name])
                    {
                        WriteLogWarning($"表示名の不一致 {coin.Name} {coin.DisplayName} {serverCoins[coin.Name]}");
                    }
                }
                else
                {
                    WriteLogWarning($"名前がサーバー上で見つからない {coin.Name}");
                }
            }
            foreach (var serverCoin in serverCoins)
            {
                if (Coins.All(coin => coin.Name != serverCoin.Key))
                {
                    WriteLogWarning($"名前がクライアント上で見つからない {serverCoin.Key}");
                }
            }
            foreach (var coin in Coins.Where(coin => !(coin is IExtraCoin)))
            {
                var wrap = coin.DisplayNameWrap.Replace("\r", "").Replace("\n", "");
                if (coin.DisplayName != wrap)
                {
                    WriteLogWarning($"表示名と折り返し名が一致していない {coin.Name} {coin.DisplayName} {wrap}");
                }
            }
        }

        public abstract bool IsMatch(Defines.CoinVersion coinVersion);

        [System.Diagnostics.Conditional("DEBUG")]
        static void WriteLog(string message)
        {
            Utility.Functions.WriteLog(message);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        static void WriteLogWarning(string message)
        {
            Utility.Functions.WriteLog(UnityEngine.LogType.Warning, message);
        }

        static readonly object lockLoad = new object();

        public IEnumerable<UniTask> LoadTasks(System.Threading.CancellationToken cancellationToken)
        {
            return Enum.GetValues(typeof(Defines.CoinType))
                .Cast<Defines.CoinType>()
                .Select(async coinType =>
                {
                    Body.CSVImport[] items;

                    var key = ZString.Format("CoinValues/{0}.{1}", VersionName, coinType);
                    var handle = Addressables.LoadAssetAsync<UnityEngine.TextAsset>(key);
                    try
                    {
                        var text = (await handle.WithCancellation(cancellationToken))?.text;
                        if (string.IsNullOrWhiteSpace(text)) return;

                        items = CSVSerializer.Deserialize<Body.CSVImport>(text);
                    }
                    finally
                    {
                        Addressables.Release(handle);
                    }

                    lock (lockLoad)
                    {
                        foreach (var item in items)
                        {
                            var coin = Coins.FirstOrDefault(c => c.Name == item.name);
                            if (coin == null) continue;

                            var body = coin.Bodies.FirstOrDefault(b => b.CoinType == coinType);
                            if (body == null) continue;

                            // 複数Bodyを持つ場合はBody毎にサイズが異なる記述が出来てしまうので、最大値を採用
                            coin.Size = Math.Max(coin.Size, item.size);

                            body.SetData(item);
                        }
                    }
                });
        }
    }
}
