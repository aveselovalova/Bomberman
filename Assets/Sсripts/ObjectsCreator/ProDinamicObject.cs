using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProDynamicObject:DynamicObjectsCreator
{
    public override void CreatePlayer(int xPlayerPosition, int zPlayerPosition)
    {
        var player = CreateDynamicGameObject("Characters/PlayerPro2", new Vector3(xPlayerPosition, 0, zPlayerPosition));
        if (player.GetComponent<PlayerController>() == null)
            player.AddComponent<PlayerController>();
    }
    public override void CreateEnemy(int xPlayerPosition, int zPlayerPosition)
    {
        var player = CreateDynamicGameObject("Characters/EnemyProPro", new Vector3(xPlayerPosition, 0, zPlayerPosition));
        if (player.GetComponent<EnemiesController>() == null)
            player.AddComponent<EnemiesController>();
    }
    public override void CreateIntelligentEnemy(int xEnemyPosition, int zEnemyPosition)
    {
        var enemy = CreateDynamicGameObject("Characters/IntelEnemyPro", new Vector3(xEnemyPosition, 0, zEnemyPosition));
        if (enemy.GetComponent<IntelligentEnemiesController>() == null)
            enemy.AddComponent<IntelligentEnemiesController>();
    }
}
