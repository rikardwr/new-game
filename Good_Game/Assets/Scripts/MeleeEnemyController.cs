using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

[RequireComponent(typeof(NavMeshAgent))]
#endif

public class MeleeEnemyController : MonoBehaviour
{
    // public NavMeshAgent navMeshAgent;
    public float lookRadius = 10f;
	private float _speed = 10;
	private float _animationBlend;

    public static bool isAlive;
    public int maxHealth = 100;
    public static int currentEnemyHealth;
    public MeleeHealthBar enemyHealthBar;

    Transform target;
    NavMeshAgent agent;
	private CharacterController controller;
    Animator enemyAnimator; 
	private bool hasAnimator;


    private LevelSystem levelSystem;
    // Start is called before the first frame update
    void Start()
    {
        target = MeleePlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

		controller = GetComponent<CharacterController>();
		hasAnimator = TryGetComponent(out enemyAnimator);

        currentEnemyHealth = maxHealth;
        isAlive = true;
        enemyHealthBar.SetNewMaxHealth(currentEnemyHealth);
        // Debug.Log("MeleeEnemyController.start()");
        // Debug.Log("target: " + target);
        // Debug.Log("agent: " + agent);
        // enemyAnimator.Play("Jump1");

        // slow
        // GameObject.FindGameObjectWithTag("tagName");
    }

    // Update is called once per frame
    void Update()
    {
        float distance  = Vector3.Distance(target.position, transform.position);
        _animationBlend = agent.speed;
        float currentSpeed;

        if (distance <= lookRadius) {
            if (distance >= agent.stoppingDistance) {
                agent.speed = 2;
                _animationBlend = agent.speed;
                // Debug.Log("animationBlend" + _animationBlend);
                // Debug.Log("update() target: " + target);
                // Debug.Log("update()  agent: " + agent);
                // Debug.Log("meleeEnemyController.update() if() target: " + target);
                // Debug.Log("MeleeEnemyController.update() if ()");

                agent.SetDestination(target.position);
                // Debug.Log("target position: " + target.position);
                FaceTarget();
                Move();
            }

            if (distance <= agent.stoppingDistance) {
                // attack the target
                // face the target
                // Debug.Log("enemy-player distance: " + distance);
                agent.speed = 0;
                // Debug.Log("agent.speed: " + agent.speed);           
                FaceTarget();
                Move();
                enemyAnimator.Play("Attack");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
		// Debug.Log("meleeEnemyController.onCollision() with: " + collision.gameObject.tag);
		// Debug.Log(collision.gameObject.tag);
			// Debug.Log("collision with untagged");
		// Debug.Log(gameObject.tag);
        if (collision.gameObject.tag == "Player")
        // if (collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Damage_10")
        {
			// Debug.Log("collision with mySword");
			int damage = 26;
			OpponentTakeDamage(damage);
        }
        if (collision.gameObject.tag == "mySword") {
			// Debug.Log("collision with mySword");
			OpponentHealth.OpponentTakeDamage(34);
		}
        if (collision.gameObject.tag == "getHealth") {
			// Debug.Log("if(true) => getting max health");
			currentEnemyHealth = maxHealth;
        	enemyHealthBar.SetNewHealth(currentEnemyHealth);
		}
    }

    public void SetLevelSystem(LevelSystem levelSysten)
    {
        this.levelSystem = levelSysten;
    }
    /*
    public void Dead()
    {
        if (isAlive == true)
        {
            Debug.Log("The enemy is dead, you have getting xp");
            levelSystem.AddExperience(30);
        }
    }
    */
    public void OpponentTakeDamage(int damage)
    {
        // Debug.Log("EnemyMeleeController.OpponentTakeDamage() " + damage);
        enemyAnimator.Play("TakeDamage");
        currentEnemyHealth -= damage;
        enemyHealthBar.SetNewHealth(currentEnemyHealth);
        if (currentEnemyHealth <= 0)
        {
            isAlive = false;
			// pause_menu.Pause();
       	 	// meleeAnimator.SetTrigger("Die");
       	 	enemyAnimator.Play("Die");

            if(isAlive == false)
            {
                Debug.Log("Enemy diead");
                enemyAnimator.SetTrigger("Die");

                

                Destroy(gameObject,5);
            }

        }
        // Debug.Log("(meleeController) TakeDamage() " + currentHealth);
        // meleeAnimator.SetTrigger("takeDamage");
    }

    void FaceTarget () {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

	private void Move()
	{
		float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
        // Debug.Log("currentHorizontal speed: " + currentHorizontalSpeed);
		float speedOffset = 9f;
        
        // _speed = agent.speed;
        // _animationBlend = agent.speed;
        // Debug.Log("_animationBlend speed: " + _animationBlend);

        if (agent.speed > 1) {
            // Debug.Log("_animationBlend if: " + _animationBlend);
			enemyAnimator.SetFloat("Speed", _animationBlend);
        }
        else {
            // Debug.Log("_animationBlend else: " + _animationBlend);
			enemyAnimator.SetFloat("Speed", _animationBlend);
            // enemyAnimator.Play("motion");
        }

		// float speedOffset = 1f;
		// float speedOffset = 9f;

		// if (hasAnimator)
		// {
		// 	enemyAnimator.SetFloat("Speed", _animationBlend);
		// 	// animator.SetFloat("MoveSpeed",animIDMotionSpeed, inputMagnitude);
		// }
		// set target speed based on move speed, sprint speed and if sprint is pressed
		// float targetSpeed = input.sprint ? SprintSpeed : MoveSpeed;

		// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

		// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is no input, set the target speed to 0
		// if (input.move == Vector2.zero) targetSpeed = 0.0f;

		// a reference to the players current horizontal velocity
		// float inputMagnitude = input.analogMovement ? input.move.magnitude : 9f;

		// accelerate or decelerate to target speed
		// if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
		// {
		// 	// creates curved result rather than a linear one giving a more organic speed change
		// 	// note T in Lerp is clamped, so we don't need to clamp our speed
		// 	// _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

		// 	// round speed to 3 decimal places
		// 	_speed = Mathf.Round(_speed * 1000f) / 1000f;
		// }
		// else
		// {
		// 	_speed = targetSpeed;
		// }
		// _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

		// normalise input direction
		// Vector3 inputDirection = new Vector3(input.move.x, 0.0f, input.move.y).normalized;

		// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is a move input rotate player when the player is moving
		// if (input.move != Vector2.zero)
		// {
		// 	_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
		// 	float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

		// 	// rotate to face input direction relative to camera position
		// 	transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
		// }


		// Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

		// move the player
		// controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

		// update animator if using character
	}
}
