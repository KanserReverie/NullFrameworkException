using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.CharacterMovement.Rigidbody3d
{
    /// <summary> This is a basic character controller. </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementRigidbody3d : MonoBehaviour
    {
        /// <summary> The height of the player for the grounded check. </summary>
        [SerializeField, Tooltip("The height of the player collider")] private float playerHeight = 2f;

        [SerializeField] private Transform orientation; // ---!!!--- TO WORK OUT!!

        /// <summary> Speed the player moves at normally. </summary>
        [Header("Movement")] [SerializeField] float moveSpeed = 6f;
        /// <summary> Because we have 2 different drags we need a mutiplier for when in the air. </summary>
        [SerializeField] float airMultiplier = 0.4f;
        /// <summary> The internal movement mutiplier of the player so they move at a mormal speed. </summary>
        private float movementMultiplier = 10f;

        /// <summary> Normal walking speed the player moves at. </summary>
        [Header("Sprinting")] [SerializeField] float walkSpeed = 4f;
        /// <summary> The player sprinting speed. </summary>
        [SerializeField] float sprintSpeed = 6f;
        /// <summary> How fast the player comes to either of these speeds. </summary>
        [SerializeField] float acceleration = 10f;

        /// <summary> The jump force suddenly applied on the player when they jump. </summary>
        [Header("Jumping")] public float jumpForce = 5f;

        
        /// <summary> The keycode to jump. Can be changed in inspector. </summary>
        [Header("Keybinds")] [SerializeField] KeyCode jumpKey = KeyCode.Space;
        [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

        /// <summary> The player's physics drag on the ground. </summary>
        [Header("Drag")] [SerializeField] float groundDrag = 6f;
        /// <summary> The player's physics drag in the air. It needs to be larger for it to feel better. </summary>
        [SerializeField] float airDrag = 2f;


        [Header("Ground Detection")] 
        [SerializeField] Transform groundCheck; // ---!!!--- TO WORK OUT!!
        [SerializeField] LayerMask groundMask; // ---!!!--- TO WORK OUT!!
        [SerializeField] float groundDistance = 0.2f; // ---!!!--- TO WORK OUT!!
        public bool isGrounded { get; private set; } // ---!!!--- TO WORK OUT!!

        // PRIVATE VARIABLES.
        /// <summary> The current normalised direction the player is to move at. </summary>
        private Vector3 moveDirection;
        
        private Vector3 slopeMoveDirection; // ---!!!--- TO WORK OUT!!
        
        /// <summary> The current horizontal movement of the player. </summary>
        private float horizontalMovement;
        /// <summary> The current horizontal movement of the player. </summary>
        private float verticalMovement;
        /// <summary> The Rigidbody attached to the player. </summary>
        private Rigidbody rb;
        
        private RaycastHit slopeHit; // ---!!!--- TO WORK OUT!!

        private bool OnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
            {
                if(slopeHit.normal != Vector3.up)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void Start()
        {
            // Get the Rigidbody on the player.
            rb = GetComponent<Rigidbody>();
            // Make sure interpolation = True.
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.freezeRotation = true;
        }

        private void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            WASDMovementInput();
            ControlDrag();
            ControlSpeed();

            if(Input.GetKeyDown(jumpKey) && isGrounded)
            {
                Jump();
            }

            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        }

        /// <summary> This handles the input of all the WASD movement, Foward, Back, Left and Right. </summary>
        void WASDMovementInput()
        {
            // Returns:     1, for "d" key (right) || -1, for "a" key (left)  
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            // Returns:     1, for "w" key (forward) || -1, for "s" key (backward)
            verticalMovement = Input.GetAxisRaw("Vertical");
            // Move in the direction the player is looking.
            moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        }

        void Jump()
        {
            if(isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }

        void ControlSpeed()
        {
            if(Input.GetKey(sprintKey) && isGrounded)
            {
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            }
        }

        /// <summary> If the player is in the air they have less drag, this improves jump feel. </summary>
        void ControlDrag() => rb.drag = isGrounded ? groundDrag : airDrag;

        /// <summary> FixedUpdate has the frequency of the movement system.
        /// Since movement is physics based we will need to use this. </summary>
        private void FixedUpdate()
        {
            MovePlayer();
        }

        /// <summary> This is for the physics movement of the player. </summary>
        void MovePlayer()
        {
            // To note: you dont need to make "rb.AddForce" framerate independent.
            
            if(isGrounded && !OnSlope())
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else if(isGrounded && OnSlope())
            {
                rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else if(!isGrounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            }
        }
    }
}