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
        public ReferenceTeleportPoint[] referenceTeleportPoints;
        
        /// <summary> Yes or No if the player is to teleport on Keypress </summary>
        public bool teleportOnKeypress;
        
        [Header("Keybind")]
        public KeyCode teleportClosestKey = KeyCode.Alpha1;
        
        // Start is called before the first frame update 
        void Start()
        {
            myRigidbody = GetComponentInChildren<Rigidbody>();
            // Repeats the change every second so that you see the changes.
            InvokeRepeating(nameof(GetAndSortPoints),0,0.1f);
        }

        /// <summary> This will get and sort all the TeleportPoints in the scene. </summary>
        private void GetAndSortPoints()
        {
            // Gets all the points in Scene.
            TeleportPoint[] teleportPoints = FindObjectsOfType<TeleportPoint>();
            // Resets the list of point.
            referenceTeleportPoints = new ReferenceTeleportPoint[teleportPoints.Length];
            
            // Save all these points a serializable class.
            for(int i = 0; i < teleportPoints.Length; i++)
            {
                referenceTeleportPoints[i] = new ReferenceTeleportPoint
                {
                    thisTeleportPoint = null, pointName = null, distanceAwayFromPlayer = 0
                };
                
                // Saves the reference point.
                referenceTeleportPoints[i].thisTeleportPoint = teleportPoints[i];
                // Update point name.
                referenceTeleportPoints[i].pointName = teleportPoints[i].PointName;
                // Shows distance from Player.
                referenceTeleportPoints[i].distanceAwayFromPlayer = Vector3.Distance(teleportPoints[i].Position, transform.position);
            }
            
            // If there are any points quick sort them.
            if (referenceTeleportPoints.Length > 0)
                SortPoints(referenceTeleportPoints);
            else
                Debug.Log("No Points to Sort");
        }

        /// <summary> This will sort the reference points. </summary>
        /// <param name="_referenceTeleportPoints"> Reference to the Point Array to be sorted. </param>
        private void SortPoints(ReferenceTeleportPoint[] _referenceTeleportPoints)
        {
            QuicksortPoints(_referenceTeleportPoints, 0, _referenceTeleportPoints.Length - 1);
        }

        /// <summary> A Quicksort of the implemented points. </summary>
        /// <param name="_referenceTeleportPoints"> Array to sort. </param>
        /// <param name="_left"> Left most element index. </param>
        /// <param name="_right"> Right most element index. </param>
        private void QuicksortPoints(ReferenceTeleportPoint[] _referenceTeleportPoints, int _left, int _right)
        {
            int i = _left;
            int j = _right;
            
            ReferenceTeleportPoint pivot = _referenceTeleportPoints[(_left + _right) / 2];

            while(i <= j)
            {
                while(_referenceTeleportPoints[i].distanceAwayFromPlayer < pivot.distanceAwayFromPlayer)
                {
                    i++;
                }

                while(_referenceTeleportPoints[j].distanceAwayFromPlayer > pivot.distanceAwayFromPlayer)
                {
                    j--;
                }

                if(i <= j)
                {
                    ReferenceTeleportPoint tmp = _referenceTeleportPoints[i];
                    _referenceTeleportPoints[i] = _referenceTeleportPoints[j];
                    _referenceTeleportPoints[j] = tmp;

                    i++;
                    j--;
                }
            }
            
            if (_left < j)
                QuicksortPoints(_referenceTeleportPoints, _left, j);
                
            if (i < _right)
                QuicksortPoints(_referenceTeleportPoints, i, _right);
        }

        // Update is called once per frame, but you probably already knew that.
        void Update()
        {
            if(Input.GetKeyDown(teleportClosestKey) && teleportOnKeypress)
            {
                TeleportToClosestPoint();
            }
        }

        /// <summary> This will telepoint to the closest point.  </summary>
        public void TeleportToClosestPoint()
        {
            if(referenceTeleportPoints.Length > 0)
            {
                // Gets all the points in scene and teleports to the nearest.
                GetAndSortPoints();

                // Gets the closest point using a linear search.
                // This should all be in order so should not matter anyway
                TeleportPoint closestPoint = LinearSearchToPoint();

                TeleportToPoint(closestPoint);
            }
            else
            {
                Debug.LogWarning("No where to teleport");
            }
        }

        /// <summary> Teleport this player to the input point. </summary>
        /// <param name="_pointToTeleportTo"> The TeleportPoint for the player to teleport to. </param>
        public void TeleportToPoint(TeleportPoint _pointToTeleportTo)
        {
            // Move the gameobject with the Rigidbody on it.
            this.transform.position = _pointToTeleportTo.Position;
            this.transform.rotation = _pointToTeleportTo.Rotation;
            
            // Reset the Rigidbody.
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = Vector3.zero;
        }

        /// <summary> A simple Linear Search. </summary>
        /// <returns> Returns the closest TeleportPoint. </returns>
        private TeleportPoint LinearSearchToPoint()
        {
            ReferenceTeleportPoint closestPoint = referenceTeleportPoints[0]; 
            for(int i = 0; i < referenceTeleportPoints.Length; i++)
            {
                if(referenceTeleportPoints[i].distanceAwayFromPlayer < closestPoint.distanceAwayFromPlayer)
                    closestPoint = referenceTeleportPoints[i];
            }
            return closestPoint.thisTeleportPoint;
        }
    }
    
    [System.Serializable]
    public class ReferenceTeleportPoint
    {
        [HideInInspector] public TeleportPoint thisTeleportPoint;
        /// <summary> Name of the point. </summary>
        [ReadOnly] public string pointName;
        /// <summary> How far away is the player from this point. </summary>
        [ReadOnly] public float distanceAwayFromPlayer;
    }
}