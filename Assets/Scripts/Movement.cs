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
            Debug.Log("Jump !");
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
            Vector3 position = transform.position;
            physics.addForce(new Vector3(0, jumpForce, 0));
            transform.position = position;
            changeGrounded();
        }
    }
    void changeGrounded()
    {
        isGrounded = !isGrounded;
        physics.grounded();
    }
}
