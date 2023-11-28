using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtPlatformer_Dungeon
{
    public class Door : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite spriteOpened;
        public Sprite spriteClosed;
        public BoxCollider2D Hitbox;
        public bool IsOpen = false;
        public bool IsLock = false;
        public bool IsInRange = false;
        public Material deafultMaterial;
        public Material LightMaterial;
        public bool IsActive = false;
        private Animator Animator
        {
            get
            {
                if (animator == null ) animator = GetComponent<Animator>();
                return animator;
            }
        }
        private Animator animator;

        [ExposeProperty]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;

                if (Application.isPlaying)
                {
                    Animator.SetBool("IsOpened", isOpened);
                }
                else
                {
                    if(spriteRenderer) spriteRenderer.sprite = isOpened ? spriteOpened : spriteClosed;
                }
            }
        }
        [SerializeField,HideInInspector]
        private bool isOpened;

        private void Start()
        {
            Animator.Play(isOpened ? "Opened" : "Closed");
            IsOpened = isOpened;
        }


        public void Open()
        {
            IsOpened = true;
            Hitbox.enabled = false;
        }

        public void Close()
        {
            IsOpened = false;
            Hitbox.enabled = true;
        }

        private void Update()
        {
            if(!IsLock)
            {
                if (IsOpen) Open();
                else Close();
            }

            if(IsActive)
            {
                if (IsInRange)
                {
                    spriteRenderer.GetComponent<SpriteRenderer>().material = LightMaterial;
                    Color Test = new Color(3f, 255f, 0f, 255f);
                    spriteRenderer.GetComponent<SpriteRenderer>().color = Test;
                }
                else if (!IsInRange)
                {
                    spriteRenderer.GetComponent<SpriteRenderer>().material = deafultMaterial;
                    Color Test2 = new Color(255f, 255f, 255f, 255f);
                    spriteRenderer.GetComponent<SpriteRenderer>().color = Test2;
                }
            }            
        }
    }
}
