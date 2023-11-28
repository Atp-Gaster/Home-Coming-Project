using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.CustomizablePixelCharacter;
public class UniqueAblity : MonoBehaviour
{
    public int Char_ID = -99;
    public GameObject Shilde;    
    public GameObject VFXShilde;
    public PixelCharacter Char;
    public bool IsShildeUp = false;
    public bool IsNearWall = false;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Awake()
    {
        if (transform.parent.name == "Main Character") Char_ID = 0;
        else if (transform.parent.name == "Hero") Char_ID = 1;
        else if (transform.parent.name == "Rouge") Char_ID = 2;
        else if (transform.parent.name == "Healer") Char_ID = 3;      
    }

    private IEnumerator ActiveShilde()
    {
        Shilde.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(10);
        Shilde.GetComponent<Collider2D>().enabled = false;
        VFXShilde.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void EnableShilde ()
    {
        if(Char_ID == 1)
        {
            if (!IsShildeUp)
            {
                IsShildeUp = true;
                Shilde = GameObject.Find("Shield Hitbox");
                Shilde.GetComponent<Collider2D>().enabled = true;
                StartCoroutine(ActiveShilde());
                VFXShilde.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                IsShildeUp = false;
                Shilde = GameObject.Find("Shield Hitbox");
                Shilde.GetComponent<Collider2D>().enabled = false;
                GameObject.Find("GameManager").GetComponent<GameManagerScript>().SkillCount[1] += 1;
                VFXShilde.GetComponent<SpriteRenderer>().enabled = false;
            }
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            IsNearWall = true;
            Debug.Log("Test");
        }

        if (collision.tag == "Arrow" && Char_ID == 1)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            IsNearWall = false;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        /*if (Char_ID == 0) SwitchHitboxChecker();*/
        if (Char_ID == 1 )
        {    
            if (Char.Facing == -1)
            {
                Shilde.GetComponent<BoxCollider2D>().offset = new Vector2(-1.10242939f, 1.1689533f);
                VFXShilde.transform.position = new Vector2(transform.parent.localPosition.x - 1, transform.parent.localPosition.y);
                VFXShilde.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                Shilde.GetComponent<BoxCollider2D>().offset = new Vector2(1.10242939F, 1.1689533F);
                VFXShilde.transform.position = new Vector2(transform.parent.localPosition.x + 1, transform.parent.localPosition.y);
                VFXShilde.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
       
    }
}
