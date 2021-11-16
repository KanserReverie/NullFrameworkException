using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.CharacterMovement.Rigidbody3d
{
	/// <summary> This is a basic character controller. </summary>
	[RequireComponent(typeof(Rigidbody))] public class PlayerMovementRigidbody3d : MonoBehaviour
	{
		[Header("Other Player Details")]
		/// <summary> The height of the player for the grounded check. </summary>
		[SerializeField, Tooltip("The height of the player collider")]
		private float playerHeight = 2f;
		/// <summary> This is just an empty gameObject under player facing the same way as the player.
		/// This will probably be just, Position(0,0,0) & Rotation(0,0,0). </summary>
		[SerializeField, Tooltip("Make an empty child facing the direction of the player.")]
		private Transform orientation;

		/// <summary> Speed the player moves at normally. </summary>
		[Header("Movement")] [SerializeField] private float moveSpeed = 6f;
		/// <summary> Because we have 2 different drags we need a mutiplier for when in the air. </summary>
		[SerializeField] private float airMultiplier = 0.4f;

		/// <summary> Normal walking speed the player moves at. </summary>
		[Header("Sprinting")] [SerializeField] private float walkSpeed = 4f;
		/// <summary> The player sprinting speed. </summary>
		[SerializeField] float sprintSpeed = 9f;
		/// <summary> How fast the player comes to either of these speeds. </summary>
		[SerializeField] float acceleration = 10f;

		/// <summary> The jump force suddenly applied on the player when they jump. </summary>
		[Header("Jumping")] public float jumpForce = 17f;
		[SerializeField] private float fallMultiplier = 2;
		[SerializeField] private float lowJumpMultiplier = 2f;
		[SerializeField] private bool leftground = false;
		[SerializeField] private float rememberGroundedFor = 0.4f;

		/// <summary> The keycode to jump. Can be changed in inspector. </summary>
		[Header("Keybinds")] [SerializeField] KeyCode jumpKey = KeyCode.Space;
		[SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

		/// <summary> The player's physics drag on the ground. </summary>
		[Header("Drag")] [SerializeField] private float groundDrag = 6f;
		/// <summary> The player's physics drag in the air. It needs to be larger for it to feel better. </summary>
		[SerializeField] private float airDrag = 2f;

		/// <summary> This is just an empty gameObject under player facing the same way as the player.
		/// This will probably be just, Position(0,0,-1(-playerHeight/2)) & Rotation(0,0,0). </summary>
		[Header("Ground Detection")] [SerializeField, Tooltip("An empty child at Transform(0,0,(-playerHeight/2)) & Rotation(0,0,0)")]
		private Transform groundCheck;
		/// <summary> The layer the ground is on. </summary>
		[SerializeField, Tooltip("You might need to make a 'ground' layer.")]
		private LayerMask groundMask;
		/// <summary> How close player is to the ground to reset jump.</summary>
		[SerializeField, Tooltip("How close player is to the ground to reset jump.")]
		private float groundDistance = 0.3f;

		// HIDDEN VARIABLES.
		/// <summary> Method for if player is "Grounded" or not. </summary>
		public bool isGrounded { get; private set; }
		/// <summary> The current normalised direction the player is to move at. </summary>
		private Vector3 moveDirection;
		/// <summary> The current normalised slope direction the player is to move at. </summary>
		private Vector3 slopeMoveDirection;
		/// <summary> The current horizontal movement of the player. </summary>
		private float horizontalMovement;
		/// <summary> The current horizontal movement of the player. </summary>
		private float verticalMovement;
		/// <summary> The Rigidbody attached to the player. </summary>
		private Rigidbody myRigidbody;
		/// <summary> The internal movement mutiplier of the player so they move at a mormal speed. </summary>
		private float movementMultiplier = 10f;
		/// <summary> The raycast if we are on a slope. </summary>
		private RaycastHit slopeHit;
		/// <summary> If the player is jumping. </summary>
		private bool jumpingNow = false;
		/// <summary> Current time since the player was grounded (for if the player recently fell off). </summary>
		private float lastTimeGrounded;

		
		/// <summary> Check if we are on a slope. </summary>
		/// <returns> Returns true if on a slope. </returns>
		private bool OnSlope()
		{
			if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
			{
				return slopeHit.normal != Vector3.up;
			}

			return false;
		}

		private void Start()
		{
			// Get the Rigidbody on the player.
			myRigidbody = GetComponent<Rigidbody>();
			// Make sure interpolation = True.
			myRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			myRigidbody.freezeRotation = true;
		}

		private void Update()
		{
			// Check if the player is grounded.
			isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
			WASDMovementInput();
			ControlDrag();
			ControlSpeed();

			if(Input.GetKeyDown(jumpKey) && isGrounded)
			{
				Jump();
			}

			BetterJump();
			CheckIfGrounded();
			
			slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
		}

		private void CheckIfGrounded()
		{
			if(isGrounded && !jumpingNow)
			{
				leftground = false;
			}
			else
			{
				if(!leftground)
				{
					lastTimeGrounded = Time.time;
				}
				leftground = true;
				isGrounded = false;
			}
		}

		/// <summary> Press jump to jump. </summary>
		private void Jump()
		{
			// if(isGrounded)
			// {
			// 	myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0, myRigidbody.velocity.z);
			// 	myRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
			// }
			
			if((isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
			{
				myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0,myRigidbody.velocity.z);
				myRigidbody.AddForce(transform.up*jumpForce, ForceMode.Impulse);
				jumpingNow = true;
			}
		}
		
		/// <summary> This makes the Jumping feel better </summary>
		private void BetterJump()
		{
			if(myRigidbody.velocity.y < 0)
			{
				Vector2 HoriVerti = Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
				myRigidbody.velocity += new Vector3(HoriVerti.x, HoriVerti.y, 0);
				jumpingNow = false;
			}
			else if(myRigidbody.velocity.y > 0 && !Input.GetButtonDown("Jump"))
			{
				Vector2 HoriVerti2 = Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
				myRigidbody.velocity += new Vector3(HoriVerti2.x, HoriVerti2.y, 0);
			}
		}

		/// <summary> This handles the input of all the WASD movement, Foward, Back, Left and Right. </summary>
		private void WASDMovementInput()
		{
			// Returns:     1, for "d" key (right) || -1, for "a" key (left)  
			horizontalMovement = Input.GetAxisRaw("Horizontal");
			// Returns:     1, for "w" key (forward) || -1, for "s" key (backward)
			verticalMovement = Input.GetAxisRaw("Vertical");
			// Move in the direction the player is looking.
			moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
		}


		/// <summary> Control the changing acceleration of the player. </summary>
		private void ControlSpeed()
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
		private void ControlDrag() => myRigidbody.drag = isGrounded ? groundDrag : airDrag;

		/// <summary> FixedUpdate has the frequency of the movement system.
		/// Since movement is physics based we will need to use this. </summary>
		private void FixedUpdate()
		{
			MovePlayer();
		}

		/// <summary> This is for the physics movement of the player. </summary>
		private void MovePlayer()
		{
			// To note: you dont need to make "rb.AddForce" framerate independent.

			if((isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor) && !OnSlope())
			{
				myRigidbody.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
			}
			else if((isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor) && OnSlope())
			{
				myRigidbody.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
			}
			else if(!(isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
			{
				myRigidbody.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
			}
		}
	}
}