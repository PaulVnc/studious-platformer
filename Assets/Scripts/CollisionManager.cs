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

        bool collideGround = false;
        bool collideCeiling = false;
        bool collideRightWall = false;
        bool collideLeftWall = false;
        foreach (Hitbox candidate in HitboxManager.Instance.getHitboxes()){
            if(playerHitbox.getId() != candidate.getId() && playerHitbox.intersect(candidate)){
                Collision collision = playerHitbox.getCollisionInfo(candidate);
                Vector2 n = collision.GetNormal();
                Debug.Log("Collisions :" + candidate.getId());
                if(n == new Vector2(0,1)){
                    collideGround = true;
                    Vector3 playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x, candidate.GetMax().y + playerHitbox.h / 2,0);
                }
                if(n== new Vector2(0,-1)){
                    collideCeiling = true;
                    Vector3 playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x, candidate.GetMin().y - playerHitbox.h / 2, 0);
                }
                if(n== new Vector2(1,0)){
                    collideLeftWall = true;
                    Debug.Log("Collide left wall");
                }   
                if(n== new Vector2(-1,0)){
                    collideRightWall = true;
                    Debug.Log("Collide right wall");
                }
            }
        }
        if (collideCeiling)
        {
            player.GetComponent<Movement>().collideCeiling();
        }
        player.GetComponent<Movement>().setGrounded(collideGround);
        player.GetComponent<Movement>().setCollideLeftWall(collideLeftWall);
        player.GetComponent<Movement>().setCollideRightWall(collideRightWall);
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
