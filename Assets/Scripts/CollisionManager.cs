using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public static CollisionManager Instance;
    private List<Collision> Collisions;
    // Start is called before the first frame update
    void Start(){

        // SINGLETON
        if(Instance == null){ 
            Instance = this;
        }else{
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update(){
        foreach(Hitbox hitbox in HitboxManager.Instance.getHitboxes()){
            foreach(Hitbox candidate in HitboxManager.Instance.getHitboxes()){
                if(hitbox.intersect(candidate)){
                    Collision collision = hitbox.getCollisionInfo(candidate);
                    addCollision(collision);
                }
            }
        }
    }

    public List<Collision> getCollisions(){
        return Collisions;
    }

    public void addCollision(Collision collision){
        Collisions.Add(collision);
    }
}
