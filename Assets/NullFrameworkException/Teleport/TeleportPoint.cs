using System;
using UnityEngine;

namespace NullFrameworkException.Teleport
{
    [System.Serializable]
    /// <summary> The point the player can teleport to. </summary>
    public class TeleportPoint : MonoBehaviour, IComparable<TeleportPoint>
    {
        /// <summary> How far away is the player from this point </summary>
        [SerializeField] private string pointName;
        /// <summary> Lambda to pointName </summary>
        public string PointName => pointName;
        /// <summary> The position of the player. </summary>
        public Vector3 Position => transform.position;
        /// <summary> The rotation of the player. </summary>
        public Quaternion Rotation => transform.rotation;
        
        
        // Implement this OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }

        public int CompareTo(TeleportPoint other) => throw new NotImplementedException();
    }
}