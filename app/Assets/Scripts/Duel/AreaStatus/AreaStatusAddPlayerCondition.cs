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
    public partial class AreaStatusAddPlayerCondition : IAreaStatus
    {
        [MemoryPackConstructor]
        public AreaStatusAddPlayerCondition()
        {

        }
        public AreaStatusAddPlayerCondition(AreaStatusAddPlayerCondition src)
        {
            RegisteredPlayerNo = src.RegisteredPlayerNo;
            PlayerCondition = src.PlayerCondition.Duplicate();
        }

        #region serialize

        public int RegisteredPlayerNo { get; set; }

        public PlayerCondition.PlayerCondition PlayerCondition;

        #endregion

        public string GetName() => MakeName(PlayerCondition?.GetDetail()?.DisplayName);

        public string GetNameExample() => MakeName("?");

        string MakeName(string condition)
        {
            var param = new Dictionary<string, string>()
            {
                { "condition", condition }
            };
            return Functions.GetLocalizedStringName(nameof(AreaStatusAddPlayerCondition), param);
        }

        public string MakeExplain()
        {
            var param = new Dictionary<string, string>()
            {
                { "condition", PlayerCondition?.MakeNameWithCount() ?? "?" }
            };
            return Functions.GetLocalizedString(nameof(AreaStatusAddPlayerCondition), param);
        }

        public bool IsMatch(IAreaStatus other)
        {
            return other is AreaStatusAddPlayerCondition;
        }

        public string GetExplainExample()
        {
            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append(MakeExplain());
                if (PlayerCondition != null)
                {
                    sb.Append("(");
                    sb.Append(PlayerCondition.GetDetail().MakeExplain());
                    sb.Append(")");
                }
                return sb.ToString();
            }
        }

        public string GetImageName()
        {
            string imageName;
            switch (PlayerCondition.InnerName)
            {
                case nameof(Duel.PlayerCondition.PlayerConditionDetailVirus): imageName = "AreaStatusVirus";break;
                default: throw new NotImplementedException();
            }
            return ZString.Format("Textures/Duel/{0}", imageName);
        }

        public void Marge(IAreaStatus other)
        {
            RegisteredPlayerNo = other.RegisteredPlayerNo;
            PlayerCondition = (other as AreaStatusAddPlayerCondition).PlayerCondition.Duplicate();
        }

        public IAreaStatus Duplicate()
        {
            return new AreaStatusAddPlayerCondition(this);
        }

        public UniTask ReceiveEventAsync(DuelManager duelManager, After after, AreaData areaData, int areaNo)
        {
            if (after is AfterMovePlayer afterMovePlayer &&
                afterMovePlayer.AreaNo == areaNo)
            {
                duelManager.RegistDuelEventAction(new ActionEffectPlayer()
                {
                    PlayerNo = afterMovePlayer.PlayerNo,
                    ParticleType = PlayerCondition.GetParticleType()
                });

                duelManager.RegistDuelEventAction(new ActionAddPlayerCondition()
                {
                    PlayerNo = afterMovePlayer.PlayerNo,
                    PlayerCondition = PlayerCondition
                });
            }
            return UniTask.CompletedTask;
        }
    }
}
