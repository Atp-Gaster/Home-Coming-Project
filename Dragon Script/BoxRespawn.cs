using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    public Vector2 RespawnPoint;
    public bool Respawn = false;
    // Start is called before the first frame update
    void Start()
    {
        RespawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Respawn)
        {
            transform.position = RespawnPoint;
            Respawn = false;
        }
    }
}
