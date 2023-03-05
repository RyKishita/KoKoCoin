using System;
using System.Collections.Generic;
using Cysharp.Text;
using MemoryPack;

namespace Assets.Scripts.Duel.PlayerCondition
{
    [MemoryPackable]
    public partial class PlayerCondition : ICloneable, IEquatable<PlayerCondition>, IEqualityComparer<PlayerCondition>
    {
        [MemoryPackConstructor]
        public PlayerCondition()
        {

        }

        public PlayerCondition(string playerConditionInnerName, int value)
        {
            InnerName = playerConditionInnerName;
            Value = value;
        }

        public PlayerCondition(PlayerCondition src)
            : this(src.InnerName, src.Value)
        {
        }

        #region serialize

        public string InnerName;

        public int Value;

        #endregion

        public string GetDisplayName() => GetDetail().DisplayName;

        public string MakeNameWithCount()
        {
            return ZString.Format("{0} [{1}]", GetDisplayName(), Value);
        }

        public string MakeExplain(DuelData duelData)
        {
            return GetDetail().MakeExplain(duelData, this);
        }

        public void Marge(PlayerCondition other)
        {
            if (other.GetDetail() is PlayerConditionDetailFixDice)
            {
                Value = other.Value;
            }
            else
            {
                Value += other.Value;
            }
        }

        public bool IsMatch(PlayerCondition other)
        {
            return InnerName == other.InnerName;
        }

        public bool Has(PlayerCondition other)
        {
            return InnerName == other.InnerName && other.Value <= Value;
        }

        public object Clone()
        {
            return Duplicate();
        }

        public PlayerCondition Duplicate()
        {
            return new PlayerCondition(this);
        }

        public bool Equals(PlayerCondition other)
        {
            if (other == null) return false;
            if (ReferenceEquals(other, this)) return true;
            return InnerName == other.InnerName && Value == other.Value;
        }

        public bool Equals(PlayerCondition x, PlayerCondition y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(PlayerCondition obj)
        {
            return obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PlayerCondition);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append(InnerName);
                sb.Append('_');
                sb.Append(Value);
                return sb.ToString();
            }
        }

        public PlayerConditionDetail GetDetail()
        {
            return PlayerConditionList.GetDetail(InnerName);
        }

        public bool IsGood() => GetDetail().IsGood(this);

        public Defines.ParticleType GetParticleType() => GetDetail().GetParticleType(this);
    }
}
