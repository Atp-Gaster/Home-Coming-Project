using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.CustomizablePixelCharacter;
using Cainos.PixelArtMonster_Dungeon;

public class EnemyAI : MonoBehaviour
{
    public float RepeatingAttack;
    // Start is called before the first frame update
    public MonsterController Enemy;
    public EnemyPath Pathfinder;
    public bool IsRange = false;
    PixelCharacter Character;
    public bool Stationary = false;
    public EnemyHitbox Hitbox;

    float AttackCD = 3.5f;
    bool AlreadyATK = false;

    public bool AbletoMove = false;
    public bool Respawn = false;
    
    private IEnumerator coroutine;
    bool Seeing = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            /*if (collision.transform.parent.GetComponent<PixelCharacter>().IsAttacking) HitPoint -= 1;*/
            Seeing = true;
            if (AlreadyATK) StartCoroutine(AttackingC());            
            Character = collision.GetComponent<PixelCharacter>();
            if(AbletoMove) Pathfinder.CanMove = false;
            
        }      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            /*if (collision.transform.parent.GetComponent<PixelCharacter>().IsAttacking) HitPoint -= 1;*/           
        }      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Seeing = false;
            if (AbletoMove) Pathfinder.CanMove = true;
        }
    }

    IEnumerator AttackingC()
    {
        while (Seeing)
        {
            Enemy.Attack(true);
            AlreadyATK = false;
            if (!IsRange) GameObject.Find("GameManager").GetComponent<GameManagerScript>().MC_HP -= 10;
            yield return new WaitForSeconds(3.5f);          
        }
    }
    
    void Awake()
    {
        if(Pathfinder != null) AbletoMove = Pathfinder.CanMove;
    }

    // Update is called once per frame
    void Update()
    {        
        /*transform.parent.GetComponent<PixelMonster>().HitPoint*/

        if (AttackCD > 0 && !AlreadyATK) AttackCD -= Time.deltaTime;
        if (AttackCD <= 0 && !AlreadyATK)
        {
            AlreadyATK = true;
            AttackCD = 3.5f;
        }        

        if (Respawn)
        {
            Respawn = false;
            Hitbox.Respawn();
        }
        if (Stationary) transform.parent.GetComponent<PixelMonster>().Facing = -1;
    }
}


