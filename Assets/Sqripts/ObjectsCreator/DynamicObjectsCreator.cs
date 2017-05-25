using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectsCreator : DynamicObjectsBase
{
    private float _yPos = 1f;
    public GameObject CreateDynamicGameObject(string pathToObject, Vector3 objectPosition)
    {
        var gameObj = ResourceLoader.LoadItem(pathToObject);
        return Instantiate(gameObj, objectPosition, new Quaternion(0, 0, 0, 0));
    }
    public override void CreatePlayer(int xPlayerPosition, int zPlayerPosition)
    {
        var player = CreateDynamicGameObject("Characters/Player", new Vector3(xPlayerPosition, _yPos, zPlayerPosition));
        if (player.GetComponent<PlayerController>() == null)
            player.AddComponent<PlayerController>();
    }
    public override void CreateEnemy(int xEnemyPosition, int zEnemyPosition)
    {
        var enemy = CreateDynamicGameObject("Characters/Enemy", new Vector3(xEnemyPosition, _yPos, zEnemyPosition));
        if (enemy.GetComponent<EnemiesController>() == null)
            enemy.AddComponent<EnemiesController>();
    }
    public override void CreateIntelligentEnemy(int xEnemyPosition, int zEnemyPosition)
    {
        var enemy = CreateDynamicGameObject("Characters/IntelligentEnemy", new Vector3(xEnemyPosition, _yPos, zEnemyPosition));
        if (enemy.GetComponent<IntelligentEnemiesController>() == null)
            enemy.AddComponent<IntelligentEnemiesController>();
    }
    public override void CreatePowerUp(string powerupName, Vector3 powerupPos)
    {
        var player = CreateDynamicGameObject("PowerUp/" + powerupName, powerupPos);
    }
}
