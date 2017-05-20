using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generating : MonoBehaviour {

    public int columnsCount = 11;
    public int rowsCount = 11;
    public int enemyCount = 3;
    private float _cameraOffset=0.5f;
    Building newItem;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(rowsCount / 2f - _cameraOffset, 10  , -5);
        newItem = new Building(columnsCount, rowsCount, enemyCount);
        newItem.CreatePlane();
        newItem.GenerateConcreteWalls();
        newItem.GenerateBreakWalls();
        newItem.SetPlayerOrEnemy(true);//player
        newItem.SetPlayerOrEnemy(false);//enemy
    }

    void Update () {
		
	}
}
