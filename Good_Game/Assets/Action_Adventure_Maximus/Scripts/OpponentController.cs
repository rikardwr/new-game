using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// #if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
// using UnityEngine.InputSystem;
// #endif

[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

[RequireComponent(typeof(PlayerInput))]
#endif
//[RequireComponent(typeof(Rigidbody))]
public class OpponentController : MonoBehaviour
{
	[Header("Player")]
	[Tooltip("Move speed of the character in m/s")]
	public float MoveSpeed = 6.0f;
	[Tooltip("Sprint speed of the character in m/s")]
	public float SprintSpeed = 3.0f;
	[Tooltip("How fast the character turns to face movement direction")]
	[Range(0.0f, 0.3f)]
	public float RotationSmoothTime = 0.12f;
	[Tooltip("Acceleration and deceleration")]
	public float SpeedChangeRate = 10.0f;

	[Space(10)]
	[Tooltip("The height the player can jump")]
	public float JumpHeight = 1.2f;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	public float Gravity = -15.0f;

	/* added this */
	[Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float JumpTimeout = 0.50f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float FallTimeout = 0.15f;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	public bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	public float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	public float GroundedRadius = 0.28f;
	[Tooltip("What layers the character uses as ground")]
	public LayerMask GroundLayers;
	/* added this */

	[Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float AttackTimeOut = 1f;

	[Header("Cinemachine")]
	[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
	public GameObject CinemachineCameraTarget;
	[Tooltip("How far in degrees can you move the camera up")]
	public float TopClamp = 70.0f;
	[Tooltip("How far in degrees can you move the camera down")]
	public float BottomClamp = -30.0f;
	[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
	public float CameraAngleOverride = 0.0f;
	[Tooltip("For locking the camera position on all axis")]
	public bool LockCameraPosition = false;

	// cinemachine
	private float _cinemachineTargetYaw;
	private float _cinemachineTargetPitch;

	// player
	private float _speed;
	private float _animationBlend;
	private float _targetRotation = 0.0f;
	private float _rotationVelocity;
	private float _verticalVelocity;
	private float _terminalVelocity = 53.0f;

	// playerHealth
    public static bool isAlive;
    public int maxHealth = 100;
    private static int currenOpponentHealth;
    public static HealthBar healthBar;

	// timeout deltatime
	private float _jumpTimeoutDelta;
	private float _attackTimeOutDelta;
	private float _fallTimeoutDelta;

	[Tooltip("player is blocking")] public bool blocking = false;
	[Tooltip("player is attacking")] public bool attacking = false;
	[Tooltip("player is jumping")] public bool jump = false;


	private Animator animatorOpponent;
	private CharacterController opponentController;
	private NewInput input;
	private GameObject mainCamera;
	private const float threshold = 0.01f;
	private bool hasAnimator;

	private void Awake()
	{
		// get a reference to our main camera
		if (mainCamera == null)
		{
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		hasAnimator = TryGetComponent(out animatorOpponent);
		opponentController = GetComponent<CharacterController>();
		input = GetComponent<NewInput>();
		//playerHealth
        currenOpponentHealth = maxHealth;
        isAlive = true;
        // healthBar.SetMaxHealth(currenOpponentHealth);

		// reset our timeouts on start
		_attackTimeOutDelta = AttackTimeOut;
		_jumpTimeoutDelta = JumpTimeout;
	}

	// Update is called once per frame
	void Update()
	{
		hasAnimator = TryGetComponent(out animatorOpponent);
		JumpAndGravity();
		input = GetComponent<NewInput>();
		animationAction();
		Move();
	}

	private void LateUpdate()
	{
		CameraRotation();
	}

	private void animationAction()
	{
		if (!attacking)
		{
			if (input.attack)
			{
				animatorOpponent.SetTrigger("Attack");
				input.attack = false;
			}
		}
		if (!blocking)
		{
			if (input.block)
			{
				animatorOpponent.SetTrigger("Attack2");
				input.block = false;
			}
		}
		if (!jump)
		{
			if (input.jump)
			{
				animatorOpponent.SetTrigger("Jump");
				input.jump = false;
			}
		}
		else
		{
			_attackTimeOutDelta -= Time.deltaTime;
			input.attack = false;
		}
		if (_attackTimeOutDelta <= 0)
		{
			_attackTimeOutDelta = AttackTimeOut;
			attacking = false;
		}
	}

	private void Move()
	{
		// set target speed based on move speed, sprint speed and if sprint is pressed
		float targetSpeed = input.sprint ? SprintSpeed : MoveSpeed;

		// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

		// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is no input, set the target speed to 0
		if (input.move == Vector2.zero) targetSpeed = 0.0f;

		// a reference to the players current horizontal velocity
		float currentHorizontalSpeed = new Vector3(opponentController.velocity.x, 0.0f, opponentController.velocity.z).magnitude;

		float speedOffset = 0.1f;
		float inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;

		// accelerate or decelerate to target speed
		if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
		{
			// creates curved result rather than a linear one giving a more organic speed change
			// note T in Lerp is clamped, so we don't need to clamp our speed
			_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

			// round speed to 3 decimal places
			_speed = Mathf.Round(_speed * 1000f) / 1000f;
		}
		else
		{
			_speed = targetSpeed;
		}
		_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

		// normalise input direction
		Vector3 inputDirection = new Vector3(input.move.x, 0.0f, input.move.y).normalized;

		// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is a move input rotate player when the player is moving
		if (input.move != Vector2.zero)
		{
			_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
			float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

			// rotate to face input direction relative to camera position
			transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		}


		Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

		// move the player
		opponentController.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

		// update animator if using character
		if (hasAnimator)
		{
			animatorOpponent.SetFloat("Speed", _animationBlend);
			/*animator.SetFloat("MoveSpeed",animIDMotionSpeed, inputMagnitude);*/
		}
	}


	private void CameraRotation()
	{
		// if there is an input and camera position is not fixed
		if (input.look.sqrMagnitude >= threshold && !LockCameraPosition)
		{
			_cinemachineTargetYaw += input.look.x * Time.deltaTime;
			_cinemachineTargetPitch += input.look.y * Time.deltaTime;
		}

		// clamp our rotations so our values are limited 360 degrees
		_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
		_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

		// Cinemachine will follow this target
		CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
	}

	private void JumpAndGravity()
	{
		if (Grounded)
		{
			Debug.Log("opponent is grounded");
			// reset the fall timeout timer
			_fallTimeoutDelta = FallTimeout;

			// update animator if using character
			/*
			if (hasAnimator)
			{
				animator.SetBool(animIDJump, false);
				_animator.SetBool(_animIDFreeFall, false);
			}
			*/

			// stop our velocity dropping infinitely when grounded
			if (_verticalVelocity < 0.0f)
			{
				_verticalVelocity = -9f;
			}

			// Jump
			if (input.jump && _jumpTimeoutDelta <= 0.0f)
			{
				// the square root of H * -2 * G = how much velocity needed to reach desired height
				_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				Debug.Log("input jump");
				// TakeDamage(24);
				input.jump = false;
			}

			// jump timeout
			if (_jumpTimeoutDelta >= 0.0f)
			{
				Debug.Log("jump timeout delta");
				_jumpTimeoutDelta -= Time.deltaTime;
			}
		}

		else
		{
			// reset the jump timeout timer
			_jumpTimeoutDelta = JumpTimeout;

			// fall timeout
			if (_fallTimeoutDelta >= 0.0f)
			{
				Debug.Log("is not grounded");
				_fallTimeoutDelta -= Time.deltaTime;
			}
	
			// if we are not grounded, do not jump
			input.jump = false;
		}

		// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		if (_verticalVelocity < _terminalVelocity)
		{
			_verticalVelocity += Gravity * Time.deltaTime;
		}
	}


	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

    public void TakeDamage(int damage)
    {
        currenOpponentHealth -= damage;
        // Debug.Log(currenOpponentHealth);
        Debug.Log("TakeDamage() " + currenOpponentHealth);
        animatorOpponent.SetTrigger("takeDamage");
        if (currenOpponentHealth <= 0)
        {
            Debug.Log("player died");
            isAlive = false;
       	 	animatorOpponent.SetTrigger("Die");
        }
        healthBar.SetHealth(currenOpponentHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
			// Debug.Log("collision with untagged");
        if (collision.gameObject.tag == "OpponentSword")
        // if (collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Damage_10")
        {
			int damage = 26;
			TakeDamage(damage);
			Debug.Log("collision with untagged");
            // Debug.Log(damage);
        }
        if (collision.gameObject.tag == "mySword") {
			OpponentHealth.OpponentTakeDamage(34);
		}
    }
}

