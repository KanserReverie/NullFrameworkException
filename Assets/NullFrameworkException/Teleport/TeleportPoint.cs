using UnityEngine;

namespace NullFrameworkException.Teleport
{
    /// <summary> The point the player can teleport to. </summary>
    public class TeleportPoint : MonoBehaviour
    {
        /// <summary> The position of the player. </summary>
        public Vector3 Position => transform.position;
        /// <summary> The rotation of the player. </summary>
        public Quaternion Rotation => transform.rotation;

        public float DistanceAwayFromPlayer;

        // Implement this OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
        }
    }
}