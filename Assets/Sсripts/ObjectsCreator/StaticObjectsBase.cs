using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticObjectsBase : MonoBehaviour
{
    public abstract void CreatePlane();
    public abstract void CreateConcreteWalls(int xPosition, int zPosition);
    public abstract void CreateBreakWalls(int xPosition, int zPosition);
}
