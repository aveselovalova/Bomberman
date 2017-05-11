using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Moving _move;
    void Start()
    {
        _move = new Moving(this);
    }
    void FixedUpdate()
    {
        bool forward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool back = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (forward) _move.MoveForward();
        else if (right) _move.MoveRight();
        else if (left) _move.MoveLeft();
        else if (back) _move.MoveBack();
    }



}
