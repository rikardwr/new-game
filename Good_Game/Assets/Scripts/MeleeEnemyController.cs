using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
	private CharacterController controller;
    Animator enemyAnimator; 
	private bool hasAnimator;

    // Start is called before the first frame update
    void Start()
    {
        target = MeleePlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

		controller = GetComponent<CharacterController>();
		hasAnimator = TryGetComponent(out enemyAnimator);

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

        if (distance <= lookRadius) {
            // Debug.Log("update() target: " + target);
            // Debug.Log("update()  agent: " + agent);
            // Debug.Log("meleeEnemyController.update() if() target: " + target);
            // Debug.Log("MeleeEnemyController.update() if ()");

            agent.SetDestination(target.position);
            // Debug.Log("target position: " + target.position);
            FaceTarget();

            if (distance <= agent.stoppingDistance) {
                // attack the target
                // face the target
                // Debug.Log("enemy-player distance: " + distance);
                FaceTarget();
                enemyAnimator.Play("Attack");
            }
        }
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
}
