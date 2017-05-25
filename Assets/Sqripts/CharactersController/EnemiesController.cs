using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MovementController
{
    private System.Random _rand = new System.Random();
    protected const float _timeForRotation = 3f;
    protected float _timer = 0;
    private int _directionAmount = 4;
    private float _speed=1.5f;

    private void Start()
    {
        ChoseDirection();
    }
    public void FixedUpdate()
    {
        MoveInDirection();
        Move(_speed);
        ChoseRotation();
    }
    protected override void MoveInDirection()
    {
        if (_timer == 0)
        {
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
    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            ChoseDirection();
    }
}