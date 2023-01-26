using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Duel.DuelEvent;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using MemoryPack;

namespace Assets.Scripts.Duel.AreaStatus
{
    [MemoryPackable]
    public partial class AreaStatusAddResource : IAreaStatus
    {
        [MemoryPackConstructor]
        public AreaStatusAddResource()
        {

        }
        public AreaStatusAddResource(AreaStatusAddResource src)
        {
            RegisteredPlayerNo = src.RegisteredPlayerNo;
            Value = src.Value;
        }

        #region serialize

        public int RegisteredPlayerNo { get; set; }

        public int Value;

        #endregion

        public string GetName() => Functions.GetLocalizedStringName(nameof(AreaStatusAddResource));

        public string MakeExplain()
        {
            var param = new Dictionary<string, string>()
            {
                { "value", Value.ToString() }
            };
            return Functions.GetLocalizedString(nameof(AreaStatusAddResource), param);
        }

        public bool IsMatch(IAreaStatus other)
        {
            return other is AreaStatusAddResource;
        }

        public string GetNameExample() => GetName();

        public string GetExplainExample()
        {
            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append(MakeExplain());
                if (0 < Value)
                {
                    sb.Append("(");
                    sb.Append(Value);
                    sb.Append(")");
                }
                return sb.ToString();
            }
        }

        public string GetImageName()
        {
            return "Textures/Duel/AreaStatusAddResource";
        }

        public void Marge(IAreaStatus other)
        {
            RegisteredPlayerNo = other.RegisteredPlayerNo;
            Value = Math.Max(Value,(other as AreaStatusAddResource).Value);
        }

        public IAreaStatus Duplicate()
        {
            return new AreaStatusAddResource(this);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, After after, AreaData areaData, int areaNo)
        {
            if (after is AfterMovePlayer afterMovePlayer &&
                afterMovePlayer.AreaNo == areaNo)
            {
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = afterMovePlayer.PlayerNo,
                    ParticleType = Defines.ParticleType.AddResource
                });

                duelManager.RegistDuelEventAction(new ActionAddResource()
                {
                    PlayerNo = afterMovePlayer.PlayerNo,
                    Value = Value
                });
            }
            return UniTask.CompletedTask;
        }
    }
}
