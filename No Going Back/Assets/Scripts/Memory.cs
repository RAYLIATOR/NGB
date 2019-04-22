using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    GameObject target;
    bool attack;
    float distanceToTarget;

	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Invoke("Attack", 5);
	}
	
	void Update ()
    {
        if(attack)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            print(distanceToTarget);
            //Vector3.MoveTowards(transform.position, target.transform.position, 1);
            transform.LookAt(Camera.main.transform);
            transform.position += transform.forward * 0.5f;
            if(distanceToTarget <= 3f)
            {
                PlayerLook.freezeLook = false;
                PlayerMove.freezeMove = false;
                Destroy(gameObject);
            }
        }
	}

    void Attack()
    {
        attack = true;
    }
}
