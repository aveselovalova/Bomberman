using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingBase : MonoBehaviour {

    public abstract void MoveLeft();
    public abstract void MoveRight();
    public abstract void MoveForward();
    public abstract void MoveBack();
}
