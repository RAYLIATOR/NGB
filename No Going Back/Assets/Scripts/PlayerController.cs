using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed;
    public float jumpForce;
    float jumpRayCastDistance;
    public static bool freezeMove;
    Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        speed = 0.3f;
        jumpForce = 15;
        jumpRayCastDistance = 5f;
	}
	
	void Update ()
    {
        Jump();
	}

    void FixedUpdate()
    {
        if (!Input.anyKey && !Input.anyKeyDown && isGrounded() && !freezeMove)
        {
            rb.velocity = Vector3.zero;
        }
        Move();
    }

    void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed;

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        if (!freezeMove)
        {
            rb.MovePosition(newPosition);
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded() && !freezeMove)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, jumpRayCastDistance);         
    }
}
