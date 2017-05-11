using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MovingBase
{
    private float _speed = 4f;
    private PlayerMovement _gameObj;
    public Moving(PlayerMovement gameObj)
    {
        _gameObj = gameObj;
    }
    public override void MoveLeft()
    {
        _gameObj.transform.eulerAngles = new Vector3(0, 180, 0);
        _gameObj.transform.position += new Vector3(-_speed * Time.deltaTime, 0, 0);
    }
    public override void MoveRight()
    {
        _gameObj.transform.eulerAngles = new Vector3(0, 0, 0);
        _gameObj.transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
    }
    public override void MoveForward()
    {
        _gameObj.transform.eulerAngles = new Vector3(0, -90, 0);
        _gameObj.transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
    }
    public override void MoveBack()
    {
        _gameObj.transform.eulerAngles = new Vector3(0, 90, 0);
        _gameObj.transform.position += new Vector3(0, 0, -_speed * Time.deltaTime);
    }
}
