using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isGrounded = true;
    public bool isCollidingLeftWall = false; //Boolean to change when colliding on a wall placed on the left
    public bool isCollidingRightWall = false; //Boolean to change when colliding on a wall placed on the right


    [Header("Player Speed")]
    [SerializeField] float playerInitialSpeed = 5f;
    [SerializeField] float playerMaxSpeed = 10f;
    [SerializeField] float playerIncrementSpeed = 5f;
    float playerCurrentSpeed = 0f;
    [Header("Player Air Movement")]
    [SerializeField] float playerIncrementSpeedAir = 2.5f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] float wallJumpHorizontalForce = 100f;
    [SerializeField] float wallJumpVerticalForce = 200f;
    public float wallJumpDuration = 0.3f;
    public float wallJumpCooldown = 0f;
    [Header("Player Dash")]
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float dashDuration = 0.3f;
    [SerializeField] float dashCooldown = 0.3f;
    float directionFacing = 1;
    public float dashTimer;
    public float dashCooldownTimer;
    float lastDashXSize;
    bool isEndDashing;
    private Physics physics;
    public bool isDashing;
    [Header("Level")] 
    [SerializeField] bool isOnLevel = true;
    
     


    // Start is called before the first frame update
    void Awake()
    {
        physics = GetComponent<Physics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        if (wallJumpCooldown <= 0)
        {
            if (!isDashing)
            {


                if (Input.GetButtonDown("Dash"))
                {
                    startDash();
                }
                float direction = Input.GetAxisRaw("Horizontal");
                moveHorizontally(Input.GetAxisRaw("Horizontal"));
                if (direction != 0f)
                {
                    directionFacing = Mathf.Sign(direction);
                }

                if (Input.GetButtonDown("Jump"))
                {
                    jump();
                }
            }
            else
            {
                dash();
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
        if (Mathf.Sign(direction) != Mathf.Sign(playerCurrentSpeed) && playerCurrentSpeed != 0f)
        {
            direction = 0;
        }
        if (direction != 0f)
        {
            if (playerCurrentSpeed == 0f)
            {
                playerCurrentSpeed = playerInitialSpeed * Mathf.Sign(direction);
            }
            if (playerCurrentSpeed < playerMaxSpeed)
            {
                float incr;
                if (isGrounded)
                {
                    incr = playerIncrementSpeed;
                }
                else
                {
                    incr = playerIncrementSpeedAir;
                }
                float newSpeed = playerCurrentSpeed + direction * Time.deltaTime * incr;
                if (Mathf.Abs(newSpeed) >= playerMaxSpeed)
                {
                    newSpeed = Mathf.Sign(newSpeed) * playerMaxSpeed;
                }
                playerCurrentSpeed = newSpeed;
            }
        }
        if (direction == 0f)
        {
            playerCurrentSpeed = 0f;
        }

        position.x += playerCurrentSpeed * Time.deltaTime;
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
        
        else
        {
            //Wall jump to the right
            if (!isGrounded && isCollidingLeftWall)
            {
                directionFacing = 1;
                wallJumpCooldown = wallJumpDuration;
                physics.resetY();
                Debug.Log("WallJump to the right");
                stopWallColliding();
                physics.addForce(new Vector3(wallJumpHorizontalForce, wallJumpVerticalForce, 0));

            }
            //Wall jump to the left
            if (!isGrounded && isCollidingRightWall)
            {
                directionFacing = -1;
                wallJumpCooldown = wallJumpDuration;
                physics.resetY();
                Debug.Log("WallJump to the left");
                stopWallColliding();
                physics.addForce(new Vector3(-wallJumpHorizontalForce, wallJumpVerticalForce, 0));

            }
        }
    }
    void startDash()
    {
        if (dashCooldownTimer <= 0)
        {
            Debug.Log("StartDash");
            dashTimer = dashDuration;
            isDashing = true;
        }


    }
    void dash()
    {
        if (dashTimer <= 0)
        {//End of dash
            endDash();
        }
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (!isCollidingLeftWall && !isCollidingRightWall)
            {
                Vector3 position = transform.position;
                lastDashXSize = dashSpeed * directionFacing * Time.deltaTime;
                position.x += lastDashXSize;
                transform.position = position;
            }
            else
            { //C'est plus un pansement c'est un platre
                transform.position = new Vector3(transform.position.x - lastDashXSize / 3, transform.position.y);


                endDash();
                moveHorizontally(directionFacing);
            }
        }
    }

    void endDash()
    {
        isDashing = false;
        dashCooldownTimer = dashCooldown;
        physics.speed.x = 0f;
        physics.speed.y = 0f;
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
