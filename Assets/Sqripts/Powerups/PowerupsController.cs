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
    private Animator _animator;


    void Start ()
    {
        _extraSpeed = 4f;
        _player = GetComponent<PlayerController>();
        _bomb = GetComponent<BombLaying>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Powerups>())
        {
            _animator.SetTrigger("SetBomb");
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
                PassThroughWalls();
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
        SetActiveChildElement("BigRadius");
    }

    private void ExtraSpeed()
    {
        _player.speed = _extraSpeed;
        SetActiveChildElement("SpeedEffect");
    }
    private void SetActiveChildElement(string childName)
    {
        transform.FindChild(childName).gameObject.SetActive(true);
    }
    private void PassThroughWalls()
    {
        var breakWalls = GameObject.FindGameObjectsWithTag("BreakWall");
        foreach (var wall in breakWalls)
        {
            DiscolourBreakWall(wall);
            Physics.IgnoreCollision(_collider, wall.GetComponent<Collider>());
        }
    }
    private void DiscolourBreakWall(GameObject wall)
    {
        Color breakWallsColor = wall.GetComponent<Renderer>().material.color;
        breakWallsColor.a = 0.7f;
        wall.GetComponent<Renderer>().material.color= breakWallsColor;
    }
}
