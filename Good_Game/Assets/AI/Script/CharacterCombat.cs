using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackRate = 1f;
    public float attackSpeed = 1f;
    private float attackCoolDown = 0f;

    public float attackDelay = .6f;

    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats enemyStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();

    }

    private void Update()
    {
        attackCoolDown -= Time.deltaTime;

    }

    public void Attack (CharacterStats targetStats)
    {
        if(attackCoolDown <=0f)
        {
            this.enemyStats = targetStats;

            attackCoolDown = 1f / attackRate;
            StartCoroutine(DoDamage(targetStats, .6f));

            if (OnAttack != null) { OnAttack();}
        }
    }
    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        print("Start");
        yield return new WaitForSeconds(delay);
        Debug.Log(transform.name + "swings for " + myStats.damage.GetValue() + "damage");
        enemyStats.TakeDamage (myStats.damage.GetValue());
        //stats.TakeDamage(myStats.damage.GetValue());
            //print(myStats.damage.GetValue());
    }
}
