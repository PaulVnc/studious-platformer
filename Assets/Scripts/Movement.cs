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

    public float wallJumpDuration = 0.3f;
    public float wallJumpCooldown = 0f;
     


    // Start is called before the first frame update
    void Awake()
    {
        physics = GetComponent<Physics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wallJumpCooldown <= 0)
        {
            moveHorizontally(Input.GetAxis("Horizontal"));
            if (Input.GetButtonDown("Jump"))
            {
                jump();
            }
        }
        else
        {
            wallJumpCooldown -= Time.deltaTime;
            if (wallJumpCooldown <= 0)
            {
                stopWallJump();
            }
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
                wallJumpCooldown = wallJumpDuration;
                physics.resetY();
                Debug.Log("WallJump to the right");
                stopWallColliding();
                physics.addForce(new Vector3(wallJumpHorizontalForce, wallJumpVerticalForce, 0));

            }
            if (!isGrounded && isCollidingRightWall)
            {
                wallJumpCooldown = wallJumpDuration;
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
    public void setCollideLeftWall(bool val)
    {
        if ((wallJumpCooldown + 0.1f < wallJumpDuration) && val)
        {
            if (wallJumpCooldown > 0f && wallJumpCooldown < wallJumpDuration)
            {
                stopWallJump();
                Debug.Log("Cooldown refreshed");
            }
            physics.wallBlockLeft();
        }

        isCollidingLeftWall = val;
    }
    public void setCollideRightWall(bool val)
    {
        if ((wallJumpCooldown + 0.1f < wallJumpDuration) && val)
        {
            if (wallJumpCooldown > 0f && wallJumpCooldown < wallJumpDuration)
            {
                stopWallJump();
                Debug.Log("Cooldown refreshed");
            }
            physics.wallBlockRight();
        }
        isCollidingRightWall = val;
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
    public void setGrounded(bool val)
    {
        isGrounded=val;
        if (val)
        {
            physics.grounded();
        }
    }
    private void stopWallJump()
    {
        wallJumpCooldown = 0;
        physics.speed.x *= 0.5f;
    }
}
