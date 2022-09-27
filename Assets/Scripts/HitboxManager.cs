using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{

    public static HitboxManager Instance;
    private vector<Hitbox> Hitboxes;
    // Start is called before the first frame update
    void Start(){

        // SINGLETON
        if(Instance == null){ 
            Instance = this;
        }else{
            this.destroy;
        }

    }

    // Update is called once per frame
    void Update(){
        
    }

    public vector<Hitbox> getHitboxes(){
        return Hitboxes;
    }

    public void addCollision(Hitbox hitbox){
        Hitboxes.push_back(hitbox);
    }
}
