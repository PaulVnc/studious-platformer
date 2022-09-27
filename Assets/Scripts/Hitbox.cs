using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private CollisionLayerEnum layer;
    private float xMin, yMin, xMax, yMax;
    private int id;
    // Start is called before the first frame update
    void Start()
    {
        this.id = HitboxManager.Instance.getNextHitboxId();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getId(){
        return id;
    }

    public bool intersect(Hitbox candidate){
        return (this.xMin < candidate.xMin && this.xMax > candidate.xMin && (this.yMin > candidate.yMin && this.yMax > candidate.yMin
            || this.yMax > candidate.yMin && this.yMax < candidate.yMax)
            || this.xMin > candidate.xMin && this.xMin < candidate.xMax && (this.yMin > candidate.yMin && this.yMax > candidate.yMin
            || this.yMax > candidate.yMin && this.yMax < candidate.yMax));
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
        if(this.yMin > other.yMax){
            n = new Vector2(0,1);
        }
        else if(this.yMax < other.yMin){
            n = new Vector2(0,-1);
        }
        else{
            n = (this.xMax>other.xMin)? new Vector2(-1,0): new Vector2(1,0);
        }
        Collision collision = new Collision(x,y,n);
        return collision;
    }
}
