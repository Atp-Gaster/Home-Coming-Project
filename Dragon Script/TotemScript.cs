using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
    public GameObject DamageZone;
    public GameObject GlowingFX;
    public GameObject SparkFX;
    public bool isClean = false;
    public SpriteRenderer[] spriteRenderer;
    public Material deafultMaterial;
    public Material LightMaterial;
    public bool IsInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isClean)
        {
            DamageZone.SetActive(false);
            GlowingFX.SetActive(false);
            SparkFX.SetActive(false);
        }

        if (IsInRange)
        {
            spriteRenderer[0].GetComponent<SpriteRenderer>().material = LightMaterial;
            spriteRenderer[1].GetComponent<SpriteRenderer>().material = LightMaterial;
            Color Test = new Color(3f, 255f, 0f, 255f);
            spriteRenderer[0].GetComponent<SpriteRenderer>().color = Test;
            spriteRenderer[1].GetComponent<SpriteRenderer>().color = Test;
        }
        else if (!IsInRange)
        {
            spriteRenderer[0].GetComponent<SpriteRenderer>().material = deafultMaterial;
            spriteRenderer[1].GetComponent<SpriteRenderer>().material = deafultMaterial;
            Color Test2 = new Color(255f, 255f, 255f, 255f);
            spriteRenderer[0].GetComponent<SpriteRenderer>().color = Test2;
            spriteRenderer[1].GetComponent<SpriteRenderer>().color = Test2;
        }
    }
}
