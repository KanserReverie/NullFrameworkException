using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.Teleport
{
    /// <summary> The point the player can teleport to. </summary>
    public class TeleportPoint : MonoBehaviour
    {
        /// <summary> This is the name of the point. </summary>
        [SerializeField] private string pointName;
        /// <summary> The position of the player. </summary>
        public Vector3 Position => transform.position;
        /// <summary> The rotation of the player. </summary>
        public Quaternion Rotation => transform.rotation;

        // Simply a way to find any object marked as a waypoint

        // Implement this OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
    }
}