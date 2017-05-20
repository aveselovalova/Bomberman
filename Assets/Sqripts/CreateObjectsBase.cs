using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBase : MonoBehaviour {
        public abstract GameObject CreatePlane();
        public abstract GameObject CreateConcreteWalls(int xPosition, int zPosition);
        public abstract GameObject CreateBreakWalls(int xPosition, int zPosition);


        public abstract GameObject GetPlayer(int xPosition, int zPosition);
        public abstract GameObject GetEnemy(int xPosition, int zPosition);

}