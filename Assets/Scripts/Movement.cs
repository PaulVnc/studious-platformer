using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isGrounded = true;
    public bool isCollidingLeftWall = false; //Boolean to change when colliding on a wall placed on the left
    public bool isCollidingRightWall = false; //Boolean to change when colliding on a wall placed on the right
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] float wallJumpHorizontalForce = 100f;
    [SerializeField] float wallJumpVerticalForce = 200f;
    private Physics physics;
     


    // Start is called before the first frame update
    void Awake()
    {
        physics = GetComponent<Physics>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontally(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }
    }
    void moveHorizontally(float direction)
    {
        Vector3 position = transform.position;
        if (isCollidingLeftWall && direction <0)
        {
            direction = 0;
        }
        if (isCollidingRightWall && direction > 0)
        {
            direction = 0;
        }
        position.x += direction * playerSpeed * Time.deltaTime;
        transform.position = position;
    }

    void jump()
    {
        if (isGrounded)
        {
            Debug.Log("Jump");
            changeGrounded();
            physics.addForce(new Vector3(0, jumpForce, 0));
            
        }
        //Wall jump to the right
        else
        {
            if (!isGrounded && isCollidingLeftWall)
            {
                physics.resetY();
                Debug.Log("WallJump to the right");
                stopWallColliding();
                physics.addForce(new Vector3(wallJumpHorizontalForce, wallJumpVerticalForce, 0));

            }
            if (!isGrounded && isCollidingRightWall)
            {
                physics.resetY();
                Debug.Log("WallJump to the left");
                stopWallColliding();
                physics.addForce(new Vector3(-wallJumpHorizontalForce, wallJumpVerticalForce, 0));

            }
        }
    }
    void changeGrounded()
    {
        isGrounded = !isGrounded;
        if (isGrounded)
        {
            physics.grounded();
        }
    }
    public void collideLeftWall()
    {
        physics.wallBlock();
        isCollidingLeftWall = true;
    }
    public void collideRightWall()
    {
        physics.wallBlock();
        isCollidingLeftWall = true;
    }
    public void stopWallColliding()
    {
        isCollidingLeftWall=false;
        isCollidingRightWall=false;
    }
    public void collideCeiling()
    {
        physics.ceilingBlock();
    }
    public void collideGround()
    {
        isGrounded=true;
        physics.grounded();
    }
}
