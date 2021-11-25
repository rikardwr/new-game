using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;  // Detection range for player

    Transform target;   // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent
    CharacterCombat combat;

    // Use this for initialization

    void Start()
    {
        target = GameMang.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }



   

    // Update is called once per frame
    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);
        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            // Move towards the target
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null)
                {
                    combat.Attack(targetStats);
                    Debug.Log(targetStats.ToString());

                }
                FaceTarget();
            }
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
