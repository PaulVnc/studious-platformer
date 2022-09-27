using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{

    public static HitboxManager Instance;
    private List<Hitbox> Hitboxes;
    private int nextHitboxId = 0;
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
        
    }

    public List<Hitbox> getHitboxes(){
        return Hitboxes;
    }

    public void addCollision(Hitbox hitbox){
        Hitboxes.Add(hitbox);
    }

    public int getNextHitboxId(){
        return nextHitboxId++;
    }
}
