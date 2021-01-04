using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jumpPower;
    public float moveSpeed;

    public bool isGrounded = false;

    public int MaxJumpCount = 2;

    public float GravityPower = -9.81f;

    [SerializeField] private float jumpCount = 0;

    [SerializeField] private float groundCheckDistance = 1f;

    public Vector2 moveVelocity = Vector2.right;

    private Rigidbody2D rig = null;

    private RaycastHit2D[] groundCastResult = new RaycastHit2D[2];

    private LayerMask groundCheckLayerMask = 0;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = 0;
        jumpCount = MaxJumpCount;

        groundCheckLayerMask = 1 << LayerMask.NameToLayer("Obstacle");
    }

    // 물리 계산은 FixedUpdate에서 해주는게 좋음
    // Update는 가변이다 보니 Fixed가 정확함.
    private void FixedUpdate()
    {
        // TODO : rigidbody2d 문제인지 Collision이 발생하면 velocity가 zero가 됨. 해당 현상을 고쳐야 Custom한 시스템을 만들 수 있음.
        var velocity = rig.velocity;
        // Update
        MoveUpdate(ref velocity);
        JumpUpdate(ref velocity);
        isGrounded = GroundCheck();
        if (!isGrounded)
            GravityUpdate(ref velocity);
        rig.velocity = velocity;
    }

    private void Update()
    {
        
    }

    private bool GroundCheck()
    {
        var isGround = Physics2D.RaycastNonAlloc(transform.position, Vector3.down, groundCastResult,
            groundCheckDistance,
            groundCheckLayerMask) > 0;

        if (isGrounded == false && isGround)
            jumpCount = MaxJumpCount;
        return isGround;
    }

    private void JumpUpdate(ref Vector2 velocity)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!(jumpCount > 0)) return;
            velocity.y = jumpPower;
            jumpCount--;
            Debug.Log("Jump!");
        }
    }

    private void GravityUpdate(ref Vector2 velocity)
    {
        var y = velocity.y;
        y += GravityPower * Time.fixedDeltaTime;
        velocity.y = y;
    }

    private void MoveUpdate(ref Vector2 velocity)
    {
        var nextVelocity = moveVelocity * moveSpeed;
        var currVel = velocity;
        currVel.x = nextVelocity.x;
        velocity = nextVelocity;
    }
}