using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject [] PlayerCharacter;
    public GameManagerScript GM;
    public void Respawn()
    {
        PlayerCharacter[0].transform.position = GM.GetComponent<GameManagerScript>().CurrentCheckPoint;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
