using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public static CollisionManager Instance;
    private List<Collision> Collisions = new List<Collision>();
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Awake(){

        // SINGLETON
        if(Instance == null){ 
            Instance = this;
        }else{
            Destroy(this);
        }

    }

    void FixedUpdate(){
        foreach(Hitbox hitbox in HitboxManager.Instance.getHitboxes()){
            foreach(Hitbox candidate in HitboxManager.Instance.getHitboxes()){
                if(hitbox.getId() != candidate.getId() && hitbox.intersect(candidate)){
                    Debug.Log("Collision");
                    Collision collision = hitbox.getCollisionInfo(candidate);
                    addCollision(collision);    
                    if(hitbox.layer == CollisionLayerEnum.Layer.Player){
                        Vector2 n = collision.getNormal();
                        Debug.Log("Normal:" + n);
                        if(n == new Vector2(0,1)){
                            player.GetComponent<Movement>().collideGround();
                        }
                        if(n== new Vector2(0,-1)){
                            player.GetComponent<Movement>().collideCeiling();
                        }
                        if(n== new Vector2(1,0)){
                            player.GetComponent<Movement>().collideLeftWall();
                        }
                        if(n== new Vector2(-1,0)){
                            player.GetComponent<Movement>().collideRightWall();
                        }
                    }       
                }
            }
        }
    }

    public List<Collision> getCollisions(){
        return Collisions;
    }

    public void addCollision(Collision collision){
        Collisions.Add(collision);
        Debug.Log("oui");
    }
}
