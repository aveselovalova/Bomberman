using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicObjectsBase : MonoBehaviour
{
    public abstract void CreatePlayer(int xPosition, int zPosition);
    public abstract void CreateEnemy(int xPosition, int zPosition);
    public abstract void CreateIntelligentEnemy(int xPosition, int zPosition);
    public abstract void CreatePowerUp(string powerupName, Vector3 powerupPos);
}
