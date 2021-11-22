using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable PossibleNullReferenceException

namespace NullFrameworkException.Teleport
{
    public class ReferenceTeleportPointCompare : Comparer<ReferenceTeleportPoint>
    {
        public override int Compare(ReferenceTeleportPoint _x, ReferenceTeleportPoint _y)
        {
            
            if(_x.distanceAwayFromPlayer > _y.distanceAwayFromPlayer)
                return -1;
            
            if(_x.distanceAwayFromPlayer < _y.distanceAwayFromPlayer)
                return 1;
            
            return 0;
            
        }
    }
}