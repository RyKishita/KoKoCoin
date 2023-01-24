using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Assets.Scripts.Coin
{
    public abstract class Coin
    {
        public Coin()
        {

        }

        /// <summary>
        /// 内部名
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 表示コイン名
        /// </summary>
        public string DisplayName => LocalizationSettings.StringDatabase.GetLocalizedString("CoinName", Name);

        /// <summary>
        /// 表示コイン名。折り返し考慮
        /// </summary>
        public string DisplayNameWrap => LocalizationSettings.StringDatabase.GetLocalizedString("CoinNameWrap", Name);

        /// <summary>
        /// よみがな（並べ替えコイン名）
        /// </summary>
        public string Yomigana => LocalizationSettings.StringDatabase.GetLocalizedString("CoinNameYomigana", Name);

        /// <summary>
        /// コインサイズ
        /// </summary>
        public byte Size { get; set; }

        /// <summary>
        /// コイン実体部
        /// </summary>
        public virtual Body.Core[] Bodies { get; } = new Body.Core[] { };

        public int SortIndex => Defines.GetSortIndex(CoinType);

        public Defines.CoinType CoinType
        {
            get
            {
                Defines.CoinType result = Defines.CoinType.None;
                foreach (var coinBody in Bodies)
                {
                    result |= coinBody.CoinType;
                }
                return result;
            }
        }

        /// <summary>
        /// 要約(Body以外)
        /// </summary>
        public IEnumerable<string> Summaries
        {
            get
            {
                using (var sb = ZString.CreateStringBuilder())
                {
                    sb.Append(Defines.GetLocalizedString(Defines.StringEnum.Size));
                    sb.Append(Size);
                    yield return sb.ToString();
                }
            }
        }

        /// <summary>
        /// 要約(Body以外)
        /// </summary>
        public string Summary => ZString.Join(" ", Summaries.ToList());

        /// <summary>
        /// プレファブ名
        /// </summary>
        public abstract string PrefabName { get; }

        /// <summary>
        /// コインとして表示する際のモデル位置X(右が＋)
        /// </summary>
        public virtual float PositionX { get; } = 0f;

        /// <summary>
        /// コインとして表示する際のモデル位置Y(上が＋)
        /// </summary>
        public virtual float PositionY { get; } = -0.5f;//標準モデルは原点(0,0,0)・高さ1.0・z+方向を向いている

        /// <summary>
        /// コインとして表示する際のモデル位置Z(奥が＋)
        /// </summary>
        public virtual float PositionZ { get; } = 0f;

        /// <summary>
        /// コインとして表示する際のモデル位置
        /// </summary>
        public Vector3 Position => new Vector3(PositionX, PositionY, PositionZ);

        /// <summary>
        /// コインとして表示する際のモデル向きX
        /// </summary>
        public virtual float RotateX { get; } = 0f;

        /// <summary>
        /// コインとして表示する際のモデル向きY
        /// </summary>
        public virtual float RotateY { get; } = 180f;//標準モデルは原点(0,0,0)・高さ1.0・z+方向を向いている

        /// <summary>
        /// コインとして表示する際のモデル向きZ
        /// </summary>
        public virtual float RotateZ { get; } = 0f;

        /// <summary>
        /// コインとして表示する際のモデル向き
        /// </summary>
        public Quaternion Rotate => Quaternion.Euler(RotateX, RotateY, RotateZ);

        /// <summary>
        /// コインとして表示する際の大きさ倍率
        /// </summary>
        public virtual float ScaleCoinValue { get; } = 1f;

        /// <summary>
        /// コインとして表示する際の大きさ
        /// </summary>
        public Vector3 ScaleCoin => new Vector3(ScaleCoinValue, ScaleCoinValue, ScaleCoinValue);

        /// <summary>
        /// モデルとして表示する際の大きさ倍率
        /// </summary>
        public virtual float ScaleModelValue { get; } = 1f;

        public bool IsExistFromBodies<T>() where T : Body.Core
        {
            return Bodies.Any(body => body is T);
        }

        public IEnumerable<T> GetBodies<T>() where T : Body.Core
        {
            return Bodies.Where(body => body is T).Cast<T>();
        }

        public bool IsAllBodies<T>() where T : Body.Core
        {
            return Bodies.All(body => body is T);
        }

        public IEnumerable<string> GetPrefabNames()
        {
            foreach(var body in Bodies)
            {
                foreach(var prefabName in body.GetPrefabNames())
                {
                    yield return prefabName;
                }
            }
            yield return PrefabName;
        }

        public IEnumerable<string> GetCopiedCoinNames()
        {
            return Bodies.SelectMany(body => body.GetCopiedCoinNames());
        }

        public int GetEffectIndex(object effect)
        {
            return Bodies.SelectMany(body => body.Effects).ToList().FindIndex(e => object.ReferenceEquals(e, effect));
        }

        public IEnumerable<string> GetSearchTexts()
        {
            yield return DisplayName;
            yield return Yomigana;
            foreach(var summary in Summaries)
            {
                yield return summary;
            }
            foreach(var body in Bodies)
            {
                foreach(var text in body.GetSearchTexts())
                {
                    yield return text;
                }
            }
        }

        public bool HasCoinTag(Defines.CoinTag coinTag) => Bodies.Any(body => body.CoinTag.HasFlag(coinTag));

        public bool HasCoinType(Defines.CoinType coinType) => Bodies.Any(body => body.IsCoinType(coinType));

        public IEnumerable<int> GetValues(bool bSetAttack, bool bDirectAttack, bool bGuard)
        {
            foreach(var body in Bodies)
            {
                switch(body)
                {
                    case Body.SetAttack.Core setAttack:
                        if (bSetAttack)
                        {
                            yield return setAttack.SetAttackValue;
                        }
                        break;
                    case Body.DirectAttack.Core directAttack:
                        if (bDirectAttack)
                        {
                            yield return directAttack.DirectAttackValue;
                        }
                        break;
                    case Body.Guard.Core guard:
                        if (bGuard)
                        {
                            yield return guard.GuardValue;
                        }
                        break;
                }
            }
        }

        public bool? IsAdvantageEffect_ProgressedTurn(DuelData duelData)
        {
            int trueNum = Bodies.Count(body => body.IsAdvantageEffect_ProgressedTurn(duelData) == true);
            int falseNum = Bodies.Count(body => body.IsAdvantageEffect_ProgressedTurn(duelData) == false);
            if (trueNum == falseNum) return null;
            return falseNum < trueNum;
        }

        public IEnumerable<Defines.CoinType> GetCoinTypes()
        {
            return Bodies.Select(body => body.CoinType).Distinct();
        }
    }
}
