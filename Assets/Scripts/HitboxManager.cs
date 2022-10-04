using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{

    public static HitboxManager Instance;
    private List<Hitbox> Hitboxes = new List<Hitbox>();
    private int nextHitboxId = 0;
    // Start is called before the first frame update
    void Awake(){
        // SINGLETON
        if(Instance == null){ 
            Instance = this;
        }else{
            Destroy(this);
        }

    }

    public List<Hitbox> getHitboxes(){
        return Hitboxes;
    }

    public void addHitbox(Hitbox hitbox){
        Hitboxes.Add(hitbox);
    }

    public int getNextHitboxId(){
        return nextHitboxId++;
    }

    public void updateHitboxesPosition(){
        foreach(Hitbox hitbox in getHitboxes()){
            hitbox.updatePosition();
        }
    }
}
