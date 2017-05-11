using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Building : BuildingBase
{
    public int _columnsCount;
    public int _rowsCount;
    private float _planeOffset = 0.5f;

    private float _cubeEdge = 0.8f;
    private float _yCubePosition=0.4f;

    private ResourcesLoading _resource;
    private bool[,] _isPlaceFilled;
    private GameObject _gameObj;

    private int _radiusAroundPlayer = 2;
    private int _enemyCount = 3;

    private System.Random _random;
    
    public Building(int rows, int columns, int enemies)
    {
        _rowsCount = rows;
        _columnsCount = columns;
        _enemyCount = enemies;
        _resource = new ResourcesLoading();
        _isPlaceFilled = new bool[_rowsCount, _columnsCount];
        _random = new System.Random();
    }
    //
    //CREATE OBBJECTS
    //
    public override GameObject CreatePlane()
    {
        _gameObj = _resource.LoadItem("Plane");
        _gameObj.transform.localScale = new Vector3(_columnsCount/10f, 1, _rowsCount/10f);
        return Instantiate(_gameObj, new Vector3(_columnsCount/2f- _planeOffset, 0, _rowsCount/2f- _planeOffset), new Quaternion(0, 0, 0, 0));
    }
    public override GameObject CreateConcreteWalls(int xPosition, int zPosition)
    {
        _gameObj = _resource.LoadItem("Walls/ConcreteWall");
        _gameObj.transform.localScale = new Vector3(_cubeEdge, _cubeEdge, _cubeEdge);
        return Instantiate(_gameObj, new Vector3(xPosition, _yCubePosition, zPosition), new Quaternion(0, 0, 0, 0));
    }
    public override GameObject CreateBreakWalls(int xPosition, int zPosition)
    {
        _gameObj = _resource.LoadItem("Walls/BreakWall");
        _gameObj.transform.localScale = new Vector3(_cubeEdge, _cubeEdge, _cubeEdge);
        return Instantiate(_gameObj, new Vector3(xPosition, _yCubePosition, zPosition), new Quaternion(0, 0, 0, 0));
    }
    //
    //WALLS
    //
    public void GenerateConcreteWalls()
    {
        for (int i = 0; i < _rowsCount; i++)
            for (int j = 0; j < _columnsCount; j++)
                if (IsPlaceForConcreteWall(i, j))
                {
                    CreateConcreteWalls(j,i);
                    _isPlaceFilled[i,j] = true;
                }
    }
    private bool IsPlaceForConcreteWall(int row, int column)
    {
        return (column == _columnsCount - 1 || row == _rowsCount - 1 || column == 0 || row == 0 ||
                (column % 2 == 0 && row % 2 == 0));
    }
    public void GenerateBreakWalls()
    {
        System.Random random = new System.Random();
        int maxCountBreakWalls = (_rowsCount-1) * (_columnsCount-1) / 4;
        for (int i = 1; i < maxCountBreakWalls; i++)
        {
            int ind1 = random.Next(1, _rowsCount- 1);
            int ind2 = random.Next(1, _columnsCount - 1);
            if (!_isPlaceFilled[ind1, ind2])
            {
                CreateBreakWalls(ind2, ind1);
                _isPlaceFilled[ind1, ind2] = true;
            }
        }
    }
    //
    //PLAYER
    //
    private GameObject GetPlayer(int xPlayerPosition, int zPlayerPosition)
    {
        var player = new PlayersGenerating().GetPlayer();
        if (player.GetComponent<PlayerMovement>() == null)
            player.AddComponent<PlayerMovement>();
        return Instantiate(player, new Vector3(xPlayerPosition, 1f, zPlayerPosition), new Quaternion(0, 0, 0, 0));
    }
    
    private void MakeDistanceBetweenPlayerAndEnemy(int xPlayerPosition, int zPlayerPosition)
    {
        List<int> area = PlayerArea(xPlayerPosition, zPlayerPosition);
        for (int i = area[0]; i <= area[area.Count-1]; i++)
            for (int j = area[1]; j <= area[2]; j++)
                    _isPlaceFilled[i, j] = true;
    }
    private List<int> PlayerArea(int xPlayerPosition, int zPlayerPosition)
    {
        int upperBorder = (xPlayerPosition - _radiusAroundPlayer) > 0 ? xPlayerPosition - _radiusAroundPlayer : 0;
        int leftBorder = (zPlayerPosition - _radiusAroundPlayer) > 0 ? zPlayerPosition - _radiusAroundPlayer : 0;
        int rightBorder = (zPlayerPosition + _radiusAroundPlayer) < _columnsCount ? zPlayerPosition + _radiusAroundPlayer : _columnsCount - 1;
        int bottomBorder = (xPlayerPosition + _radiusAroundPlayer) < _rowsCount ? xPlayerPosition + _radiusAroundPlayer : _rowsCount - 1;
        return new List<int>() { upperBorder, leftBorder, rightBorder, bottomBorder };
    }
    //
    //ENEMY
    //
    public GameObject GetEnemy(int xEnemyPosition, int zEnemyPosition)
    {
        var enemy = new PlayersGenerating().GetEnemy();
        return Instantiate(enemy, new Vector3(xEnemyPosition, 1f, zEnemyPosition), new Quaternion(0, 0, 0, 0));
    }
    //
    //SETS PLAYER AND ENEMIES
    //
    public void SetPlayerOrEnemy(bool player)
    {
        while (_enemyCount>0)
        {
            int index1 = _random.Next(1, _columnsCount - 1);
            int index2 = _random.Next(1, _rowsCount - 1);
            if (!_isPlaceFilled[index2, index1])
            {
                if(player)
                {
                    MakeDistanceBetweenPlayerAndEnemy(index2, index1);
                    GetPlayer(index1, index2);
                    break;
                }
                else
                {
                    GetEnemy(index1, index2);
                    _isPlaceFilled[index2, index1] = true;
                    _enemyCount--;
                }
            }
        }
    }
    
}



