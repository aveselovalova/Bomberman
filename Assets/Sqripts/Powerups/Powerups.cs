using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour {

    private float _rotateSpeed = 5f;
    private float _xRoteteOffset = 2f;
    private float _yRoteteOffset = 5f;

    void Update ()
    {
        transform.Rotate(new Vector3(_xRoteteOffset,_yRoteteOffset,0) * _rotateSpeed * Time.deltaTime);
	}
}
public enum Powerup
{
    BombCount,
    FlameRadius,
    Speed,
    BreakWallpass
}
