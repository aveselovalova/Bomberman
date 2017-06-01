using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IntelligentEnemiesController : EnemiesController
{

    private int _columnsCount;
    private int _rowsCount;

    private List<Point> _pathToAim;
    private Point prePlayerPos;
    private bool[,] gameField;
    private Animator _animator;

    private void Start()
    {
        _pathToAim = new List<Point>();
        GetFieldSizes();
        gameField = new bool[_columnsCount, _rowsCount];
        GetGameField(GetAllWallsOnPlane());
        _animator = GetComponent<Animator>();

        StartCoroutine(GetNewPath());
    }
    private void Update()
    {
        prePlayerPos = GetPlayerPosition();
    }
    protected override void MoveInDirection()
    {
        if (_pathToAim == null)
            base.MoveInDirection();
        else
            ChoseNewDirection();
        
    }
    protected void ChoseNewDirection()
    {
        if (_pathToAim != null)
        {
            if (_pathToAim[0].Y < _pathToAim[1].Y)//forward
            {
                horizontalMovement = 0;
                verticalMovement = 1;
            }
            if (_pathToAim[0].X < _pathToAim[1].X)//right
            {
                horizontalMovement = 1;
                verticalMovement = 0;
            }
            if (_pathToAim[0].Y > _pathToAim[1].Y)//back
            {
                horizontalMovement = 0;
                verticalMovement = -1;
            }
            if (_pathToAim[0].X > _pathToAim[1].X)//left
            {
                horizontalMovement = -1;
                verticalMovement = 0;
            }
        }
    }

    private IEnumerator GetNewPath()
    {
        var playerPos = GetPlayerPosition();
        var intelEnemyPos = GetIntelligentEnemyPosition();

        _pathToAim = AStartAlgorithm.FindPath(gameField, intelEnemyPos, playerPos);

        yield return new WaitWhile(()=>playerPos==prePlayerPos);
        StartCoroutine(GetNewPath());
    }

    public Point GetPlayerPosition()
    {
        return GetRoundPosition.GetPoint(GameObject.FindGameObjectWithTag("Hero").transform.position);
    }
    private Point GetIntelligentEnemyPosition()
    {
        return GetRoundPosition.GetPoint(transform.position);
    }
    private void GetFieldSizes()
    {
        _columnsCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameInitializer>().columnsCount;
        _rowsCount = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameInitializer>().rowsCount;
    }
    private void GetGameField(List<Point> wallsList)
    {
        for (int i = 0; i < _rowsCount; i++)
            for (int j = 0; j < _columnsCount; j++)
                gameField[j, i] = (wallsList.Exists(point => point.X == j && point.Y == i)) ? true : false;
    }
    public void ClearPositionOnField(Point breakWallPoint)
    {
        gameField[breakWallPoint.X, breakWallPoint.Y] = false;
    }
    public void FillPositionOnField(Point bombPos)
    {
        gameField[bombPos.X, bombPos.Y] = true;
    }

    private List<Point> GetWallsOfType(string wallType)
    {
        var listOfWalls = new List<Point>();
        var wallsArr = GameObject.FindGameObjectsWithTag(wallType);
        foreach (var wall in wallsArr)
            listOfWalls.Add(GetRoundPosition.GetPoint(wall.transform.position));
        return listOfWalls;
    }
    private List<Point> GetAllWallsOnPlane()
    {
        return GetWallsOfType("ConcreteWall").Concat(GetWallsOfType("BreakWall")).ToList();
    }
    //protected override void OnCollisionStay(Collision other)
    //{
    //    if (!other.gameObject.CompareTag("Ground") && (_pathToAim == null))
    //         base.OnCollisionStay(other);
    //}

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            _animator.SetTrigger("Attack");
        }
    }

}