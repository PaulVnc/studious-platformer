using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision
{
    private float x;
    private float y;
    private Vector2 normal;
    public Collision(float x, float y, Vector2 normal){
        this.x = x;
        this.y = y;
        this.normal = normal;
    }

    public Vector2 GetNormal(){
        return this.normal;
    }

    public Vector2 GetCoordinates()
    {
        return new Vector2(x, y);
    }
}
