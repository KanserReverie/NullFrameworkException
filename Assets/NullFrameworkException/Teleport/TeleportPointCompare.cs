using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.Teleport
{
    public class TeleportPointCompare : Comparer<TeleportPoint>
    {
        public override int Compare(TeleportPoint _x, TeleportPoint _y)
        {
            if(_x.DistanceAwayFromPlayer > _y.DistanceAwayFromPlayer)
                return -1;
            if(_x.DistanceAwayFromPlayer < _y.DistanceAwayFromPlayer)
                return 1;

            return 0;
        }
    }
}