using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cainos.CustomizablePixelCharacter;

    public class CharacterFireBullet : MonoBehaviour
    {
        public GameObject projectilePrefab;                     //the object to shoot out
        public GameObject launchFxPrefab;                       //the fx effect to instantiate at launch
        public Transform muzzle;                                //start position
        public float speed;                                     //start speed
        public float zPos;                                      //z value for projectile's position
        public Vector2 angleOffsetRange;

        public PixelCharacter fx;

        //bool CooldownAttack = false;
       // private IEnumerator coroutine;
        //private IEnumerator CDButton()
       // {
         //   CooldownAttack = true;
          //  yield return new WaitForSeconds(3);
         //   CooldownAttack = false;
        //}
        public void Launch()
        {
            var fx = GetComponent<PixelCharacter>();
            if (!fx) return;

            if (!projectilePrefab) return;
            var projectile = Instantiate(projectilePrefab);

            float angleOffset = Random.Range(angleOffsetRange.x, angleOffsetRange.y);


            //position
            var pos = muzzle.position;
            pos.z = zPos;
            projectile.transform.position = pos;

            //rotation
            float rotZ = (fx.Facing == 1) ? 0.0f : 180.0f;
            rotZ += (fx.Facing == 1) ? angleOffset : -angleOffset;
            projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ);

            var rb2d = projectile.GetComponent<Rigidbody2D>();
            if (rb2d)
            {
                rb2d.velocity = projectile.transform.right * speed;
            }

            //fx
            if (launchFxPrefab)
            {
                var launchFx = Instantiate(launchFxPrefab);
                launchFx.transform.position = muzzle.position;
                launchFx.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ);
            }
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

