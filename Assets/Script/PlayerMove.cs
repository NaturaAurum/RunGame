using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jump;
    public float moveSpeed;

    private void Update()
    {

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            {

            GetComponent<Rigidbody2D>().velocity = new Vector3(/*속력벡터*/ GetComponent<Rigidbody2D>().velocity.x, jump);

            }
        
    }

    private void Move()
    {
        Vector3 moveVelocity = Vector3.right;

        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }

   
}
