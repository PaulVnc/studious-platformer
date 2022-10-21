using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimations : MonoBehaviour
{
    [SerializeField]
    private float deformThreshold;
    [SerializeField]
    private float deformAmount;
    private Vector3 scale;
    private Vector3 defaultScale;
    float defaultRatio;
    private Movement movementScript;
    private Physics physicScript;
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale;
        scale = transform.localScale;
        defaultRatio = defaultScale.x / defaultScale.y;
        movementScript = GetComponent<Movement>();
        physicScript = GetComponent<Physics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(physicScript.speed.y) >= deformThreshold)
        {
            Debug.Log(Mathf.Abs(physicScript.speed.y));
            scale.y = defaultScale.y * (1 +deformAmount*Mathf.Log10(Mathf.Abs(physicScript.speed.y)));
            scale.x = 1/scale.y;
        }
        else 
        {
            if (movementScript.isGrounded)
            {
                scale = defaultScale;
            }
        }
        transform.localScale = scale;
    }
}
