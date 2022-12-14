using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Physics : MonoBehaviour
{
    [SerializeField] float gravity = 10f;
    public Vector3 constantAcceleration;
    public Vector3 acceleration;
    public Vector3 speed;
    private Movement movement;
    float aerialDriftMax = 10f;

    

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
        if (!movement.isDashing)
        {
            applyGravity();
            speed += acceleration * Time.fixedDeltaTime;
            if (!movement.isGrounded && Mathf.Abs(speed.x) > aerialDriftMax)
            {
                speed.x = Mathf.Sign(speed.x) * aerialDriftMax;
            }
            transform.position += speed * Time.fixedDeltaTime;
            reduceSpeed();

            acceleration = Vector3.zero;
        }
        
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
    public void resetY()
    {
        acceleration.y = 0f;
        speed.y = 0f;
    }
    void applyGravity()
    {
        if (!movement.isGrounded)
        {
             speed += new Vector3(0, -gravity, 0) * Time.fixedDeltaTime;
        }
    }
    
    //Function to call when colliding on the ground
    public void grounded()
    {
        if (speed.y <= 0f)
        {
            speed.y = 0f;
        }
        speed.x = 0f;
    }

    //Function to call when colliding on the ceiling
    public void ceilingBlock()
    {
        if (acceleration.y > 0f)
        {
            acceleration.y = 0f;
        }
        if (speed.y > 0f)
        {
            speed.y = 0f;
        }
    }
    public void wallBlockRight()
    {
       if (acceleration.x > 0)
        {
            acceleration.x = 0f;
        }
       if (speed.x > 0)
        {
            speed.x = 0f;
        }
    }

    public void wallBlockLeft()
    {
        if (acceleration.x < 0)
        {
            acceleration.x = 0f;
        }
        if (speed.x < 0)
        {
            speed.x = 0f;
        }
    }
   
}
