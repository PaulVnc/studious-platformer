using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private float x;
    private float y;
    private Vector2 normal;
    Collision(float x, float y, Vector2 normal){
        this.x = x;
        this.y = y;
        this.normal = normal;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
