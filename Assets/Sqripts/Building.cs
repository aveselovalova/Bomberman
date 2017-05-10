using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Building : BuildingBase
{
    private int _columnsCount = 11;
    private int _rowsCount = 11;
    private float _cubeEdge = 0.8f;
    private float _yCubePosition=0.3f;
    private ResourcesLoading _resource;
    private bool[,] _isFillPlace;
    private GameObject _gameObj;

    public Building()
    {
        _resource = new ResourcesLoading();
        _isFillPlace = new bool[_rowsCount, _columnsCount];
    }

    public override GameObject GetPlane()
    {
        _gameObj = _resource.LoadItem("Plane");
        return Instantiate(_gameObj, new Vector3(5f, 0, 5f), new Quaternion(0, 0, 0, 0));
    }

    public override GameObject GetConcreteWalls(int xPosition, int zPosition)
    {
        _gameObj = _resource.LoadItem("Walls/ConcreteWall");
        _gameObj.transform.localScale = new Vector3(_cubeEdge, _cubeEdge, _cubeEdge);
        return Instantiate(_gameObj, new Vector3(xPosition, _yCubePosition, zPosition), new Quaternion(0, 0, 0, 0));
    }

    public override GameObject GetBreakWalls(int xPosition, int zPosition)
    {
        _gameObj = _resource.LoadItem("Walls/BreakWall");
        _gameObj.transform.localScale = new Vector3(_cubeEdge, _cubeEdge, _cubeEdge);
        return Instantiate(_gameObj, new Vector3(xPosition, _yCubePosition, zPosition), new Quaternion(0, 0, 0, 0));
        //_gameObject breakwalls = _resource.LoadItem("Walls/BreakWall");
        //breakwalls.transform.localScale = new Vector3(_cubeEdge, _cubeEdge, _cubeEdge);
        //return Instantiate(breakwalls, new Vector3(xPosition, _yCubePosition, zPosition), new Quaternion(0, 0, 0, 0));
    }

    public void GenerateConcreteWalls()
    {
        for (int i = 0; i < _columnsCount; i++)
            for (int j = 0; j < _rowsCount; j++)
                if (IsPlaceForConcreteWall(i, j))
                {
                    GetConcreteWalls(i, j);
                    _isFillPlace[i,j] = true;
                }
    }

    public void GenerateBreakWalls()
    {
        System.Random random = new System.Random();
        int maxCountBreakWalls = (_rowsCount-1) * (_columnsCount-1) / 4;
        for (int i = 1; i < maxCountBreakWalls; i++)
        {
            int ind1 = random.Next(1, _columnsCount - 1);
            int ind2 = random.Next(1, _rowsCount - 1);
            if (!_isFillPlace[ind1,ind2])
                GetBreakWalls(ind1,ind2);
        }
    }

    private bool IsPlaceForConcreteWall(int column, int row)
    {
        return ((column == _columnsCount - 1 || row == _rowsCount - 1 || column == 0 || row == 0) || 
                (column % 2 == 0 && row % 2 == 0)) ? true : false;
    }
}



