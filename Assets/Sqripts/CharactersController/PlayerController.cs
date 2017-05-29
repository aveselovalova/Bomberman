using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController
{
    public float speed = 3f;

    public void FixedUpdate()
    {
        Move(speed);
        ChoseRotation();
    }
   
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Bomb"))
            col.isTrigger = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("IntelligentEnemy"))
        {
            Destroy(gameObject);
            GetComponent<Score>().OutputFailText();
        }
    }
}
