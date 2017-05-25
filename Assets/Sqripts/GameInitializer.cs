using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    public int columnsCount = 11;
    public int rowsCount = 11;
    public int enemyCount = 3;
    public bool intelligentEnemy = false;
    public int intelligentEnemyCount = 0;
    private float _cameraOffset = 0.5f;
    private float _cameraZPos = -5f;
    private float _cameraYPos = 10;
    private float _cameraXPosRelativelyRows = 2;
    GameFieldGenerator bombermanField;
    public GameObject scores;
    public GameObject winOrfail;
    void Start ()
    {
        transform.position = new Vector3(rowsCount / _cameraXPosRelativelyRows - _cameraOffset , _cameraYPos, _cameraZPos);

        scores = new UICreator().LoadUI("UIElements/Canvas");
        winOrfail = new UICreator().LoadUI("UIElements/WinOrFail");

        bombermanField = new GameFieldGenerator(columnsCount, rowsCount);
        bombermanField.GenerateFieldWithGameObjects(enemyCount, intelligentEnemy, intelligentEnemyCount);
    }
}
