using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using MemoryPack;
using UniRx.Triggers;

namespace Assets.Scripts.Duel.PlayerCondition
{
    [MemoryPackable]
    public partial class PlayerConditionList : ICloneable, IEquatable<PlayerConditionList>, IEqualityComparer<PlayerConditionList>
    {
        [MemoryPackConstructor]
        public PlayerConditionList()
        {

        }

        public PlayerConditionList(PlayerConditionList src)
        {
            Items = src.Items.Select(item => item.Duplicate()).ToList();
        }

        #region serialize

        public List<PlayerCondition> Items = new List<PlayerCondition>();

        #endregion

        IEnumerable<PlayerConditionDetail> GetDetailItems() => Items.Select(item => item.GetDetail());

        IEnumerable<IPlayerConditionTriggerEvent> GetTriggerEventItems() => GetDetailItems().Where(item => item is IPlayerConditionTriggerEvent).Cast<IPlayerConditionTriggerEvent>();

        public int? InterceptDice(DuelManager duelManager, int targetPlayerNo, Player player)
        {
            foreach(var item in GetDetailItems()
            .Where(item => item is IPlayerConditionInterceptDice)
            .Cast<IPlayerConditionInterceptDice>())
            {
                var result = item.InterceptDice(duelManager, targetPlayerNo, player);
                if (result.HasValue) return result;
            }
            return null;
        }

        public bool InterceptDuelAction(DuelManager duelManager, Player player, DuelEvent.Action duelEventAction)
        {
            foreach(var item in GetDetailItems()
            .Where(item => item is IPlayerConditionInterceptDuelAction)
            .Cast<IPlayerConditionInterceptDuelAction>())
            {
                if (item.InterceptDuelAction(duelManager, player, duelEventAction)) return true;                
            }
            return false;
        }

        public bool InterceptDuelStep(DuelManager duelManager, int targetPlayerNo, Defines.DuelPhase gamePhase, Player player)
        {
            foreach(var item in GetDetailItems()
                                .Where(item => item is IPlayerConditionInterceptDuelStep)
                                .Cast<IPlayerConditionInterceptDuelStep>())
            {
                if (item.InterceptDuelStep(duelManager, targetPlayerNo, gamePhase, player)) return true;
            }
            return false;
        }

        public bool InterceptSelectCoin(DuelManager duelManager, int targetPlayerNo, Defines.CoinType selectCoinType, Player player)
        {
            foreach (var item in GetDetailItems()
                .Where(item => item is IPlayerConditionInterceptSelectCoin)
                .Cast<IPlayerConditionInterceptSelectCoin>())
            {
                if (item.InterceptSelectCoin(duelManager, targetPlayerNo, selectCoinType, player)) return true;
            }
            return false;
        }

        public bool InterceptMoveAuto(DuelManager duelManager, int targetPlayerNo, Player player)
        {
            foreach(var item in GetDetailItems()
                .Where(item => item is IPlayerConditionInterceptMoveAuto)
                .Cast<IPlayerConditionInterceptMoveAuto>())
            {
                if (item.InterceptMoveAuto(duelManager, targetPlayerNo, player)) return true;
            }
            return false;
        }

        public bool InterceptMoveWorst(DuelManager duelManager, int targetPlayerNo, Player player)
        {
            foreach (var item in GetDetailItems()
                .Where(item => item is IPlayerConditionInterceptMoveWorst)
                .Cast<IPlayerConditionInterceptMoveWorst>())
            {
                if (item.InterceptMoveWorst(duelManager, targetPlayerNo, player)) return true;
            }
            return false;
        }

        public void Regist(PlayerCondition status)
        {
            var findItem = Items.FirstOrDefault(item => status.IsMatch(item));
            if (null == findItem)
            {
                Items.Add(status.Duplicate());
            }
            else
            {
                findItem.Marge(status);
            }
        }

        public List<PlayerCondition> Cleanup()
        {
            var cleanUpedItems = Items.Where(status => status.Value == 0).ToList();
            Items.RemoveAll(status => cleanUpedItems.Contains(status));
            return cleanUpedItems;
        }

