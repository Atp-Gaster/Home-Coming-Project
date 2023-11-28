using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtMonster_Dungeon
{
    public class Projectile : MonoBehaviour
    {
        public float lifeTime = 2.0f;
        public GameObject explosionPrefab;
        public GameObject MonsterProjectile;
        private float lifeTimer;

        public Vector2 facing = new Vector2(0,0);

        private void Update()
        {           
            lifeTimer += Time.deltaTime;
            if ( lifeTimer > lifeTime)
            {
                if (explosionPrefab) Explode();

                //Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (explosionPrefab)
            {
                Explode();
                Destroy(gameObject);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0);                
            }
        }

       /* private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player" && explosionPrefab != null)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2 (1f,0);
                Destroy(gameObject);
            }
        }
*/
        private void Explode()
        {
            var explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
        }
    }
}
