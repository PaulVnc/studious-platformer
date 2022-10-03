using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    [SerializeField] float gravity = 10f;
    public Vector3 constantAcceleration;
    public Vector3 acceleration;
    public Vector3 speed;
    private Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = Vector3.zero;
        speed = Vector3.zero;
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        physicUpdate();
    }

    void physicUpdate()
    {
        applyGravity();
        applyWallCollision();
        speed +=  acceleration * Time.fixedDeltaTime;
        Debug.Log(speed);
        transform.position += speed * Time.fixedDeltaTime;
        reduceSpeed();
        
        acceleration = Vector3.zero;
    }

    float reduce(float coordinateToReduce)
    {
        float newValue = coordinateToReduce;
        if(newValue > 0)
        {
            newValue -= Time.fixedDeltaTime;
            newValue = Mathf.Max(newValue, 0f);
        }
        if (newValue < 0)
        {
            newValue += Time.fixedDeltaTime;
            
            newValue = Mathf.Min(newValue, 0f);
        }
        return newValue;
    }
    void reduceSpeed()
    {
        speed.x = reduce(speed.x);
        speed.y = reduce(speed.y);
        speed.z = reduce(speed.z);
    }
    public void addForce(Vector3 force)
    {
        acceleration += force;
    }
    void applyGravity()
    {
        if (!movement.isGrounded)
        {
            addForce(new Vector3(0, -gravity, 0));
        }
    }
    void applyWallCollision()
    {
        if (movement.isCollidingLeftWall && acceleration.x <0 )
        {
            acceleration.x = 0;
            speed.x = 0f;
        }
        if (movement.isCollidingRightWall && acceleration.x > 0)
        {
            acceleration.x = 0;
            speed.x = 0f;
        }
    }
    //Function to call when colliding on the ground
    public void grounded()
    {
        speed.y = 0f;
    }
}
