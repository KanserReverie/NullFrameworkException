using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.Teleport
{
    /// <summary> Please attach this to the player with a Rigidbody. </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerToTeleport : MonoBehaviour
    {
        // PRIVATE VARIABLES
        /// <summary> The rigidbody of the player. </summary>
        private Rigidbody myRigidbody;
        /// <summary> An array of all the points to teleport on the map.
        /// It would be cool if you could select the ones you wanted to tp to and which to disregard.
        /// Maybe to also have a name for each point. </summary>
        private TeleportPoint[] teleportPoints;
        
        [Header("Keybind")] 
        [SerializeField] KeyCode teleportClosestKey = KeyCode.Alpha1;
        
        // Start is called before the first frame update 
        void Start()
        {
            myRigidbody = GetComponentInChildren<Rigidbody>();
            GetAndSortPoints();
        }

        /// <summary> This will get and sort all the TeleportPoints in the scene. </summary>
        private void GetAndSortPoints()
        {
            teleportPoints = FindObjectsOfType<TeleportPoint>();
            // Quicksort these points by distance (floats).
        }

        // Update is called once per frame, but you probably already knew that.
        void Update()
        {
            if(Input.GetKeyDown(teleportClosestKey))
            {
                TeleportToClosestPoint();
            }
        }

        /// <summary> This will telepoint to the closest point.  </summary>
        private void TeleportToClosestPoint()
        {
            // Gets all the points in scene and teleports to the nearest.
            GetAndSortPoints();
            
            // Move the gameobject with the Rigidbody on it.
            if(teleportPoints.Length > 0)
            {
                this.transform.position = teleportPoints[0].Position;
                this.transform.rotation = teleportPoints[0].Rotation;
            }

            // Reset the Rigidbody.
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = Vector3.zero;
        }
    }
}