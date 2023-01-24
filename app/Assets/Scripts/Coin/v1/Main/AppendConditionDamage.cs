using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class AppendConditionDamage : Coin
    {
        public AppendConditionDamage(string name, string playerConditionInnerName, int value)
        {
            bool bPlus = 0 < value;

            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append("v1.AppendConditionDamage");
                sb.Append('.');
                sb.Append(name);
                sb.Append('.');
                sb.Append(value);
                Name = sb.ToString();
            }
            Bodies = new Assets.Scripts.Coin.Body.Core[] { new Body.AppendConditionDamage(Name, playerConditionInnerName, value) };
            PrefabName = bPlus ? "AppendConditionDamageP" : "AppendConditionDamageM";
        }

        public override string Name { get; }

        public override Scripts.Coin.Body.Core[] Bodies { get; }

        public override string PrefabName { get; }

        public override float PositionY => -.4f;
        public override float ScaleCoinValue => .9f;
    }
}
