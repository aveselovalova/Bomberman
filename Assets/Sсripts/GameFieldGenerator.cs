using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameFieldGenerator : ObjectCreatorBase
{
    public int columnsCount;
    public int rowsCount;
  
    private bool[,] _isPlaceFilled;
    private int _radiusAroundPlayer = 2;
    private System.Random _random;
    private DynamicObjectsCreator _dynamicObj;
    private ProDynamicObject _proDynamicObj;
    private StaticObjectsCreator _staticObj;


    public GameFieldGenerator(int rows, int columns)
    {
        rowsCount = rows;
        columnsCount = columns;
        _isPlaceFilled = new bool[rowsCount, columnsCount];
        _random = new System.Random();
        _staticObj = new StaticObjectsCreator(columnsCount, rowsCount);
        _dynamicObj = new DynamicObjectsCreator();
        _proDynamicObj = new ProDynamicObject();
    }

    public void GenerateFieldWithGameObjects(int enemiesCount, bool withIntelligentEnemy, int intelligentEnemiesCount=0)
    {
        GeneratePlane();
        GenerateConcreteWalls();
        GenerateBreakWalls();
        GeneratePlayerOrEnemies(Characters.Player,1);
        GeneratePlayerOrEnemies(Characters.Enemy, enemiesCount);
        if(withIntelligentEnemy && intelligentEnemiesCount>0)
            GeneratePlayerOrEnemies(Characters.IntelligentEnemy, intelligentEnemiesCount);
    }

    protected override void GeneratePlane()
    {
        _staticObj.CreatePlane();
    }
    protected override void GenerateConcreteWalls()
    {
        for (int i = 0; i < rowsCount; i++)
            for (int j = 0; j < columnsCount; j++)
                if (IsPlaceForConcreteWall(i, j))
                {
                    _staticObj.CreateConcreteWalls(j,i);
                    _isPlaceFilled[i,j] = true;
                }
    }
    private bool IsPlaceForConcreteWall(int row, int column)
    {
        return (column == columnsCount - 1 || row == rowsCount - 1 || column == 0 || row == 0 ||
                (column % 2 == 0 && row % 2 == 0));
    }
    protected override void GenerateBreakWalls()
    {
        System.Random random = new System.Random();
        int maxCountBreakWalls =  (rowsCount - 1) * (columnsCount - 1) / 5;
        for (int i = 1; i < maxCountBreakWalls; i++)
        {
            int ind1 = random.Next(1, rowsCount- 1);
            int ind2 = random.Next(1, columnsCount - 1);
            if (!_isPlaceFilled[ind1, ind2])
            {
                _staticObj.CreateBreakWalls(ind2, ind1);
                _isPlaceFilled[ind1, ind2] = true;
            }
        }
    }
    protected override void GeneratePlayerOrEnemies(Characters person, int enemyCount)
    {
        while (enemyCount > 0)
        {
            int index1 = _random.Next(1, columnsCount - 1);
            int index2 = _random.Next(1, rowsCount - 1);

            if (!_isPlaceFilled[index2, index1])
            {
                if (person == Characters.Player)
                {
                    MakeDistanceBetweenPlayerAndEnemy(index2, index1);
                    _proDynamicObj.CreatePlayer(index1, index2);
                    break;
                }
                else if (person == Characters.Enemy)
                {
                    _proDynamicObj.CreateEnemy(index1, index2);
                    _isPlaceFilled[index2, index1] = true;
                    enemyCount--;
                }
                else if (person == Characters.IntelligentEnemy)
                {
                    _proDynamicObj.CreateIntelligentEnemy(index1, index2);
                    _isPlaceFilled[index2, index1] = true;
                    enemyCount--;
                }
            }
        }
    }
   
    private void MakeDistanceBetweenPlayerAndEnemy(int xPlayerPosition, int zPlayerPosition)
    {
        List<int> area = PlayerArea(xPlayerPosition, zPlayerPosition);
        int upperBorder = area[0]; 
        int leftBorder = area[1];
        int rightBorder = area[2];
        int bottomBorder= area[3];
        for (int i = upperBorder; i <= bottomBorder; i++)
            for (int j = leftBorder; j <= rightBorder; j++)
                    _isPlaceFilled[i, j] = true;
    }
    private List<int> PlayerArea(int xPlayerPosition, int zPlayerPosition)
    {
        int upperBorder = (xPlayerPosition - _radiusAroundPlayer) > 0 ? xPlayerPosition - _radiusAroundPlayer : 0;
        int leftBorder = (zPlayerPosition - _radiusAroundPlayer) > 0 ? zPlayerPosition - _radiusAroundPlayer : 0;
        int rightBorder = (zPlayerPosition + _radiusAroundPlayer) < columnsCount ? zPlayerPosition + _radiusAroundPlayer : columnsCount - 1;
        int bottomBorder = (xPlayerPosition + _radiusAroundPlayer) < rowsCount ? xPlayerPosition + _radiusAroundPlayer : rowsCount - 1;
        return new List<int>() { upperBorder, leftBorder, rightBorder, bottomBorder };
    }
}



