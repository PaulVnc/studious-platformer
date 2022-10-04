using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public static CollisionManager Instance;
    private List<Collision> Collisions = new List<Collision>();
    [SerializeField]
    private GameObject player;
    private Hitbox playerHitbox;
    // Start is called before the first frame update
    void Awake(){

        // SINGLETON
        if(Instance == null){ 
            Instance = this;
        }else{
            Destroy(this);
        }
        playerHitbox = player.GetComponent<Hitbox>();
    }

    void FixedUpdate(){
            foreach(Hitbox candidate in HitboxManager.Instance.getHitboxes()){
                if(playerHitbox.getId() != candidate.getId() && playerHitbox.intersect(candidate)){
                    Collision collision = playerHitbox.getCollisionInfo(candidate);
                    Vector2 n = collision.getNormal();
                Debug.Log("Collisions :" + candidate.getId());
                    if(n == new Vector2(0,1)){
                        player.GetComponent<Movement>().collideGround();
                    }
                    if(n== new Vector2(0,-1)){
                        player.GetComponent<Movement>().collideCeiling();
                    }
                    if(n== new Vector2(1,0)){
<<<<<<< Updated upstream
                        Debug.Log("Collide left wall");
                        player.GetComponent<Movement>().collideLeftWall();
                    }   
                    if(n== new Vector2(-1,0)){
                    Debug.Log("Collide right wall");
                    player.GetComponent<Movement>().collideRightWall();
=======
                        player.GetComponent<Movement>().collideRightWall();
                    }   
                    if(n== new Vector2(-1,0)){
                        player.GetComponent<Movement>().collideLeftWall();
>>>>>>> Stashed changes
                    }
                }
            }
        HitboxManager.Instance.updateHitboxesPosition();
    }
    public void Update()
    {
        Collisions.Clear();
    }
    public List<Collision> getCollisions(){
        return Collisions;
    }

    public void addCollision(Collision collision){
        Collisions.Add(collision);
    }
}
