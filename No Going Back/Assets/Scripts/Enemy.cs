using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject target;
    Animator anim;
    float distanceToTarget;
    float chaseRange;
    float attackRange;
    float attackRate;
    float attackTime;
    float damage;
    float health;

	void Start ()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        chaseRange = 20f;
        attackRange = 6f;
        attackTime = 0;
        attackRate = 1.5f;
        damage = 10;
        health = 100f;
	}
	
	void Update ()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        //print(distanceToTarget);
        if(distanceToTarget <= chaseRange)
        {
            Chase();
        }
        else
        {
            anim.SetBool("Chase", false);
            //anim.SetBool("Idle", true);
        }
	}

    void Chase()
    {
        if (distanceToTarget <= attackRange)
        {
            target.GetComponent<Combat>().closestEnemy = gameObject;
            if (Time.time >= attackTime)
            {
                Attack();
            }
            else
            {
                anim.SetBool("Chase", false);
            }
        }
        else
        {
            anim.SetBool("Chase", true);
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            transform.position += transform.forward * 0.1f;
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        Invoke("DealDamage", 0.5f);
        attackTime = Time.time + attackRate;
    }

    void DealDamage()
    {
        target.GetComponent<Combat>().TakeDamage(damage);
    }

    public void TakeDamage()
    {
        health -= 20f;
    }
}
