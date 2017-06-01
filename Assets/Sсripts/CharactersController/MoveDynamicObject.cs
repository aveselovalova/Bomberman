using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class MoveDynamicObject : MonoBehaviour
    {
        protected float moveHorizontal;
        protected float moveVertical;
        private float speed=0.05f;
        public AudioClip step;
        protected AudioSource sound;

        public void FixedUpdate()
        {
            Move1();
        }


        protected void PlayStepSound() {
            sound.PlayOneShot(step,0.5f);
        }

        public void ActivateMaxSpeed() {
            speed = 0.08f;
        }

        public void Move1()
        {
            SetNewDirection();
            if (CanMove())
            {             
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                transform.position += movement * speed;
                SetYRotation(GetYRotation(moveHorizontal, moveVertical));
            }
        }

        virtual protected void SetNewDirection()
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }

        protected virtual bool CanMove()
        {
            return (moveHorizontal != 0 || moveVertical != 0);
        }

        private void SetYRotation(int yRotation)
        {
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        private int GetYRotation(float Horizontal, float Vertical)
        {

            if (Horizontal == 0)
            {
                if (Vertical < 0)
                    return 180;
                if (Vertical > 0)
                    return 0;
            }
            if (Horizontal < 0)
            {
                if (Vertical < 0)
                    return 225;
                if (Vertical == 0)
                    return 270;
                if (Vertical > 0)
                    return 315;
            }
            if (Horizontal > 0)
            {
                if (Vertical > 0)
                    return 45;
                if (Vertical < 0)
                    return 135;
                if (Vertical == 0)
                    return 90;
            }
            return 0;
        }
    }
}
