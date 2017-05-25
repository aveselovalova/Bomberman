using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectCreatorBase : MonoBehaviour
{
    protected abstract void GeneratePlane();
    protected abstract void GenerateConcreteWalls();
    protected abstract void GenerateBreakWalls();
    protected abstract void GeneratePlayerOrEnemies(Characters person, int enemyCount);
}

public enum Characters
{
    Player,
    Enemy,
    IntelligentEnemy
}