using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsController : MonoBehaviour
{
    private const int _bigRadius=2;
    private float _extraSpeed;
    private PlayerController _player;
    private BombLaying _bomb;
    private Collider _collider;

    void Start ()
    {
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
        var count = ++_bomb.maxBombCount;
        GetComponent<Score>().OutputAvaliableBombCount(count);
    }
    private void SetRadius()
    {
        _bomb.radius = _bigRadius;
        transform.FindChild("BigRadius").gameObject.SetActive(true);
    }

    private void ExtraSpeed()
    {
        _player.speed = _extraSpeed;
        transform.FindChild("SpeedEffect").gameObject.SetActive(true);
    }
    private void WallPass()
    {
        var breakWalls = GameObject.FindGameObjectsWithTag("BreakWall");
        foreach (var wall in breakWalls)
        {
            ChangeBreakWallColor(wall);
            Physics.IgnoreCollision(_collider, wall.GetComponent<Collider>());
        }
    }
    private void ChangeBreakWallColor(GameObject wall)
    {
        Color breakWallsColor = wall.GetComponent<Renderer>().material.color;
        breakWallsColor.a = 0.75f;
        wall.GetComponent<Renderer>().material.color= breakWallsColor;
    }
}
