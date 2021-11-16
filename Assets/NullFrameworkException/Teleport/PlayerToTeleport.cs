using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.Teleport
{
    /// <summary> Please attach this to the player with a Rigidbody. </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerToTeleport : MonoBehaviour
    {
        private Rigidbody myRigidbody;
        private TeleportPoint[] teleportPoints;
        
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
            
            // Quicksort these points. 
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                TeleportToClosestPoint();
            }
        }

        /// <summary> This will telepoint to either the closest point.  </summary>
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