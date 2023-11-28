using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtMonster_Dungeon;
public class EnemyPath : MonoBehaviour
{
    public Transform FirstPoint;
    public Transform SecondPoint;
    public Transform MovePoint;

    public MonsterController Enemy;
    public GameObject LoS;
    public int TargetPos = -69;
    float MoveSpeed = 0;

    public GameObject Animation;
    public bool CanMove = false;
    public int MovePattern = 0; // 0 Walk 1 Run    

    public bool inverse = false;

    void Start()
    {
       
    }

    void MoveToTarget(int TargetPos)
    {
        switch (TargetPos)
        {
            case 0:
                MovePoint.position = new Vector3(FirstPoint.position.x, FirstPoint.position.y, FirstPoint.position.z); //Move waypoint(movePoint) // -130 for make it stop in front of target
                break;
            case 1:
                MovePoint.position = new Vector3(SecondPoint.position.x, SecondPoint.position.y, SecondPoint.position.z); //Move waypoint(movePoint) // -130 for make it stop in front of target
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        MoveToTarget(TargetPos);               

        //GetComponent<PixelMonster>().MovingBlend = 0.5f;
        if (CanMove)
        {
            switch (MovePattern)
            {
                case 0:
                    GetComponent<PixelMonster>().MovingBlend = 0.5f;
                    MoveSpeed = 0.25f;
                    break;
                case 1:
                    GetComponent<PixelMonster>().MovingBlend = 10f;
                    MoveSpeed = 0.5f;
                    break;
                case 2:
                    GetComponent<PixelMonster>().MovingBlend = 100f;
                    MoveSpeed = 0.75f;
                    break;
            }

            if(!inverse)
            {
                if (MovePoint.position.x >= (transform.position.x - 0.5f) && TargetPos == 0)
                {
                    TargetPos = 1;
                    GetComponent<MonsterController>().inputMove = new Vector2(1, 0);
                    
                  /*  Debug.Log("->");*/
                }
                else if (MovePoint.position.x <= (transform.position.x + 0.5f) && TargetPos == 1)
                {
                    TargetPos = 0;
                    GetComponent<MonsterController>().inputMove = new Vector2(-1, 0);
                  
                   /* Debug.Log("<-");*/
                }
            }
            else if (inverse)
            {
                if (MovePoint.position.x >= (transform.position.x - 0.5f) && TargetPos == 0)
                {
                    TargetPos = 1;
                    GetComponent<MonsterController>().inputMove = new Vector2(1, 0);
                    GetComponent<PixelMonster>().Facing = 1;
                    LoS.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    //GetComponent<PixelMonster>().MovingBlend = 100f;
                    //transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);         
                    /*Debug.Log("->");*/
                }
                else if (MovePoint.position.x <= (transform.position.x + 0.5f) && TargetPos == 1)
                {
                    TargetPos = 0;
                    GetComponent<MonsterController>().inputMove = new Vector2(-1, 0);
                    GetComponent<PixelMonster>().Facing = -1;
                    LoS.transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);                
                    //if (Animation.transform.localScale.z == -1) Animation.transform.localScale = new Vector3(1, 1, 1);
                    /*Debug.Log("<-");*/
                }
            }




            transform.position = Vector3.Lerp(transform.position, MovePoint.position, MoveSpeed * Time.deltaTime);
        }
    }        
}
