using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.CharacterMovement.Rigidbody3d
{
    public class MoveCameraRigidbody3d : MonoBehaviour
    {
        [SerializeField] Transform cameraPosition = null;

        void Update()
        {
            transform.position = cameraPosition.position;
        }
    }
}