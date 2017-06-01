using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementController
{
    public float speed = 3f;
    public AudioClip deathSound;
    private Animator _animator;
    public void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void FixedUpdate()
    {
        
        Move(speed);
        ChoseRotation();
        if (IsMove())
            _animator.SetFloat("Speed", 2);
        else
            _animator.SetFloat("Speed", 0);
    }
    
    private bool IsMove()
    {
        return (horizontalMovement != 0 || verticalMovement != 0);
    }
    private void PlayerDeathPlay()
    {
        soundSource.PlayOneShot(deathSound);
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
            enabled = false;
            _animator.SetTrigger("Killed");
            Destroy(gameObject, 3);
            GetComponent<Score>().OutputFailText();
        }
        if (other.gameObject.CompareTag("ConcreteWall"))
        {
            Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);
            transform.position -= movement * speed * Time.fixedDeltaTime;
        }
    }
    
}
