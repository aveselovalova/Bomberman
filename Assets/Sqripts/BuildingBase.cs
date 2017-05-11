using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBase : MonoBehaviour {
        public abstract GameObject CreatePlane();
        public abstract GameObject CreateConcreteWalls(int i, int j);
        public abstract GameObject CreateBreakWalls(int i, int j);
}