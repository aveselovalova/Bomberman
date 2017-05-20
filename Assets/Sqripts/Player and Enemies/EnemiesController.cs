using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : RotateBase
{
    private System.Random _rand = new System.Random();
    private float _speed = 1.5f;
    private int _rotateAngle = 0;
    private float _minDistanceBetweenPos = 0.05f;

    void Start()
    {
        StartCoroutine(Moving(Diretcion()));
    }

    private IEnumerator Moving(Vector3 distance)
    {
        while (Vector3.Distance(transform.position, distance + transform.position) > _minDistanceBetweenPos)
        {
            transform.position = Vector3.Lerp(transform.position, distance + transform.position, _speed * Time.deltaTime);
            yield return null;
        }
    }
    private Vector3 Diretcion()
    {
        int directionAmount = 4;
        return SwitchDirection(_rand.Next(directionAmount));
    }
    private Vector3 SwitchDirection(int direct)
    {
        switch (direct)
        {
            case 0:
                {
                    Rotate(_rotateAngle - 90);
                    return Vector3.forward;
                }
            case 1:
                {
                    Rotate(_rotateAngle);
                    return Vector3.right;
                }
            case 2:
                {
                    Rotate(_rotateAngle + 180);
                    return Vector3.left;
                }
            case 3:
                {
                    Rotate(_rotateAngle + 90);
                    return Vector3.back;
                }
            default: return Vector3.zero;
        }

    }


    private void OnCollisionEnter(Collision col)
    {
        NormalMoving(col);
    }
    private void OnCollisionStay(Collision col)
    {
        NormalMoving(col);
    }
    private void NormalMoving(Collision col)
    {
         if (!col.gameObject.CompareTag("Ground"))
        {
            StopAllCoroutines();
            StartCoroutine(Moving(Diretcion()));
        }
    }
    
   
}
