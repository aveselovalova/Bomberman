using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RotateBase : MonoBehaviour {

    protected virtual void Rotate(int angle)
    {
        transform.eulerAngles = new Vector3(0, angle, 0);
    }
}
