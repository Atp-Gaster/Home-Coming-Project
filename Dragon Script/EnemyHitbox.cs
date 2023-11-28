using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public int HitPoint;
    int MAXHP;
    bool IsDEAD = false;    
    private IEnumerator coroutine;
    IEnumerator CDDead()
    {
        yield return new WaitForSeconds(0.15f);
        transform.parent.gameObject.SetActive(false);    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            HitPoint -= 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MAXHP = HitPoint;
    }

    public void Respawn()
    {
        HitPoint = MAXHP;
        IsDEAD = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (HitPoint <= 0 && !IsDEAD)
        {
            IsDEAD = true;
            StartCoroutine(CDDead());
            
        }
    }
}
