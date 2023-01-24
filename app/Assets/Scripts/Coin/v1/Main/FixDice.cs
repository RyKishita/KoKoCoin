using Cysharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Coin.v1.Main
{
    class FixDice : Coin
    {
        public FixDice(int dice)
        {
            this.dice = dice;

            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append("v1.FixDice");
                sb.Append('.');
                sb.Append(dice);
                Name = sb.ToString();
            }
            Bodies = new Scripts.Coin.Body.Core[] { new Body.FixDice(Name, true, dice) };
        }

        readonly int dice;

        public override string Name { get; }

        public override Scripts.Coin.Body.Core[] Bodies { get; }

        public override string PrefabName
        {
            get
            {
                using (var sb = ZString.CreateStringBuilder())
                {
                    sb.Append("FixDice");
                    sb.Append(dice);
                    return sb.ToString();
                }
            }
        }
    }
}
