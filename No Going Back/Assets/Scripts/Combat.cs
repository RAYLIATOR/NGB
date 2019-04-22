using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Combat : MonoBehaviour
{
    float health;
    public GameObject closestEnemy;
    
    void Start ()
    {
        health = 100f;
	}
	
	void Update ()
    {
		
	}

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Attack()
    {

    }
}
