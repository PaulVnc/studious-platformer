using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    public CollisionLayerEnum.Layer layer;
    [SerializeField]
    private float xMin, yMin, xMax, yMax;
    [SerializeField]
    private float oldxMin, oldyMin, oldxMax, oldyMax;
    [SerializeField]
    private float w,h;
    private int id;
    // Start is called before the first frame update
    void Start()
    {
        this.id = HitboxManager.Instance.getNextHitboxId();
        HitboxManager.Instance.addHitbox(this);
        w = transform.localScale.x;
        h = transform.localScale.y;
        xMin = transform.position.x - w/2;
        xMax = transform.position.x + w/2;
        yMin = (transform.position.y - h/2) - 0.1f;
        yMax = transform.position.y + h/2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xMin = transform.position.x - w/2;
        xMax = transform.position.x + w/2;
        yMin = (transform.position.y - h/2) - 0.1f;
        yMax = transform.position.y + h/2;

        Debug.DrawLine(new Vector3(xMin, yMin, 0), new Vector3(xMax, yMin, 0), Color.green);
        Debug.DrawLine(new Vector3(xMin, yMin, 0), new Vector3(xMin, yMax, 0), Color.green);
        Debug.DrawLine(new Vector3(xMax, yMax, 0), new Vector3(xMax, yMin, 0), Color.green);
        Debug.DrawLine(new Vector3(xMax, yMax, 0), new Vector3(xMin, yMax, 0), Color.cyan);
    }

    public void updatePosition(){
        oldxMin = xMin;
        oldyMin = yMin;
        oldxMax = xMax;
        oldyMax = yMax;
    }

    public int getId(){
        return id;
    }

    public bool intersect(Hitbox candidate){
        return ((this.xMin < candidate.xMax && this.xMax > candidate.xMin) && (this.yMin < candidate.yMax && this.yMax > candidate.yMin));
    }

    public Collision getCollisionInfo(Hitbox other){
        float x1,y1,x2,y2;
        
        if(this.xMin < other.xMin){
            if(this.xMax > other.xMax){
                x1 = other.xMin;
                x2 = other.xMax;
            }else{
                x1 = other.xMin;
                x2 = this.xMax;
            }
        }else{
            if(this.xMax < other.xMax){
                x1 = this.xMin;
                x2 = this.xMax;
            }else{
                x1 = this.xMin;
                x2 = other.xMax;
            }
        }

        if(this.yMin < other.yMin){
            if(this.yMax > other.yMax){
                y1 = other.yMin;
                y2 = other.yMax;
            }else{
                y1 = other.yMin;
                y2 = this.yMax;
            }
        }else{
            if(this.yMax < other.yMax){
                y1 = this.yMin;
                y2 = this.yMax;
            }else{
                y1 = this.yMin;
                y2 = other.yMax;
            }
        }

        float x = (x1+x2)/2;
        float y = (y1+y2)/2;
        Vector2 n;
        if (this.oldyMin > other.oldyMax)
        {
            n = new Vector2(0, 1);
        }
        else if (this.oldyMax < other.oldyMin)
        {
            n = new Vector2(0, -1);
        }
        else
        {
            float xDist = xMax - w / 2 - x;
            float yDist = yMax - h / 2 - y;
            if (Mathf.Abs(xDist) < Mathf.Abs(yDist))
            {
                xDist = 0;
                yDist = 1 * Mathf.Sign(yDist);
            }
            else
            {
                yDist = 0;
                xDist = 1 * Mathf.Sign(xDist);
            }
            n = new Vector2(xDist, yDist);
        }
        /*else if (this.oldxMax < other.oldxMin && this.oldyMin < other.oldyMax)
        {
            n = new Vector2(1, 0);
        }
        else if (this.oldxMin > other.oldyMax && this.oldyMin < other.oldyMax)
        {
            n = new Vector2(-1, 0);
        }
        else
        {
            n = new Vector2(0, 0);
        }*/
        Debug.Log(n);
        Collision collision = new Collision(x,y,n);
        Debug.DrawLine(new Vector3(this.xMax-w/2,this.yMax-h/2,0),new Vector3(x,y,0),Color.red);
        return collision;
    }

    public Vector2 GetMin()
    {
        return new Vector2(xMin, yMin);
    }
    public Vector2 GetMax()
    {
        return new Vector2(xMax, yMax);
    }
}
