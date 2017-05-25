using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsController : MonoBehaviour
{
    private int _bigRadius;
    private float _extraSpeed;
    private PlayerController _player;
    private BombLaying _bomb;
    private Collider _collider;

    void Start ()
    {
        _bigRadius = 3;
        _extraSpeed = 4f;
        _player = GetComponent<PlayerController>();
        _bomb = GetComponent<BombLaying>();
        _collider = GetComponent<Collider>();
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Powerups>())
        {
            ChoseEnumElement(other.name);
            other.gameObject.SetActive(false);
        }
    }
    private void ChoseEnumElement(string name)
    {
        switch (name)
        {
            case "BombCount(Clone)":
                SetMaxBombCount();
                break;
            case "FlameRadius(Clone)":
                SetRadius();
                break;
            case "Speed(Clone)":
                ExtraSpeed();
                break;
            case "BreakWallpass(Clone)":
                WallPass();
                break;
        }
    }
    private void SetMaxBombCount()
    {
        _bomb.maxBombCount++;
    }
    private void SetRadius()
    {
        _bomb.radius = (_bomb.radius!= _bigRadius) ? _bigRadius : _bomb.radius;
    }
    private void ExtraSpeed()
    {
        _player.speed = _extraSpeed;
    }
    private void WallPass()
    {
        var breakWalls = GameObject.FindGameObjectsWithTag("BreakWall");
        foreach (var wall in breakWalls)
            Physics.IgnoreCollision(_collider, wall.GetComponent<Collider>());
    }

}