        public bool IsEmpty()
        {
            return !Items.Any();
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Remove(PlayerCondition playerCondition)
        {
            return Remove(playerCondition.InnerName);
        }

        public bool Remove(string playerConditionInnerName)
        {
            return 0 < Items.RemoveAll(item => item.InnerName == playerConditionInnerName);
        }

        public PlayerCondition GetItem<T>() where T : PlayerConditionDetail
        {
            return Items.Where(item => item.GetDetail() is T).FirstOrDefault();
        }

        public PlayerCondition GetItem(PlayerCondition playerCondition)
        {
            return GetItem(playerCondition.InnerName);
        }

        public PlayerCondition GetItem(string playerConditionInnerName)
        {
            return Items.Where(item => item.InnerName == playerConditionInnerName).FirstOrDefault();
        }

        public bool Has<T>() where T : PlayerConditionDetail
        {
            return Items.Any(item => item.GetDetail() is T);
        }

        public bool Has(PlayerCondition playerCondition)
        {
            return Items.Any(item => item.Has(playerCondition));
        }

        public bool Has(string playerConditionInnerName)
        {
            return Items.Any(item => item.InnerName == playerConditionInnerName);
        }

        public bool IsEffectStatus<T>(DuelData duelData) where T : PlayerConditionDetail
        {
            var hasStatus = GetItem<T>();
            if (hasStatus == null) return false;
            return duelData.GetConditionCount() <= hasStatus.Value;
        }

        public IEnumerable<string> MakeNameWithCounts() => Items.Select(item => item.MakeNameWithCount());

        public IEnumerable<string> MakeExplainTexts() => Items.Select(item => item.GetDetail().MakeExplain());

        public IEnumerable<string> MakeExplainTextsInDuel(DuelData duelData) => Items.Select(item => item.MakeExplain(duelData));

        public object Clone()
        {
            return Duplicate();
        }

        public PlayerConditionList Duplicate()
        {
            return new PlayerConditionList(this);
        }

        public bool Equals(PlayerConditionList other)
        {
            if (other == null) return false;
            if (ReferenceEquals(other, this)) return true;
            if ((Items == null) != (other.Items == null)) return false;
            if (Items != null && other.Items != null && !Enumerable.SequenceEqual(Items, other.Items)) return false;
            return true;
        }

        public bool Equals(PlayerConditionList x, PlayerConditionList y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(PlayerConditionList obj)
        {
            return obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PlayerConditionList);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return ZString.Join(",", Items.Select(item => item.ToString()).ToList());
        }

        public async UniTask ReceiveEventAsync(DuelManager duelManager, Player player, DuelEvent.After duelEvent)
        {
            foreach (var item in GetTriggerEventItems().Where(item => item.IsReceiveEvent(duelManager.DuelData, player, duelEvent)))
            {
                await item.ReceiveEventAsync(duelManager, player, duelEvent);
            }
        }

        static Dictionary<string, PlayerConditionDetail> details = null;

        public static Dictionary<string, PlayerConditionDetail> Details
        {
            get
            {
                if (details == null)
                {
                    details = new Dictionary<string, PlayerConditionDetail>()
                    {
                        { nameof(PlayerConditionDetailAppendDirectAttack), new PlayerConditionDetailAppendDirectAttack() },
                        { nameof(PlayerConditionDetailAppendGuard), new PlayerConditionDetailAppendGuard() },
                        { nameof(PlayerConditionDetailAppendSetAttack), new PlayerConditionDetailAppendSetAttack() },
                        { nameof(PlayerConditionDetailConfusionMove), new PlayerConditionDetailConfusionMove() },
                        { nameof(PlayerConditionDetailCurse), new PlayerConditionDetailCurse() },
                        { nameof(PlayerConditionDetailElectric), new PlayerConditionDetailElectric() },
                        { nameof(PlayerConditionDetailFire), new PlayerConditionDetailFire() },
                        { nameof(PlayerConditionDetailFixDice), new PlayerConditionDetailFixDice() },
                        { nameof(PlayerConditionDetailGravity), new PlayerConditionDetailGravity() },
                        { nameof(PlayerConditionDetailPoison), new PlayerConditionDetailPoison() },
                        { nameof(PlayerConditionDetailSkipGuard), new PlayerConditionDetailSkipGuard() },
                        { nameof(PlayerConditionDetailVirus), new PlayerConditionDetailVirus() },

                        // 与えるコインが無い
                        //{ nameof(PlayerConditionDetailStop), new PlayerConditionDetailStop() },
                        //{ nameof(PlayerConditionDetailSkipSupport), new PlayerConditionDetailSkipSupport() },
                        //{ nameof(PlayerConditionDetailSkipDirectAttack), new PlayerConditionDetailSkipDirectAttack() },
                        //{ nameof(PlayerConditionDetailSkipMove), new PlayerConditionDetailSkipMove() },
                        //{ nameof(PlayerConditionDetailSkipSet), new PlayerConditionDetailSkipSet() },
                        //{ nameof(PlayerConditionDetailSkipDraw), new PlayerConditionDetailSkipDraw() },
                    };
                }
                return details;
            }
        }

        public static PlayerConditionDetail GetDetail(string innerName) => Details[innerName];

        public static IEnumerable<string> MakeDetailExplains() =>
            Details.Values
            .OrderBy(pcd => pcd.DisplayName)
            .Select(pcd =>
            {
                using (var sb = ZString.CreateStringBuilder())
                {
                    sb.Append('[');
                    sb.Append(pcd.DisplayName);
                    sb.Append(']');
                    sb.Append(pcd.MakeExplain());
                    return sb.ToString();
                }
            });

        public int GetChangeDice(DuelData duelData)
        {
            return Items.Sum(item => (item.GetDetail() as IPlayerConditionChangeDice)?.GetChangeDice(duelData, item) ?? 0);
        }
    }
}
