using UnityEngine;

public class EnemiesController : MovementController
{
    public AudioClip deathSound;
    public AudioClip attackSound;
    protected const float _timeForRotation = 3f;
    protected float _timer = 0;
    protected Animator animator;

    private System.Random _rand = new System.Random();
    private int _directionAmount = 4;
    private float _speed=1.5f;
    
    private void Start()
    {
        ChoseDirection();
        animator = GetComponent<Animator>();
    }
    public void FixedUpdate()
    {
        Move(_speed);
        ChoseRotation();
    }
   
    protected override void MoveInDirection()
    {
        if (_timer == 0)
        {
            transform.position = transform.position.RoundXZCoordinate();
            ChoseDirection();
            _timer = _timeForRotation;
        }
        else if (_timer > 0)
            _timer -= Time.fixedDeltaTime;
    }

    protected void ChoseDirection()
    {
        int direct = _rand.Next(_directionAmount);

        switch (direct)
        {
            case 0:
                {
                    horizontalMovement = 0;
                    verticalMovement = 1;
                    break;
                }
            case 1:
                {
                    horizontalMovement = 1;
                    verticalMovement = 0;
                    break;
                }
            case 2:
                {
                    horizontalMovement = 0;
                    verticalMovement = -1;
                    break;
                }
            case 3:
                {
                    horizontalMovement = -1;
                    verticalMovement = 0;
                    break;
                }
        }
    }
    protected void EnemiesDeathPlay()
    {
        soundSource.PlayOneShot(deathSound);
    }
    protected void AttackPlay()
    {
        soundSource.PlayOneShot(attackSound);
    }
    protected virtual void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            transform.position = transform.position.RoundXZCoordinate();
            ChoseDirection();
        }
    }
    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            animator.SetTrigger("Attack");
        }
    }
}