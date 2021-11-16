using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.CharacterMovement.Rigidbody3d
{
    public class WallRunRigidbody3d : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private Transform orientation;

        [Header("Detection")]
        [SerializeField] private float wallDistance = 0.6f;
        [SerializeField] private float minimumJumpHeight = 1.5f;

        [Header("Wall Running")]
        [SerializeField] private float wallRunGravity = 1f;
        [SerializeField] private float wallRunJumpForce = 6f;

        [Header("Camera")]
        [SerializeField] private Camera cammera;
        [SerializeField] private float fov = 90f;
        [SerializeField] private float wallRunfov = 100f;
        [SerializeField] private float wallRunfovTime = 10f;
        [SerializeField] private float camTilt = 10f;
        [SerializeField] private float camTiltTime = 10f;

        public float tilt { get; private set; }

        private bool wallLeft = false;
        private bool wallRight = false;

        RaycastHit leftWallHit;
        RaycastHit rightWallHit;

        private Rigidbody rb;

        bool CanWallRun()
        {
            return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void CheckWall()
        {
            wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
            wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
        }

        private void Update()
        {
            CheckWall();

            if (CanWallRun())
            {
                if (wallLeft)
                {
                    StartWallRun();
                }
                else if (wallRight)
                {
                    StartWallRun();
                }
                else
                {
                    StopWallRun();
                }
            }
            else
            {
                StopWallRun();
            }
        }

        void StartWallRun()
        {
            rb.useGravity = false;

            rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

            cammera.fieldOfView = Mathf.Lerp(cammera.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

            if (wallLeft)
                tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
            else if (wallRight)
                tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (wallLeft)
                {
                    Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
                }
                else if (wallRight)
                {
                    Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); 
                    rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
                }
            }
        }

        void StopWallRun()
        {
            rb.useGravity = true;

            cammera.fieldOfView = Mathf.Lerp(cammera.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
            tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
        }
    }
}