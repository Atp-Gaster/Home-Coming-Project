using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtPlatformer_Dungeon;
using Cainos.CustomizablePixelCharacter;


public class PlayerHitbox : MonoBehaviour
{
    public bool isMC = false; //Check whatever is Main Character or not?
    private IEnumerator coroutine;
    private bool Isinfog = false;
    public GameObject AudioManager;
    public bool IsNearWall = false;

    float AttackCD = 1.5f;
    bool AlreadyATK = false;    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            Destroy(collision.transform.parent);
            Destroy(collision.gameObject);
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().MC_HP -= 5;
            AudioManager.GetComponent<AudioManager>().Playsound(4);
        }
        if (collision.tag == "DeadZone")
        {
            Debug.Log("Entry Deadzone");
            coroutine = DeadZone();
            StartCoroutine(coroutine);
            if (transform.parent.name == "Main Character") AudioManager.GetComponent<AudioManager>().Playsound(5);
        }
        if (collision.tag == "DamageZone")
        {
            if (transform.parent.name != "Healer")
            {
                Isinfog = true;
                //InvokeRepeating("DamageZone", 0.5f, 1f);
                coroutine = DamageZone();
                StartCoroutine(coroutine);
            }
        }
        if(collision.tag == "SavePoint")
        {
            
            if (isMC)
            {
                Debug.Log("Hit!");
                GameObject.Find("GameManager").GetComponent<GameManagerScript>().SaveCheckPoint();
            }
        }       
        if (collision.tag == "ShowTutorial")
        {
           if(GameObject.Find("GameManager").GetComponent<ToturialManager>() != null) GameObject.Find("GameManager").GetComponent<ToturialManager>().ShowPickLock();
        }

        if (collision.tag == "Enemy" && transform.parent.GetComponentInParent<PixelCharacter>().IsAttacking)
        {
            if(AlreadyATK)
            {                
                collision.GetComponent<EnemyHitbox>().HitPoint -= 1;
                AudioManager.GetComponent<AudioManager>().Playsound(4);
                if (transform.parent.name == "Main Character") AudioManager.GetComponent<AudioManager>().Playsound(5);
                AlreadyATK = false;
            }
      
          
        }

        if (collision.tag == "Wall")
        {
            IsNearWall = true;
            Debug.Log("Test");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Lighting Object for interact with UI
        if (collision.tag == "Door")
        {
           collision.GetComponent<Door>().IsInRange = true;
        }
        if (collision.tag == "Totem")
        {
            if (transform.parent.name == "Healer") collision.GetComponent<TotemScript>().IsInRange = true;
        }
        if (collision.tag == "Pilar")
        {
            collision.GetComponent<Pilar>().IsInRange = true;
        }

        if (collision.tag == "Enemy" && transform.parent.GetComponentInParent<PixelCharacter>().IsAttacking)
        {
            if (AlreadyATK)
            {
                collision.GetComponent<EnemyHitbox>().HitPoint -= 1;
                AudioManager.GetComponent<AudioManager>().Playsound(4);
                if (transform.parent.name == "Main Character") AudioManager.GetComponent<AudioManager>().Playsound(5);
                AlreadyATK = false;
            }
        }
        /*if (collision.tag == "Arrow")
        {
            Destroy(collision.transform.parent);
            Destroy(collision.gameObject);            
        }*/


        // Interact Object With
        if (collision.tag == "Exit" && Input.GetKeyDown(KeyCode.E))
        {
            //collision.GetComponent<Door>().IsInRange = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage 1");
            Debug.Log("Going To Next Stage");
        }

        if (collision.tag == "TrueExit" && Input.GetKeyDown(KeyCode.E))
        {
            //collision.GetComponent<Door>().IsInRange = true;

            GameObject.Find("GameManager").GetComponent<GameManagerScript>().GGEnding = true;

        }
        if (collision.tag == "Door" && Input.GetKeyDown(KeyCode.E))
        {
            // Debug.Log("Check");
            // Debug.Log(collision.GetComponent<Door>().IsOpened);

            if (transform.parent.name == "Rouge")
            {
                collision.GetComponent<Door>().IsLock = false;
                collision.GetComponent<Door>().IsOpen = true;
                AudioManager.GetComponent<AudioManager>().Playsound(9);
            }
            else if (collision.GetComponent<Door>().IsOpened == false && collision.GetComponent<Door>().IsLock == false)
            {
                collision.GetComponent<Door>().IsOpen = true;
                AudioManager.GetComponent<AudioManager>().Playsound(6);
            }
            else if (collision.GetComponent<Door>().IsOpened == true)
            {
                collision.GetComponent<Door>().IsOpen = false;
                AudioManager.GetComponent<AudioManager>().Playsound(7);
            }
        }            
        if(collision.tag == "Totem" && Input.GetKeyDown(KeyCode.E))
        {
            if (transform.parent.name == "Healer")
            {
                collision.GetComponent<TotemScript>().isClean = true;
                AudioManager.GetComponent<AudioManager>().Playsound(11);
            }
        }
        if (collision.tag == "Pilar" && Input.GetKeyDown(KeyCode.E))
        {
            if (transform.parent.name == "Hero") //Debug.Log(collision.name);                
            {
                collision.GetComponent<Animation>().Play();
                AudioManager.GetComponent<AudioManager>().Playsound(12);
            }
            
        }
        //Etc
        if (collision.tag == "Crouch Area")
        {
            transform.parent.GetComponentInParent<PixelCharacter>().EntryCrouchArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DamageZone")
        {
            if (transform.parent.name != "Healer")
            {
                Isinfog = false;

            }
        }

        if (collision.tag == "Door")
        {
            collision.GetComponent<Door>().IsInRange = false;
        }
        if (collision.tag == "Totem")
        {
            collision.GetComponent<TotemScript>().IsInRange = false;
        }
        if (collision.tag == "Pilar")
        {
            collision.GetComponent<Pilar>().IsInRange = false;
        }

        //Etc
        if (collision.tag == "Crouch Area")
        {
            transform.parent.GetComponentInParent<PixelCharacter>().EntryCrouchArea = false;
        }

        if (collision.tag == "Wall")
        {
            IsNearWall = false;
            Debug.Log("Test");
        }
    }

    IEnumerator DamageZone()
    {
        while (Isinfog)
        {
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().MC_HP -= 4;
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator DeadZone()
    {
        transform.parent.GetComponentInParent<PixelCharacter>().IsDead = true;                 
        yield return new WaitForSeconds(2f);
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().MC_HP -= 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponentInParent<PixelCharacter>().Facing == -1) transform.localScale = new Vector2(-1, 1);
        if (transform.parent.GetComponentInParent<PixelCharacter>().Facing == 1) transform.localScale = new Vector2(1, 1);

        if (AttackCD > 0 && !AlreadyATK) AttackCD -= Time.deltaTime;
        if (AttackCD <= 0 && !AlreadyATK)
        {
            AlreadyATK = true;
            AttackCD = 1.5f;
        }
    }
}
