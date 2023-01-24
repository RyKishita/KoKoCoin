using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class ChangeAreaType : Coin
    {
        public ChangeAreaType(Defines.AreaType areaType)
        {
            this.areaType = areaType;

            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append("v1.ChangeAreaType");
                sb.Append('.');
                sb.Append(areaType);
                Name = sb.ToString();
            }
            Bodies = new Scripts.Coin.Body.Core[] { new Body.ChangeAreaType(Name, areaType) };
        }

        readonly Defines.AreaType areaType;

        public override string Name { get; }

        public override Scripts.Coin.Body.Core[] Bodies { get; }

        public override string PrefabName
        {
            get
            {
                using (var sb = ZString.CreateStringBuilder())
                {
                    sb.Append("ChangeAreaType");
                    sb.Append(areaType);
                    return sb.ToString();
                }
            }
        }
    }
}
