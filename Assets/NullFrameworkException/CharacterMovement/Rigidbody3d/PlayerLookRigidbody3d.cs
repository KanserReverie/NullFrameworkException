using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.CharacterMovement.Rigidbody3d
{
    /// <summary> Attach to the player. </summary>
    public class PlayerLookRigidbody3d : MonoBehaviour
    {
        [Header("References")] [SerializeField] WallRunRigidbody3d wallRun;

        /// <summary> Horizontal sensitivity. </summary>
        [SerializeField] private float sensX = 150f;
        /// <summary> Vertical sensitivity. </summary>
        [SerializeField] private float sensY = 150f;

        [SerializeField] private Transform cammeraHolder = null;
        [SerializeField] private Transform orientation = null;

        float mouseX;
        float mouseY;

        float multiplier = 0.01f;

        float xRotation;
        float yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            LookInput();

            // If you rotate on your 
            yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cammeraHolder.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        /// <summary> Mouse inputs of the player. </summary>
        private void LookInput()
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
        }
    }
}