﻿using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Duel.PlayerCondition
{
    public interface IPlayerConditionInterceptMoveWorst
    {
        bool InterceptMoveWorst(DuelManager duelManager, int targetPlayerNo, Player player);
    }
}
