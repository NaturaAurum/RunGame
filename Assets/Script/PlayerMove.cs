using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jumpPower;
    public float moveSpeed;

    public bool isGrounded = false;

    private float jumpCount = 0;

    public Vector3 moveVelocity = Vector3.right;

    private Rigidbody2D rig = null;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")//그라운드 
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        jumpCount = 0;
    }

    private void Update()
    {

        Move();
        if (isGrounded)
        {
            if (jumpCount < 2)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rig.velocity = new Vector3(rig.velocity.x, jumpPower);

                    jumpCount++;
                }
            }
        }
    }

    private void Move()
    {
        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }

   
}
