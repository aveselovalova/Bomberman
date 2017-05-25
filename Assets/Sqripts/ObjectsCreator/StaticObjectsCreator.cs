using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectsCreator : StaticObjectsBase
{
    private int _columnsCount;
    private int _rowsCount;
    private float _planeOffset = 0.5f;
    private float _offsetToZeroCoordinate = 2f;
    private float _yPlanePosition = 0;
    private float _dividerForCorrectPlaneSize = 10f;
    private float _planeHeight = 1;

    private float _cubeEdge = 0.8f;
    private float _yCubePosition = 0.4f;

    public StaticObjectsCreator(int columns, int rows)
    {
        _columnsCount = columns;
        _rowsCount = rows;
    }
    private GameObject CreateStaticGameObject(string pathToObject, Vector3 objectSize, Vector3 objectPosition)
    {
        var gameObj = ResourceLoader.LoadItem(pathToObject);
        gameObj.transform.localScale = objectSize;
        return Instantiate(gameObj, objectPosition, new Quaternion(0, 0, 0, 0));
    }
    public override void CreatePlane()
    {
        var size = new Vector3(_columnsCount / _dividerForCorrectPlaneSize, _planeHeight, _rowsCount / _dividerForCorrectPlaneSize);

        var position = new Vector3(_columnsCount / _offsetToZeroCoordinate - _planeOffset,
                                    _yPlanePosition,
                                    _rowsCount / _offsetToZeroCoordinate - _planeOffset);

        CreateStaticGameObject("Plane", size, position);
    }
    public override void CreateConcreteWalls(int xPosition, int zPosition)
    {
        var size = new Vector3(_cubeEdge, _cubeEdge, _cubeEdge);
        var position = new Vector3(xPosition, _yCubePosition, zPosition);
        CreateStaticGameObject("Walls/ConcreteWall", size, position);
    }
    public override void CreateBreakWalls(int xPosition, int zPosition)
    {
        var size = new Vector3(_cubeEdge, _cubeEdge, _cubeEdge);
        var position = new Vector3(xPosition, _yCubePosition, zPosition);
        CreateStaticGameObject("Walls/BreakWall", size, position);
    }
}
