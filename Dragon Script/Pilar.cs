using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilar : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool IsInRange = false;
    public Material deafultMaterial;
    public Material LightMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange)
        {
            spriteRenderer.GetComponent<SpriteRenderer>().material = LightMaterial;
            Color Test = new Color(3f, 255f, 0f, 255f);
            spriteRenderer.GetComponent<SpriteRenderer>().color = Test;
        }
        else if (!IsInRange)
        {
            spriteRenderer.GetComponent<SpriteRenderer>().material = deafultMaterial;
            Color Test2 = new Color(255f, 255f, 255f, 255f);
            spriteRenderer.GetComponent<SpriteRenderer>().color = Test2;
        }
    }
}
