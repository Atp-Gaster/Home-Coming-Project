using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cainos.CustomizablePixelCharacter;
using TMPro;
public class GameManagerScript : MonoBehaviour
{    
    public int CurrentTakeAction; // 0 MC , 1 Hero, 2 Rouge , 3 Healer
    public GameObject [] Player;
    private IEnumerator coroutine;
    public Button[] Button;
    //public Slider ManaBar;    
    public Slider HPBar;
    public float MC_HP = 100;
    public float MC_MP = 100;
    public GameObject Camera;

    public GameObject Hero_Skill;

    public GameObject BadEnding;
    public GameObject GoodEnding;

    public bool IsDead = false;

    public Vector3 CurrentCheckPoint;
    public Button SkillButton;
    public GameObject[] Skill;
    public bool GGEnding = false;
    bool Currently_buff = false;

    bool Decay = false;
    public int[] SkillCount = new int[] { 3, 3, 3, 3 };
    public PlayerHitbox CheckTP;

    public Image Skillimage;
    public Sprite [] SkillimageList;
    public GameObject[] MonsterSpawn;
    public GameObject TPEffect;
    public GameObject AudioManager;

    public GameObject[] Box;
    public TextMeshProUGUI DeadCountText;

    public int DeadCount = 0;

    public void CheckAction ()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            if (CurrentTakeAction == i)
            {
                Player[i].GetComponent<PixelCharacterController>().PermissionToMove = true;
                //Camera.GetComponent<CameraFollow>().target = Player[i].transform;
            }
            else Player[i].GetComponent<PixelCharacterController>().PermissionToMove = false;
        }
    }

    public void SetCDButton ()
    {
        coroutine = CDButton();
        StartCoroutine(coroutine);
    }

    private IEnumerator CDButton()
    {
        Button[0].interactable = false;
        Button[1].interactable = false;
        Button[2].interactable = false;
        Button[3].interactable = false;        
        yield return new WaitForSeconds(0.5f);
        Button[0].interactable = true;
        Button[1].interactable = true;
        Button[2].interactable = true;
        Button[3].interactable = true;

    }

    private IEnumerator DecayHP()
    {     
        while (!IsDead)
        {
            yield return new WaitForSeconds(3);
            MC_HP -= 1;
        }     
    }

    private IEnumerator BuffStat()
    {
        if(!Currently_buff)
        {
            Currently_buff = true;
            SkillCount[2] -= 1;
            Player[2].GetComponent<PixelCharacterController>().jumpSpeed = 9;
            Player[2].GetComponent<PixelCharacterController>().runSpeedMax = 9;
            Player[2].GetComponent<PixelCharacterController>().crouchSpeedMax = 4;
            yield return new WaitForSeconds(15);
            Currently_buff = false;
            Player[2].GetComponent<PixelCharacterController>().jumpSpeed = 6;
            Player[2].GetComponent<PixelCharacterController>().runSpeedMax = 5;
            Player[2].GetComponent<PixelCharacterController>().crouchSpeedMax = 1;
        }        
    }

    /*public GameObject CollisonChecker;*/

    public void Teleport()
    {
        float baseX = Player[0].transform.position.x;
        float baseY = Player[0].transform.position.y;
        float baseZ = Player[0].transform.position.z;

        Vector3 MCPos = new Vector3(baseX, baseY, baseZ);
        Vector3 LPosHero = new Vector3(baseX -0.5f, baseY, baseZ);
        Vector3 LPosRouge = new Vector3(baseX -1 , baseY, baseZ);
        Vector3 LPosHealer = new Vector3(baseX -1.5f, baseY, baseZ);

        Vector3 RPosHero = new Vector3(baseX + 0.5f, baseY, baseZ);
        Vector3 RPosRouge = new Vector3(baseX + 1, baseY, baseZ);
        Vector3 RPosHealer = new Vector3(baseX + 1.5f, baseY, baseZ);

        int Facing = Player[0].GetComponent<PixelCharacter>().Facing;
        /*Debug.Log(CheckTP.IsNearWall);*/

        if (Player[0].GetComponent<PixelCharacter>().IsGrounded)
        {
            if (!IsDead)
            {
                Player[0].GetComponent<PixelCharacter>().IsDead = false;
                Player[1].GetComponent<PixelCharacter>().IsDead = false;
                Player[2].GetComponent<PixelCharacter>().IsDead = false;
                Player[3].GetComponent<PixelCharacter>().IsDead = false;
            }

            if (Facing == 1 && !CheckTP.IsNearWall)
            {
                Player[1].transform.position = RPosHero;
                Player[2].transform.position = RPosRouge;
                Player[3].transform.position = RPosHealer;

                Instantiate(TPEffect, new Vector3(baseX + 0.5f, baseY, baseZ), Quaternion.identity);
            }
            else if (Facing == 1 && CheckTP.IsNearWall)
            {
                Player[1].transform.position = LPosHero;
                Player[2].transform.position = LPosRouge;
                Player[3].transform.position = LPosHealer;
                Instantiate(TPEffect, new Vector3(baseX - 0.5f, baseY, baseZ), Quaternion.identity);
            }
            else if(Facing == -1 && !CheckTP.IsNearWall)
            {
                Player[1].transform.position = LPosHero;
                Player[2].transform.position = LPosRouge;
                Player[3].transform.position = LPosHealer;
                Instantiate(TPEffect, new Vector3(baseX - 0.5f, baseY, baseZ), Quaternion.identity);
            }
            else if (Facing == -1 && CheckTP.IsNearWall)
            {
                Player[1].transform.position = RPosHero;
                Player[2].transform.position = RPosRouge;
                Player[3].transform.position = RPosHealer;
                Instantiate(TPEffect, new Vector3(baseX - 0.5f, baseY, baseZ), Quaternion.identity);
            }
            else
            {
                SkillCount[0] += 1;
            }
        }         
    }

    public void UseSkill()
    {         
        //Debug.Log(ManaBar.value);
        //Debug.Log(CurrentTakeAction);
        if (CurrentTakeAction == 0 && SkillCount[CurrentTakeAction] >= 1) // MC 
        {
            Teleport();
            SkillCount[CurrentTakeAction] -= 1;             
        }
        if (CurrentTakeAction == 1 && SkillCount[CurrentTakeAction] >= 1) // Hero 
        {
            Hero_Skill.GetComponent<UniqueAblity>().EnableShilde();
            SkillCount[CurrentTakeAction] -= 1;
        }
        if (CurrentTakeAction == 2 && SkillCount[CurrentTakeAction] >= 1) // Rouge
        {
            coroutine = BuffStat();
            StartCoroutine(coroutine);
            //SkillCount[CurrentTakeAction] -= 1;
        }
        if (CurrentTakeAction == 3 && SkillCount[CurrentTakeAction] >= 1) // Healer
        {
            MC_HP += 50;
            SkillCount[CurrentTakeAction] -= 1;
        }
    }   
    


    public void GameOver()
    {               
        if(!IsDead)
        {
            IsDead = true;
            DeadCount++;
            BadEnding.gameObject.SetActive(true);
            BadEnding.GetComponent<Animation>().Play();
            AudioManager.GetComponent<AudioManager>().Playsound(12);
            PlayerPrefs.SetInt("DeadCount", DeadCount);
        }
    }


    public void SaveCheckPoint ()
    {
        PlayerPrefs.SetFloat("X", Player[0].transform.position.x);
        PlayerPrefs.SetFloat("Y", Player[0].transform.position.y);
        PlayerPrefs.SetFloat("Z", Player[0].transform.position.z);        
    }

    public void LoadCheckPoint()
    {       
        float SavePointX = PlayerPrefs.GetFloat("X");
        float SavePointY = PlayerPrefs.GetFloat("Y");
        float SavePointZ = PlayerPrefs.GetFloat("Z");

        Vector3 CurrentCheckPoint = new Vector3(SavePointX, SavePointY, SavePointZ);

        Player[0].transform.position = CurrentCheckPoint;
        Teleport();
        Debug.Log(CurrentCheckPoint);
    }
    void SetSkillActive(bool skill1Active, bool skill2Active, bool skill3Active)
    {
        Skill[0].SetActive(skill1Active);
        Skill[1].SetActive(skill2Active);
        Skill[2].SetActive(skill3Active);
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveCheckPoint();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAction();
        HPBar.value = MC_HP;      
        if(!IsDead)
        {
            BadEnding.gameObject.SetActive(false);
            if (Input.GetKeyDown(KeyCode.R)) MC_HP = 0;
        }            
        if (MC_HP >= 100) MC_HP = 100;
        const int MAX_SKILLS = 3;
        if (CurrentTakeAction != -99)
        {
            switch (SkillCount[CurrentTakeAction])
            {
                case MAX_SKILLS:
                    SetSkillActive(true, true, true);
                    break;
                case MAX_SKILLS - 1:
                    SetSkillActive(false, true, true);
                    break;
                case MAX_SKILLS - 2:
                    SetSkillActive(false, false, true);
                    break;
                default:
                    SetSkillActive(false, false, false);
                    break;
            }

            switch (CurrentTakeAction)
            {
                case 0:
                    Skillimage.sprite = SkillimageList[0];
                    break;
                case 1:
                    Skillimage.sprite = SkillimageList[1];
                    break;
                case 2:
                    Skillimage.sprite = SkillimageList[2];
                    break;
                case 3:
                    Skillimage.sprite = SkillimageList[3];
                    break;                
            }

        }      
        if (!Decay)
        {
            coroutine = DecayHP();
            StartCoroutine(coroutine);
            Decay = true;
        }        

        if (MC_HP <= 0) GameOver();

        if(Input.GetKeyDown("1"))
        {
            CurrentTakeAction = 0;
        }
        if (Input.GetKeyDown("2"))
        {
            CurrentTakeAction = 1;
        }
        if (Input.GetKeyDown("3"))
        {
            CurrentTakeAction = 2;
        }
        if (Input.GetKeyDown("4"))
        {
            CurrentTakeAction = 3;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Player[0].GetComponent<PixelCharacter>().IsGrounded) UseSkill();
            else if (CurrentTakeAction != 0) UseSkill();

        }

        if(Currently_buff && CurrentTakeAction == 2) SkillButton.interactable = false;
        if(CurrentTakeAction != 2) SkillButton.interactable = true;

        if(Input.GetKeyDown(KeyCode.Space) && IsDead)
        {
            IsDead = false;
            Decay = false;
            LoadCheckPoint();
            MC_HP = 100;            
            SkillCount = new int[] { 3, 3, 3, 3 };
            for(int i = 0; i < MonsterSpawn.Length; i++)
            {
                MonsterSpawn[i].GetComponent<EnemyAI>().Respawn = true;
                MonsterSpawn[i].transform.parent.gameObject.SetActive(true);
                MonsterSpawn[i].GetComponent<EnemyAI>().Respawn = true;
            }
            for (int i = 0; i < Box.Length; i++)
            {               
                Box[i].GetComponent<BoxRespawn>().Respawn = true;
            }
        }
        if(GGEnding && GoodEnding != null) 
        {
            MC_HP = 100;
            GoodEnding.gameObject.SetActive(true);
            int TotalDead = PlayerPrefs.GetInt("DeadCount");
            if (DeadCountText != null) DeadCountText.SetText("Total Dead: " + TotalDead);
        }        
    }
}

