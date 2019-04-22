using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggers : MonoBehaviour
{
    PlayerFocus playerFocus;
    public GameObject memoryPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.tag == "LookAtShip")
            {
                PlayerLook.freezeLook = true;
                PlayerMove.freezeMove = true;
                playerFocus.LookAtShip();
                Destroy(gameObject);
            }
            if (gameObject.tag == "MemoryTrigger")
            {
                PlayerLook.freezeLook = true;
                PlayerMove.freezeMove = true;
                other.transform.LookAt(transform);
                Camera.main.transform.localEulerAngles = new Vector3(14, 0, 0);
                Memory();
                Destroy(gameObject);
            }
            if (gameObject.tag == "Zone")
            {
                other.GetComponent<Crafting>().inZone = true;
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.tag == "Zone")
            {
                other.GetComponent<Crafting>().inZone = false;
            }
        }
    }

    void Start ()
    {
        playerFocus = FindObjectOfType<PlayerFocus>();
    }
	
	void Update ()
    {
		
	}

    void Memory()
    {
        GameObject memory = Instantiate(memoryPrefab, transform.position, Quaternion.identity);
    }
}
