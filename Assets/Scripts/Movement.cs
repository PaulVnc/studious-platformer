using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isGrounded = true;
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
        if (Input.GetAxis("Jump")>0)
        {
            Debug.Log("Jump !");
            jump();
        }
    }
    void moveHorizontally(float direction)
    {
         Vector3 position = transform.position;
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

    //Can delete
    void changeGrounded()
    {
        isGrounded = !isGrounded;
        physics.grounded();
    }
}
