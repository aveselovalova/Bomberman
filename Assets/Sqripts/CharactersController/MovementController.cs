using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MovementControllerBase
{

    protected override void Move(float speed)
    {
        MoveInDirection();
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);
        transform.position += movement * speed * Time.fixedDeltaTime;
    }
    protected virtual void MoveInDirection()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }
    protected override void GetObjectRotation(int angle)
    {
        transform.eulerAngles = new Vector3(0, angle, 0);
    }
    protected override void ChoseRotation()
    {
        if (horizontalMovement == 0)
        {
            if (verticalMovement < 0)
                GetObjectRotation(_rotationAngle + 90);
            if (verticalMovement > 0)
                GetObjectRotation(_rotationAngle - 90);
        }
        if (verticalMovement == 0)
        {
            if (horizontalMovement > 0)
                GetObjectRotation(_rotationAngle);
            if (horizontalMovement < 0)
                GetObjectRotation(_rotationAngle + 180);
        }
    }
}
