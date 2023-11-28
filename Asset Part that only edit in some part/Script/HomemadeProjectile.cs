using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtMonster_Dungeon;

public class HomemadeProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;                     //the object to shoot out
    public GameObject launchFxPrefab;                       //the fx effect to instantiate at launch
    public Transform muzzle;
    public PixelMonster Monster;


    public void Launch()
    {
        var fx = GetComponent<PixelMonster>();
        if (!fx) return;

        if (!projectilePrefab) return;
        var projectile = Instantiate(projectilePrefab);

        Vector2 LAddForce = new Vector2(-10, Random.Range(0.5f, 1));
        Vector2 RAddForce = new Vector2(10, Random.Range(0.5f, 1));

        if (Monster.Facing == -1)
        {
            projectile.GetComponent<Rigidbody2D>().AddForce(LAddForce);

        }
        else if (Monster.Facing == 1)
        {
            projectile.GetComponent<Rigidbody2D>().AddForce(RAddForce);
        }
        
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
}
