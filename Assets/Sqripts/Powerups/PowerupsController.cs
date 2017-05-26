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
        _bigRadius = 2;
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
        if (_bomb.radius != _bigRadius)
        {
            _bomb.radius = _bigRadius;
            StartCoroutine(GetRadius());
        }
        else StopCoroutine(GetRadius());
    }

    private void ExtraSpeed()
    {
        _player.speed = _extraSpeed;
        StartCoroutine(GetPlayerPos("Effects/SpeedEffect"));
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
        wall.GetComponent<Renderer>().material.color = Color.white;
    }
    private IEnumerator GetPlayerPos(string path)
    {
        var effect = new DynamicObjectsCreator().CreateDynamicGameObject(path, GetPlayerPosition());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(GetPlayerPos(path));
    }
    private IEnumerator GetRadius()
    {
        var position = GetPlayerPosition();
        var radius = new DynamicObjectsCreator().CreateDynamicGameObject("Effects/BigRadius", position);
        yield return new WaitForSeconds(0.1f);
        if (position != GetPlayerPosition())
        {
            Destroy(radius);
        }
        StartCoroutine(GetRadius());
    }
    private Vector3 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Hero").transform.position;
    }
}
