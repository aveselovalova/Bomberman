using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBase : MonoBehaviour {
        public abstract GameObject GetPlane();
        public abstract GameObject GetConcreteWalls(int i, int j);
        public abstract GameObject GetBreakWalls(int i, int j);
}